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
using System.Threading.Tasks;
using System.Threading;

using libImage;
using System.Linq;

namespace seuilAuto
{
    public partial class Form1 : Form
    {
        // GENERAL
        private bool TESTING = false;
        private string m_imageDir = "C:\\Users\\adute\\Desktop\\img\\closed";
        private string[] m_images;
        private int m_imageIndex = 0;

        // Camera
        IDevice m_device;
        Rectangle m_rect;
        PixelFormat m_pixelFormat;
        uint m_pixelType;
        System.Windows.Forms.Timer timAcq;
        private bool m_camConnected = false;

        // Arduino
        static SerialPort m_serialPort;

        // TCP/IP
        private IPAddress m_remoteIP;
        private TcpClient m_tcpClient;
        private NetworkStream m_networkStream;
        private readonly int m_port = 8001;

        public Form1()
        {
            InitializeComponent();
            initTimer();
            initArduino();
            initTCPIP();

            // Launch async initialization of camera API
            Task.Run(() =>
            {
                while (!m_camConnected)
                {
                    try
                    {
                        initCamera();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Camera initialization failed: {ex.Message}");
                        Thread.Sleep(1000); // Avoid tight loop, retry every second
                    }
                }
            });
        }

        private void initTimer()
        {
            timAcq = new System.Windows.Forms.Timer
            {
                Interval = 66 // 15 FPS
            };
            timAcq.Tick += timAcq_Tick;
        }

        private void initCamera()
        {
            if (TESTING)
            {
                m_images = Directory.GetFiles(m_imageDir, "*.png");
                AppendLog(logCam, $"[CAMERA][TESTING MODE] {m_images.Length} images trouvées\r\n");
                m_camConnected = true;
                setStatus(camStatus, true);
                return;
            }

            AppendLog(logCam, $"[CAMERA] Initialisation de la caméra...\r\n");

            CameraSuite.InitCameraAPI();
            ICameraAPI smcsVisionApi = CameraSuite.GetCameraAPI();

            if (!smcsVisionApi.IsUsingKernelDriver())
            {
                Debug.WriteLine("WARNING: Kernel driver is not used.");
            }

            smcsVisionApi.FindAllDevices(3.0);
            IDevice[] devices = smcsVisionApi.GetAllDevices();

            if (devices.Length > 0)
            {
                m_device = devices[0];
                if (m_device != null && m_device.Connect())
                {
                    m_device.SetStringNodeValue("TriggerMode", "Off");
                    m_device.SetStringNodeValue("AcquisitionMode", "Continuous");
                    m_device.SetIntegerNodeValue("TLParamsLocked", 1);
                    m_device.CommandNodeExecute("AcquisitionStart");

                    m_camConnected = true; // Set connected state
                    AppendLog(logCam, $"[CAMERA] Connexion à la caméra établie\r\n");
                    setStatus(camStatus, true);
                    return; // Exit the method as the camera is now connected
                }
            }

            AppendLog(logCam, $"[CAMERA] Echec de la connexion à la caméra\r\n");
            setStatus(camStatus, false);
            m_camConnected = false; // Ensure the flag is set
        }

        private void initArduino()
        {
            m_serialPort = new SerialPort("COM3", 9600);
        }

        private void initTCPIP()
        {
            m_tcpClient = new TcpClient
            {
                // Increase the timeout span for sending and receiving
                SendTimeout = 60000,  // 60 seconds (adjust as needed)
                ReceiveTimeout = 60000  // 60 seconds (adjust as needed)
            };

            // Disable the Nagle Algorithm to send data immediately
            m_tcpClient.Client.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);

            if (TESTING)
                m_remoteIP = IPAddress.Parse("127.0.0.1");
            else
                m_remoteIP = IPAddress.Parse("127.0.0.1");
            // m_remoteIP = IPAddress.Parse("172.20.10.2");
        }

        private void setStatus(Label label, bool status)
        {
            if (label.InvokeRequired)
            {
                label.Invoke((Action)(() =>
                {
                    label.Text = status ? "Connecté" : "Déconnecté";
                    label.ForeColor = status ? Color.Green : Color.Red;
                }));
            }
            else
            {
                label.Text = status ? "Connecté" : "Déconnecté";
                label.ForeColor = status ? Color.Green : Color.Red;
            }
        }

        private void AppendLog(TextBox textBox, string message)
        {
            if (textBox.InvokeRequired)
            {
                // Use Invoke to update the UI safely
                textBox.Invoke((Action)(() =>
                {
                    textBox.AppendText(message);
                }));
            }
            else
            {
                // If already on the UI thread, update directly
                textBox.AppendText(message);
            }
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
                            this.imageDepart.SizeMode = PictureBoxSizeMode.StretchImage;
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
            imageSeuillee.SizeMode = PictureBoxSizeMode.StretchImage;
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
            {
                logCam.AppendText($"[CAMERA] Connexion serial établie\r\n");
            }
            else
            {
                logCam.AppendText($"[CAMERA] Echec de la connexion serial\r\n");
            }
        }

