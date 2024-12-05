using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Net.Sockets;
using System.Net;
using smcs;
using System.Diagnostics;
using System.IO;
using System.Text;

using libImage;
using System.Runtime.InteropServices.ComTypes;

namespace seuilAuto
{
    public partial class Form1 : Form
    {
        // GENERAL
        private bool TESTING = true;
        private string m_imageDir = "C:\\Users\\adute\\Desktop\\img\\closed";
        private string[] m_images;
        private int m_imageIndex = 0;

        // Camera
        IDevice m_device;
        Rectangle m_rect;
        PixelFormat m_pixelFormat;
        uint m_pixelType;
        Timer timAcq;

        // Arduino
        static SerialPort m_serialPort;

        // TCP/IP
        private IPAddress m_remoteIP;
        private TcpClient m_tcpClient;
        private readonly int m_port = 8001;

        public Form1()
        {
            InitializeComponent();
            initTimer();
            // initCamera();
            initArduino();
            initTCPIP();

            // Fill a list with all images in the directory (png format)
            m_images = Directory.GetFiles(m_imageDir, "*.png");
        }

        private void initTimer()
        {
            timAcq = new Timer
            {
                Interval = 100
            };
            timAcq.Tick += timAcq_Tick;
        }

        private void initCamera()
        {
            // ------------------- Simple variables -------------------
            txt_info.Text = "Connecting...\n";

            // -------------------- Camera API --------------------
            bool cameraConnected = false;

            // Initialize GigEVision API
            CameraSuite.InitCameraAPI();
            ICameraAPI smcsVisionApi = CameraSuite.GetCameraAPI();

            if (!smcsVisionApi.IsUsingKernelDriver())
            {
                Debug.WriteLine("WARNING: Kernel driver is not used.");
            }

            // Discover all devices on network  
            smcsVisionApi.FindAllDevices(3.0);
            IDevice[] devices = smcsVisionApi.GetAllDevices();

            if (devices.Length > 0)
            {
                // Take first device in list
                m_device = devices[0];
                if (m_device != null && m_device.Connect())
                {

                    // Disable trigger mode
                    bool status = m_device.SetStringNodeValue("TriggerMode", "Off");
                    
                    // Set continuous acquisition mode
                    status = m_device.SetStringNodeValue("AcquisitionMode", "Continuous");
                    
                    // Start acquisition
                    status = m_device.SetIntegerNodeValue("TLParamsLocked", 1);
                    status = m_device.CommandNodeExecute("AcquisitionStart");

                    txt_info.Text += Environment.NewLine + "\nConnected to camera\n";
                    buttonOuvrir.BackColor = Color.LimeGreen;
                }
            }

            if (!cameraConnected)
            {
                txt_info.Text += Environment.NewLine + "\n ERROR : Could not connect to camera\n";
                buttonOuvrir.BackColor = Color.FromArgb(255, 0, 0);
            }
        }

        private void initArduino()
        {
            m_serialPort = new SerialPort("COM3", 9600);
        }

        private void initTCPIP()
        {
            m_remoteIP = IPAddress.Parse("127.0.0.1");
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            timAcq.Start();
        }

        private void timAcq_Tick(object sender, EventArgs e)
        {
            if (TESTING)
            {
                Bitmap bmp = new Bitmap(m_images[m_imageIndex]);
                m_imageIndex = (m_imageIndex + 1) % m_images.Length;
                this.imageDepart.Image = bmp;
                processImage();
                return;
            }

            if (m_device != null && m_device.IsConnected())
            {
                if (!m_device.IsBufferEmpty())
                {
                    IImageInfo imageInfo = null;
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

                        // Display image
                        if (bd != null)
                            bitmap.UnlockBits(bd);

                        this.imageDepart.Invalidate();
                    }

                    // Remove (pop) image from image buffer
                    m_device.PopImage(imageInfo);
                    
                    // Empty buffer
                    m_device.ClearImageBuffer();

                    // Process image
                    processImage();
                }
            }
        }

        private void processImage()
        {
            imageSeuillee.Show();
            valeurSeuilAuto.Show();

            Bitmap bmp = new Bitmap(imageDepart.Image);
            ClImage img = new ClImage();

            unsafe
            {
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                img.objetLibDataImgPtr(1, bmpData.Scan0, bmpData.Stride, bmp.Height, bmp.Width);
                bmp.UnlockBits(bmpData);
            }

            valeurSeuilAuto.Text = img.objetLibValeurChamp(0).ToString();
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

            // Envoi de l'image
            sendImage(bmp);

            // Fin
            imageSeuillee.Invalidate();
        }

        private void openCOM(object sender, EventArgs e)
        {
            m_serialPort.Open();
            if (m_serialPort.IsOpen)
                txt_info.Text += Environment.NewLine + "Connexion au port série établie.";
            else
                txt_info.Text += Environment.NewLine + "Erreur lors de l'ouverture de la com.";
        }

        private void closeCOM(object sender, EventArgs e)
        {
            m_serialPort.Close();
            if (!m_serialPort.IsOpen)
                txt_info.Text += Environment.NewLine + "Port série fermmé.";
            else
                txt_info.Text += Environment.NewLine + "Erreur lors de la fermeture de la com.";
        }

        private string SerialPort_ReadData(object sender, SerialDataReceivedEventArgs e)
        {
            string message = m_serialPort.ReadExisting();
            return message;
        }

        private void Serialport_WriteData(string message)
        {
            m_serialPort.WriteLine(message);
        }

        private async void sendImage(Bitmap bmp)
        {
            using (TcpClient client = new TcpClient())
            {
                await client.ConnectAsync(m_remoteIP, m_port);
                NetworkStream stream = client.GetStream();

                try
                {
                    // Convert the Bitmap to a byte array
                    byte[] imageData;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bmp.Save(ms, ImageFormat.Png); // Save the bitmap in PNG format
                        imageData = ms.ToArray();
                    }

                    // Send the image size first (optional, helps server know how much data to expect)
                    byte[] imageSize = BitConverter.GetBytes(imageData.Length);
                    await stream.WriteAsync(imageSize, 0, imageSize.Length);

                    // Send the image data
                    await stream.WriteAsync(imageData, 0, imageData.Length);
                    // this.tb_log.AppendText("[CLIENT] Image sent successfully.\r\n");

                    // Read acknowledgment
                    byte[] buffer = new byte[1024];
                    int numBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string receivedMessage = Encoding.ASCII.GetString(buffer, 0, numBytesRead);
                    // this.tb_log.AppendText("[CLIENT] Acknowledgment received: " + receivedMessage + "\r\n");
                }
                catch (Exception ex)
                {
                    // this.tb_log.AppendText("[CLIENT] Error: " + ex.Message + "\r\n");
                }
            }
        }

        private void Close(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

        private async void connectToServer_Click(object sender, EventArgs e)
        {
            m_tcpClient = new TcpClient();
            // this.tb_log.AppendText("[CLIENT] Connecting...\r\n");
            try
            {
                await m_tcpClient.ConnectAsync(m_remoteIP, m_port);
            }
            catch (Exception)
            {
                //this.tb_log.AppendText("[CLIENT] Connection failed\r\n");
                return;
            }
            //this.tb_log.AppendText("[CLIENT] Connected\r\n");
        }
    }
}
