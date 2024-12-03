using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

using libImage;
 
using System.IO.Ports;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace seuilAuto
{
    public partial class Form1 : Form
    {
        // Camera
        smcs.IDevice m_device;
        Rectangle m_rect;
        PixelFormat m_pixelFormat;
        uint m_pixelType;
        Timer timAcq;

        // Arduino
        static SerialPort _serialPort;

        // Comm TCP/IP
        private IPAddress m_ipAdrDistante;
        private readonly int m_numPort;

        public Form1()
        {
            InitializeComponent();

            // Camera
            timAcq = new Timer();
            timAcq.Interval = 15;
            timAcq.Tick += timAcq_Tick;
            txt_info.Text = " Attente connexion...\n";

            // Arduino
            _serialPort = new SerialPort("COM3", 9600);

            // Comm TCP/IP
            m_ipAdrDistante = IPAddress.Parse("127.0.0.1");
            m_numPort = 8001;

            InitializeComponent();
        }

        private async void send_image(object sender, EventArgs e, Image img)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                await tcpClient.ConnectAsync(m_ipAdrDistante, m_numPort);
                NetworkStream stream = tcpClient.GetStream();

                try
                {
                    // Convert the Image to a byte array
                    byte[] imageData;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, ImageFormat.Png); // You can use other formats like Jpeg if needed
                        imageData = ms.ToArray();
                    }

                    // Send the image size first (optional, helps server know how much data to expect)
                    byte[] imageSize = BitConverter.GetBytes(imageData.Length);
                    await stream.WriteAsync(imageSize, 0, imageSize.Length);

                    // Send the image data
                    await stream.WriteAsync(imageData, 0, imageData.Length);

                    // Read acknowledgment
                    byte[] buffer = new byte[1024];
                    int numBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string receivedMessage = Encoding.ASCII.GetString(buffer, 0, numBytesRead);
                    MessageBox.Show(receivedMessage, "Acknowledgment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonOuvrir_Click(object sender, EventArgs e)
        {
            bool cameraConnected = false;

            // initialize GigEVision API
            smcs.CameraSuite.InitCameraAPI();
            smcs.ICameraAPI smcsVisionApi = smcs.CameraSuite.GetCameraAPI();

            if (!smcsVisionApi.IsUsingKernelDriver())
            {
                // MessageBox.Show("Warning: Smartek Filter Driver not loaded.");
            }

            // discover all devices on network  
            smcsVisionApi.FindAllDevices(3.0);
            smcs.IDevice[] devices = smcsVisionApi.GetAllDevices();
            if (devices.Length > 0)
            {
                // take first device in list
                m_device = devices[0];
                if (m_device != null && m_device.Connect())
                {
                    // disable trigger mode
                    bool status = m_device.SetStringNodeValue("TriggerMode", "Off");
                    // set continuous acquisition mode
                    status = m_device.SetStringNodeValue("AcquisitionMode", "Continuous");
                    // start acquisition
                    status = m_device.SetIntegerNodeValue("TLParamsLocked", 1);
                    status = m_device.CommandNodeExecute("AcquisitionStart");
                    cameraConnected = true;

                    txt_info.Text += Environment.NewLine + "\n Connexion établie\n";
                    buttonOuvrir.BackColor = Color.LimeGreen;
                    timAcq.Start();
                }
            }

            if (!cameraConnected)
            {
                txt_info.Text += Environment.NewLine + "\n ATTENTION : Caméra non connectée\n";
                buttonOuvrir.BackColor = Color.FromArgb(255, 128, 0);
            }
        }

        private void timAcq_Tick(object sender, EventArgs e)
        {
            if (m_device != null && m_device.IsConnected())
            {
                if (!m_device.IsBufferEmpty())
                {
                    smcs.IImageInfo imageInfo = null;
                    m_device.GetImageInfo(ref imageInfo);
                    if (imageInfo != null)
                    {
                        Bitmap bitmap = (Bitmap)this.imageDepart.Image;
                        BitmapData bd = null;

                        ImageUtils.CopyToBitmap(imageInfo, ref bitmap, ref bd, ref m_pixelFormat, ref m_rect, ref m_pixelType);
                        if (bitmap != null)
                        {
                            this.imageDepart.Height = bitmap.Height;
                            this.imageDepart.Width = bitmap.Width;
                            this.imageDepart.Image = bitmap;
                            this.imageDepart.SizeMode= PictureBoxSizeMode.StretchImage;
                        }

                        send_image(sender, e, bitmap);

                        // display image
                        if (bd != null)
                            bitmap.UnlockBits(bd);

                        this.imageDepart.Invalidate();
                    }
                    // remove (pop) image from image buffer
                    m_device.PopImage(imageInfo);
                    // empty buffer
                    m_device.ClearImageBuffer();

                    seuillageAuto(sender, e);
                }
            }
        }

        private void seuillageAuto(object sender, EventArgs e)
        {
            // traitement donc transférer data bmp vers C++

            imageSeuillee.Show();
            valeurSeuilAuto.Show();

            Bitmap bmp = new Bitmap(imageDepart.Image);
            ClImage Img = new ClImage();

            unsafe
            {
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                Img.objetLibDataImgPtr(1, bmpData.Scan0, bmpData.Stride, bmp.Height, bmp.Width);
                // 1 champ texte retour C++, le seuil auto
                bmp.UnlockBits(bmpData);
            }

            valeurSeuilAuto.Text = Img.objetLibValeurChamp(0).ToString();
            imageSeuillee.SizeMode= PictureBoxSizeMode.StretchImage;
            imageSeuillee.Width = bmp.Width;
            imageSeuillee.Height = bmp.Height;

            // pour centrer image dans panel
            if (imageSeuillee.Width < panel1.Width)
                imageSeuillee.Left = (panel1.Width - imageSeuillee.Width) / 2;

            if (imageSeuillee.Height < panel1.Height)
                imageSeuillee.Top = (panel1.Height - imageSeuillee.Height) / 2;

            // transférer C++ vers bmp
            imageSeuillee.Image = bmp;
        }

        private void ouverture_comUSB(object sender, EventArgs e)
        {
            _serialPort.Open();
            if (_serialPort.IsOpen)
                txt_info.Text += Environment.NewLine + "Connexion au port série établie.";
            else
                txt_info.Text += Environment.NewLine + "Erreur lors de l'ouverture de la com.";
        }

        private void fermeture_comUSB(object sender, EventArgs e)
        {
            _serialPort.Close();
            if (!_serialPort.IsOpen)
                txt_info.Text += Environment.NewLine + "Port série fermmé.";
            else
                txt_info.Text += Environment.NewLine + "Erreur lors de la fermeture de la com.";
        }

        private string SerialPort_ReadData(object sender, SerialDataReceivedEventArgs e)
        {
            string message = _serialPort.ReadLine();
            return message;
        }

        private void Serialport_WriteData(string message)
        {
            _serialPort.WriteLine(message);
        }

        private void Close(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