        private void closeCOM(object sender, EventArgs e)
        {
            m_serialPort.Close();
            if (!m_serialPort.IsOpen)
            {
                logCam.AppendText($"[CAMERA] Port série fermé\r\n");
            }
            else
            {
                logCam.AppendText($"[CAMERA] Echec de la fermeture du port série\r\n");
            }
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

        private Bitmap ResizeBitmap(Bitmap original, int newWidth, int newHeight)
        {
            Bitmap resized = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
                g.DrawImage(original, 0, 0, newWidth, newHeight);
            }
            return resized;
        }

        private async Task EnsureConnectedAsync()
        {
            if (m_tcpClient == null || !m_tcpClient.Connected)
            {
                AppendLog(logTCP, "[CLIENT] Reconnecting to the server...\r\n");
                try
                {
                    m_tcpClient?.Dispose();
                    m_tcpClient = new TcpClient();
                    await m_tcpClient.ConnectAsync(m_remoteIP, m_port);
                    m_networkStream = m_tcpClient.GetStream(); // Initialize the stream here
                    AppendLog(logTCP, "[CLIENT] Reconnected successfully.\r\n");
                }
                catch (Exception ex)
                {
                    AppendLog(logTCP, $"[CLIENT] Reconnection failed: {ex.Message}\r\n");
                }
            }
        }

        private async Task EnsureStreamAsync()
        {
            await EnsureConnectedAsync();
            if (m_networkStream == null || !m_networkStream.CanWrite)
            {
                m_networkStream = m_tcpClient.GetStream();
            }
        }

        private async void sendImage(Bitmap bmp)
        {
            try
            {
                // Ensure the connection and stream are ready
                await EnsureStreamAsync();

                // Convert the Bitmap to a byte array
                byte[] imageData;
                using (MemoryStream ms = new MemoryStream())
                {
                    Bitmap resizedBmp = ResizeBitmap(bmp, bmp.Width / 4, bmp.Height / 4);
                    resizedBmp.Save(ms, ImageFormat.Png); // Save the resized bitmap in PNG format
                    imageData = ms.ToArray();
                }

                // Send the image size first
                byte[] imageSize = BitConverter.GetBytes(imageData.Length);
                await m_networkStream.WriteAsync(imageSize, 0, imageSize.Length);

                // Send the image data
                await m_networkStream.WriteAsync(imageData, 0, imageData.Length);

                // Read acknowledgment
                byte[] buffer = new byte[1024];
                int numBytesRead = await m_networkStream.ReadAsync(buffer, 0, buffer.Length);
                string receivedMessage = Encoding.ASCII.GetString(buffer, 0, numBytesRead);
                logTCP.AppendText($"[CLIENT] Acknowledgment reçu: {receivedMessage}\r\n");
            }
            catch (Exception ex)
            {
                logTCP.AppendText($"[CLIENT] Erreur TCP: {ex.Message}\r\n");
                m_tcpClient?.Dispose();
                m_tcpClient = null;
                m_networkStream = null;
            }
        }

        private void Close(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

        private async void connectToServer_Click(object sender, EventArgs e)
        {
            int timeoutMilliseconds = 5000; // Example: 5-second timeout
            bool connected = await ConnectToServerAsync(m_remoteIP, m_port, timeoutMilliseconds);

            if (connected)
            {
                setStatus(tcpStatus, true);
            }
            else
            {
                setStatus(tcpStatus, false);
            }
        }

        private async Task<bool> ConnectToServerAsync(IPAddress ipAddress, int port, int timeoutMilliseconds)
        {
            using (var cts = new CancellationTokenSource(timeoutMilliseconds))
            {
                try
                {
                    // Ensure m_tcpClient is initialized only once and reused
                    if (m_tcpClient == null || !m_tcpClient.Connected)
                    {
                        m_tcpClient = new TcpClient();
                        AppendLog(logTCP, $"[CLIENT] Attempting to connect to {ipAddress} on port {port} with a timeout of {timeoutMilliseconds} ms...\r\n");

                        var connectTask = m_tcpClient.ConnectAsync(ipAddress, port);
                        var timeoutTask = Task.Delay(Timeout.Infinite, cts.Token); // Cancel this task if successful

                        // Wait for either the connection or timeout
                        var completedTask = await Task.WhenAny(connectTask, timeoutTask);

                        if (completedTask == connectTask)
                        {
                            cts.Cancel(); // Cancel the timeout task
                            AppendLog(logTCP, "[CLIENT] Connection successful.\r\n");
                            return true; // Connection successful
                        }
                        else
                        {
                            AppendLog(logTCP, "[CLIENT] Connection timed out.\r\n");
                            m_tcpClient.Dispose(); // Dispose if it times out
                            m_tcpClient = null;
                            return false; // Connection timeout
                        }
                    }
                    else
                    {
                        AppendLog(logTCP, "[CLIENT] Already connected.\r\n");
                        return true; // Already connected
                    }
                }
                catch (Exception ex)
                {
                    AppendLog(logTCP, $"[CLIENT] Connection error: {ex.Message}\r\n");
                    m_tcpClient?.Dispose(); // Clean up on error
                    m_tcpClient = null;
                    return false; // Connection failed
                }
            }
        }
    }
}
