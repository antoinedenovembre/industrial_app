using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

using libImage;
 
using System.IO.Ports;

namespace seuilAuto
{
    public partial class Form1 : Form
    {
        smcs.IDevice m_device;
        Rectangle m_rect;
        PixelFormat m_pixelFormat;
        UInt32 m_pixelType;
        Timer timAcq;
        static SerialPort _serialPort;

        public Form1()
        {
            InitializeComponent();
            timAcq = new Timer();
            timAcq.Interval = 15;
            timAcq.Tick += timAcq_Tick;
            txt_info.Text = " Attente connexion...\n";
            _serialPort = new SerialPort("COM3", 9600);

        }

        private void buttonOuvrir_Click(object sender, EventArgs e)
        {
            bool cameraConnected = false;

            // initialize GigEVision API
            smcs.CameraSuite.InitCameraAPI();
            smcs.ICameraAPI smcsVisionApi = smcs.CameraSuite.GetCameraAPI();

            if (!smcsVisionApi.IsUsingKernelDriver())
            {
                //MessageBox.Show("Warning: Smartek Filter Driver not loaded.");
            }

            // discover all devices on network  
            smcsVisionApi.FindAllDevices(3.0);
            smcs.IDevice[] devices = smcsVisionApi.GetAllDevices();
            //MessageBox.Show(devices.Length.ToString());
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
                }
            }

            if (!cameraConnected)
            {
                txt_info.Text+= Environment.NewLine + "\n ATTENTION : Caméra non connectée\n";
                buttonOuvrir.BackColor = Color.FromArgb(255, 128, 0);
            }

            timAcq.Start();
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

                        // display image
                        if (bd != null)
                            bitmap.UnlockBits(bd);

                        this.imageDepart.Invalidate();
                    }
                    // remove (pop) image from image buffer
                    m_device.PopImage(imageInfo);
                    // empty buffer
                    m_device.ClearImageBuffer();

                    seuillageAuto_Click(sender, e);
                }
            }
        }

        private void seuillageAuto_Click(object sender, EventArgs e)
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
            string message = _serialPort.ReadExisting();
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
