﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace PC2
{
    public partial class PC2 : Form
    {
        private bool TESTING = true;
        private IPAddress m_localIP;
        private readonly int m_port = 8001;

        private BackgroundWorker tcpWorker = new BackgroundWorker();

        // ============================================= CONSTRUCTOR =============================================
        public PC2()
        {
            InitializeComponent();
            initWorker();
            initLocalIP();
            initLogbox();

            // Add icon
            this.Icon = Properties.Resources.download;

            // Start the worker when the form is loaded
            this.Load += (s, e) => tcpWorker.RunWorkerAsync();
        }

        // ============================================= INITIALIZATION =============================================
        private void initLogbox()
        {
            // Make logboxes readonly for the user
            logbox.ReadOnly = true;
        }

        private void initWorker()
        {
            tcpWorker.WorkerSupportsCancellation = true;
            tcpWorker.DoWork += workerCallback;
        }

        private void initLocalIP()
        {
            if (TESTING)
            {
                m_localIP = IPAddress.Parse("127.0.0.1");
                return;
            }

            // Select the wireless network interface starting with "172.20"
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && ni.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork && ip.Address.ToString().StartsWith("172.20"))
                        {
                            m_localIP = ip.Address;
                            break;
                        }
                    }
                }
            }
        }


        // ============================================= MEMBER FUNCTIONS =============================================
        private void setStatus(Label label, bool status)
        {
            if (status)
            {
                label.Text = "Connecté";
                label.ForeColor = Color.Green;
            }
            else
            {
                label.Text = "Déconnecté";
                label.ForeColor = Color.Red;
            }
        }

        private void workerCallback(object sender, DoWorkEventArgs e)
        {
            TcpListener tcpListener = new TcpListener(m_localIP, m_port);
            tcpListener.Start();
            this.Invoke((MethodInvoker)delegate { logbox.AppendText("[TCP] Serveur démarré...\r\n"); });

            try
            {
                while (!tcpWorker.CancellationPending)
                {
                    if (tcpListener.Pending())
                    {
                        var socket = tcpListener.AcceptSocketAsync().Result;
                        socket.ReceiveTimeout = 5000; // Set a timeout for ReceiveAsync
                        Task.Run(() => HandleConnectionAsync(socket)); // Process each connection in a separate Task
                    }
                    else
                    {
                        Task.Delay(100).Wait(); // Prevents the loop from consuming too much CPU
                    }
                }
            }
            finally
            {
                tcpListener.Stop();
            }
        }

        private async void HandleConnectionAsync(Socket socket)
        {
            this.Invoke((MethodInvoker)delegate {
                logbox.AppendText("[TCP] Connexion acceptée\r\n");
                setStatus(labelStatus, true);
            });

            try
            {
                while (true)
                {
                    // Read the size of the incoming image
                    byte[] sizeBuffer = new byte[4];
                    int bytesRead = await socket.ReceiveAsync(new ArraySegment<byte>(sizeBuffer), SocketFlags.None);
                    if (bytesRead == 0) break; // Connection closed

                    if (bytesRead != 4)
                    {
                        throw new Exception("Failed to read image size.");
                    }
                    int imageSize = BitConverter.ToInt32(sizeBuffer, 0);

                    this.Invoke((MethodInvoker)delegate
                    {
                        logbox.AppendText($"[TCP] Taille image attendue: {imageSize} octets.\r\n");
                    });

                    // Read the image data
                    byte[] imageBuffer = new byte[imageSize];
                    int totalBytesRead = 0;

                    while (totalBytesRead < imageSize)
                    {
                        int chunkSize = await socket.ReceiveAsync(new ArraySegment<byte>(imageBuffer, totalBytesRead, imageSize - totalBytesRead), SocketFlags.None);
                        if (chunkSize == 0) break; // Connection closed
                        totalBytesRead += chunkSize;
                    }

                    if (totalBytesRead != imageSize)
                    {
                        throw new Exception("Image data size mismatch.");
                    }

                    this.Invoke((MethodInvoker)delegate
                    {
                        logbox.AppendText($"[TCP] Taille image reçue: {totalBytesRead} octets.\r\n");
                    });

                    // Load the image from the byte array and display it in PictureBox
                    using (var ms = new MemoryStream(imageBuffer))
                    {
                        Image receivedImage = Image.FromStream(ms);
                        this.Invoke((MethodInvoker)delegate
                        {
                            updatePictubreBoxImage(pictureBoxReceived, (Bitmap)receivedImage);
                            logbox.AppendText($"[TCP] Image affichée\r\n");
                        });
                    }

                    // Send acknowledgment
                    string ack = "[SERVER ACK]";
                    await socket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(ack)), SocketFlags.None);
                    this.Invoke((MethodInvoker)delegate { logbox.AppendText("[TCP] Acknowledgment envoyé\r\n"); });
                }
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate {
                    logbox.AppendText($"[TCP] Erreur: {ex.Message}\r\n");
                    setStatus(labelStatus, false);
                });
            }
            finally
            {
                socket.Close(); // Ensure the socket is closed after the connection ends
                this.Invoke((MethodInvoker)delegate {
                    logbox.AppendText("[TCP] Connexion fermée.\r\n");
                    setStatus(labelStatus, false);
                });
            }
        }

        private void updatePictubreBoxImage(PictureBox pictureBox, Bitmap img)
        {
            if (pictureBox.InvokeRequired)
            {
                // Si le contrôle nécessite un appel depuis le thread principal
                pictureBox.Invoke((MethodInvoker)(() =>
                {
                    pictureBox.Image = img; // Mise à jour du texte
                }));
            }
            else
            {
                // Si déjà sur le thread principal
                pictureBox.Image = img;
            }
        }
    }
}
