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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace PC1_Sender
{
    public partial class PC1 : Form
    {
        // GENERAL
        private bool TESTING = true;

        // Camera
        IDevice m_device;
        Rectangle m_rect;
        PixelFormat m_pixelFormat;
        uint m_pixelType;
        System.Windows.Forms.Timer timAcq;
        private bool m_camConnected = false;
        private Image m_tmpImg;
        private readonly object imgLock = new object();

        // Arduino
        private bool m_arduinoConnected = false;
        static SerialPort m_serialPort;

        // TCP/IP
        private bool m_tcpConnected = false;
        private IPAddress m_remoteIP;
        private TcpClient m_tcpClient;
        private NetworkStream m_networkStream;
        private readonly int m_port = 8001;

        // ============================================= CONSTRUCTOR =============================================
        public PC1()
        {
            InitializeComponent();
            InitArduino();
            InitTimer();
            InitTCP();
            InitLogboxes();

            // Add icon
            this.Icon = Properties.Resources.publish;

            // Launch async initialization of camera API
            Task.Run(() =>
            {
                while (!m_camConnected)
                {
                    try
                    {
                        InitCamera();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Camera initialization failed: {ex.Message}");
                        Thread.Sleep(1000); // Avoid tight loop, retry every second
                    }
                }
            });

            // Launch async connection TCP/IP, but wait each time for the previous one to finish
            Task.Run(async () =>
            {
                while (!m_tcpConnected)
                {
                    try
                    {
                        if (!await ConnectTCP())
                        {
                            Thread.Sleep(1000); // Avoid tight loop, retry every second
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"TCP/IP initialization failed: {ex.Message}");
                        Thread.Sleep(1000); // Avoid tight loop, retry every second
                    }
                }
            });

            // Launch async COM connection
            Task.Run(() =>
            {
                while (!m_arduinoConnected)
                {
                    try
                    {
                        if (!OpenCOM())
                        {
                            Thread.Sleep(1000); // Avoid tight loop, retry every second
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Arduino initialization failed: {ex.Message}");
                        Common.AppendLog(logSerial, $"[ARDUINO] Echec de la connexion: {ex.Message}");
                        Thread.Sleep(1000); // Avoid tight loop, retry every second
                    }
                }
            });

            timAcq.Start();
        }

        private void Close(object sender, FormClosingEventArgs e)
        {
            CloseCOM();
            CloseTCP();
            CloseCamera();
            Environment.Exit(1);
        }

        // ============================================= INITIALIZATION =============================================
        private void InitLogboxes()
        {
            // Make logboxes readonly for the user
            logCam.ReadOnly = true;
            logSerial.ReadOnly = true;
            logTCP.ReadOnly = true;
        }

        private void InitTimer()
        {
            timAcq = new System.Windows.Forms.Timer
            {
                Interval = 500
            };
            timAcq.Tick += Acquisition;
        }

        private void InitCamera()
        {
            Common.AppendLog(logCam, $"[CAMERA] Initialisation de la caméra...");

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
                    Common.AppendLog(logCam, $"[CAMERA] Connexion à la caméra établie");
                    Common.SetStatus(camStatus, true);
                    return; // Exit the method as the camera is now connected
                }
            }

            Common.AppendLog(logCam, $"[CAMERA] Echec de la connexion à la caméra");
            Common.SetStatus(camStatus, false);
            m_camConnected = false; // Ensure the flag is set
        }

        private void InitArduino()
        {
            // Scan for all open COM ports
            string[] ports = SerialPort.GetPortNames();

            // If no ports are found, m_serialPort is set to null
            if (ports.Length == 0)
            {
                Common.AppendLog(logSerial, "[ARDUINO] Aucun port série trouvé");
                return;
            }
            
            // If only one, m_serialPort is set to that port
            m_serialPort = new SerialPort(ports[0], 9600);

            // If more, give the user the choice to select the port
            if (ports.Length > 1)
            {
                string port = Common.SelectCOMPort(ports);
                if (port != null)
                {
                    m_serialPort = new SerialPort(port, 9600);
                }
            }

            Common.AppendLog(logSerial, $"[ARDUINO] Initialisation du port série {m_serialPort.PortName}...");

            m_serialPort.DataReceived += new SerialDataReceivedEventHandler(TriggerProcessImage);
        }

        private void InitTCP()
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
            {
                List<string> devices = Common.GetDevicesOnNetwork();
                foreach (string ip in devices)
                {
                    if (ip.StartsWith("172.20"))
                    {
                        m_remoteIP = IPAddress.Parse(ip);
                        Common.AppendLog(logTCP, "[TCP] Serveur trouvé à l'adresse: " + ip);
                        break;
                    }
                }
            }

            Common.AppendLog(logTCP, "[TCP] Appuyez sur 'Connexion serveur'");
        }

        // ================================================ UTILS ================================================

        // Serial/COM

        private bool OpenCOM()
        {
            if (m_serialPort == null)
            {
                Common.SetStatus(arduinoStatus, false);
                m_arduinoConnected = false;
                return m_arduinoConnected;
            }
            if (!SerialPort.GetPortNames().Contains(m_serialPort.PortName))
            {
                Common.AppendLog(logSerial, "[ARDUINO] Port série non trouvé");
                Common.SetStatus(arduinoStatus, false);
                m_arduinoConnected = false;
                return m_arduinoConnected;
            }

            m_serialPort.Open();

            if (m_serialPort.IsOpen)
            {
                Common.AppendLog(logSerial, "[ARDUINO] Connexion serial établie");
                Common.SetStatus(arduinoStatus, true);
                m_arduinoConnected = true;
            }
            else
            {
                Common.AppendLog(logSerial, "[ARDUINO] Echec de la connexion serial");
                Common.SetStatus(arduinoStatus, false);
                m_arduinoConnected = true;
            }

            return m_arduinoConnected;
        }

        private void CloseCOM()
        {
            m_serialPort.Close();
            if (!m_serialPort.IsOpen)
            {
                Common.AppendLog(logSerial, "[ARDUINO] Port série fermé");
                Common.SetStatus(arduinoStatus, false);
            }
            else
            {
                Common.AppendLog(logSerial, "[ARDUINO] Echec de la fermeture du port série");
                Common.SetStatus(arduinoStatus, false);
            }
        }
        
        private void WriteSerial(string message)
        {
            m_serialPort.WriteLine(message);
        }

        // Ethernet/TCP

        private async Task CheckTCPConnection()
        {
            if (m_tcpClient == null || !m_tcpClient.Connected)
            {
                Common.AppendLog(logTCP, "[TCP] Reconnexion au serveur...");
                Common.SetStatus(tcpStatus, false); // Set status to 'Disconnected
                try
                {
                    m_tcpClient?.Dispose();
                    m_tcpClient = new TcpClient();
                    await m_tcpClient.ConnectAsync(m_remoteIP, m_port);
                    m_networkStream = m_tcpClient.GetStream(); // Initialize the stream here
                    Common.AppendLog(logTCP, "[TCP] Connexion rétablie.");
                    Common.SetStatus(tcpStatus, true); // Set status to 'Connected'
                }
                catch (Exception ex)
                {
                    Common.AppendLog(logTCP, $"[TCP] Connexion perdue: {ex.Message}");
                    Common.SetStatus(tcpStatus, false); // Set status to 'Disconnected'
                }
            }
        }

        private async Task CheckTCPStream()
        {
            await CheckTCPConnection();
            if (m_networkStream == null || !m_networkStream.CanWrite)
            {
                m_networkStream = m_tcpClient.GetStream();
            }
        }

        private async Task<bool> ConnectTCP()
        {
            int timeoutMilliseconds = 5000; // Example: 5-second timeout
            Common.AppendLog(logTCP, "[TCP] En attente du serveur...");
            m_tcpConnected = await ConnectToServerAsync(m_remoteIP, m_port, timeoutMilliseconds);

            if (m_tcpConnected)
            {
                Common.SetStatus(tcpStatus, true);
            }
            else
            {
                Common.SetStatus(tcpStatus, false);
            }

            return m_tcpConnected;
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
                        Common.AppendLog(logTCP, $"[TCP] Connexion à {ipAddress} au port {port} avec un timeout de {timeoutMilliseconds} ms...");

                        var connectTask = m_tcpClient.ConnectAsync(ipAddress, port);
                        var timeoutTask = Task.Delay(Timeout.Infinite, cts.Token); // Cancel this task if successful

                        // Wait for either the connection or timeout
                        var completedTask = await Task.WhenAny(connectTask, timeoutTask);

                        if (completedTask == connectTask)
                        {
                            cts.Cancel(); // Cancel the timeout task
                            Common.AppendLog(logTCP, "[TCP] Connexion réussie.");
                            return true; // Connection successful
                        }
                        else
                        {
                            Common.AppendLog(logTCP, "[TCP] Connexion échouée");
                            m_tcpClient.Dispose(); // Dispose if it times out
                            m_tcpClient = null;
                            return false; // Connection timeout
                        }
                    }
                    else
                    {
                        Common.AppendLog(logTCP, "[TCP] Déjà connecté");
                        return true; // Already connected
                    }
                }
                catch (Exception ex)
                {
                    Common.AppendLog(logTCP, $"[TCP] Erreur lors de la connexion: {ex.Message}");
                    m_tcpClient?.Dispose(); // Clean up on error
                    m_tcpClient = null;
                    return false; // Connection failed
                }
            }
        }

        private void CloseTCP()
        {
            m_tcpClient?.Dispose();
            m_tcpClient = null;
            m_networkStream = null;
            Common.AppendLog(logTCP, "[TCP] Connexion fermée");
            Common.SetStatus(tcpStatus, false);
        }

        // Camera

        private void CloseCamera()
        {
            if (m_device != null)
            {
                if (m_device.IsConnected())
                {
                    m_device.CommandNodeExecute("AcquisitionStop");
                    m_device.SetIntegerNodeValue("TLParamsLocked", 0);
                    m_device.Disconnect();

                    smcs.CameraSuite.ExitCameraAPI();

                    m_camConnected = false;
                    Common.SetStatus(camStatus, false);
                    Common.AppendLog(logCam, "[CAMERA] Caméra déconnectée\r\n");
                }
            }
        }

        // ============================================= MEMBER FUNCTIONS =============================================

        private void Acquisition(object sender, EventArgs e)
        {
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
                            lock(imgLock)
                            {
                                m_tmpImg = bitmap;
                                if (TESTING)
                                {
                                    imageDepart.Image = bitmap;
                                }
                                Console.WriteLine("Image acquired, size : " + bitmap.Size);
                            }
                        }

                        // Display image
                        if (bd != null)
                            bitmap.UnlockBits(bd);
                    }

                    // Remove (pop) image from image buffer
                    m_device.PopImage(imageInfo);

                    // Empty buffer
                    m_device.ClearImageBuffer();
                }
            }
        }

        private void ProcessImage()
        {
            Bitmap bmp;
            lock(imgLock)
            {
                if (m_tmpImg == null)
                {
                    return;
                }
                bmp = (Bitmap) m_tmpImg.Clone();
            }
            ClImage img = new ClImage();

            unsafe
            {
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                img.objetLibDataImgPtr(1, bmpData.Scan0, bmpData.Stride, bmp.Height, bmp.Width);
                bmp.UnlockBits(bmpData);
            }

            if (imageSeuillee.InvokeRequired)
            {
                imageSeuillee.BeginInvoke(new MethodInvoker(delegate
                {
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
                    imageSeuillee.Invalidate();
                }));
            }
            else
            {
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
                imageSeuillee.Invalidate();
            }

            // Envoi de l'image
            SendImageTCP(bmp);

            // Envoi verdict
            int ret = img.objetLibValeurChamp(0);
            bool verdict = ret > 0;

            Common.SetVerdict(verdictLabel, verdict);
            SendVerdictSerial(verdict);
        }

        private void TriggerProcessImage(object sender, SerialDataReceivedEventArgs e)
        {
            string message = m_serialPort.ReadExisting();

            // Clean up message from line breaks and spaces
            message = message.Replace("\n", "").Replace("\r", "").Replace(" ", "");
            if (message.Length == 0 || message.Equals(""))
            {
                return;
            }
            Common.AppendLog(logSerial, $"[ARDUINO] Message : {message}");    

            // If message contains "OBJECT_CAM", then take current tmpImg, ProcessImage it and send it to server
            if (message.Contains("X"))
            {
                Bitmap image;
                lock (imgLock)
                {
                    if (m_tmpImg == null)
                    {
                        return;
                    }
                    image = (Bitmap)m_tmpImg.Clone();
                    Console.WriteLine("Take");
                }
                if (image != null)
                {
                    // Do the above but thread safe
                    if (this.imageDepart.InvokeRequired)
                    {
                        this.imageDepart.BeginInvoke(new MethodInvoker(delegate
                        {
                            this.imageDepart.Height = image.Height;
                            this.imageDepart.Width = image.Width;
                            this.imageDepart.Image = image;
                            this.imageDepart.SizeMode = PictureBoxSizeMode.StretchImage;
                            this.imageDepart.Invalidate();

                            ProcessImage();
                        }));
                    }
                }
            }
        }

        private async void SendImageTCP(Bitmap bmp)
        {
            try
            {
                // Ensure the connection and stream are ready
                await CheckTCPStream();

                // Convert the Bitmap to a byte array
                byte[] imageData;
                using (MemoryStream ms = new MemoryStream())
                {
                    Bitmap resizedBmp = Common.ResizeBitmap(bmp, bmp.Width / 4, bmp.Height / 4);
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
                Common.AppendLog(logTCP, $"[TCP] Acknowledgment reçu: {receivedMessage}");
            }
            catch (Exception ex)
            {
                Common.AppendLog(logTCP, $"[TCP] Erreur TCP: {ex.Message}");
                m_tcpClient?.Dispose();
                m_tcpClient = null;
                m_networkStream = null;
            }
        }

        private void SendVerdictSerial(bool verdict)
        {
            WriteSerial(verdict ? "OK" : "NOK");
        }
    }
}
