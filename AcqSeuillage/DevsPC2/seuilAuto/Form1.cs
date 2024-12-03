using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace seuilAuto
{
    public partial class Form1 : Form
    {
        // Comm TCP/IP
        private IPAddress m_ipAdrLocale;
        private TcpClient m_tcpClient;
        private readonly int m_numPort;
        private BackgroundWorker bgWorker = new BackgroundWorker();

        public Form1()
        {
            InitializeComponent();

            // Comm TCP/IP
            m_ipAdrLocale = IPAddress.Parse("172.20.10.11");
            m_numPort = 8001;

            InitializeComponent();
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += BgWorker_DoWork;
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            TcpListener tcpListener = new TcpListener(m_ipAdrLocale, m_numPort);
            tcpListener.Start();

            try
            {
                while (!bgWorker.CancellationPending)
                {
                    if (tcpListener.Pending())
                    {
                        HandleConnectionAsync(tcpListener);
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

        private async void HandleConnectionAsync(TcpListener listener)
        {
            var socket = await listener.AcceptSocketAsync();

            try
            {
                byte[] countBuffer = new byte[4];

                // Read the number of images
                int bytesRead = await socket.ReceiveAsync(new ArraySegment<byte>(countBuffer), SocketFlags.None);
                if (bytesRead != 4)
                {
                    throw new Exception("Failed to read image count.");
                }
                int imageCount = BitConverter.ToInt32(countBuffer, 0);

                for (int i = 0; i < imageCount; i++)
                {
                    // Read the size of the next image
                    byte[] sizeBuffer = new byte[4];
                    bytesRead = await socket.ReceiveAsync(new ArraySegment<byte>(sizeBuffer), SocketFlags.None);
                    if (bytesRead != 4)
                    {
                        throw new Exception($"Failed to read size of image {i + 1}.");
                    }
                    int imageSize = BitConverter.ToInt32(sizeBuffer, 0);

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
                        throw new Exception($"Image {i + 1} data size mismatch.");
                    }

                    // Display the received image in PictureBox (or save it if necessary)
                    using (var ms = new MemoryStream(imageBuffer))
                    {
                        Image receivedImage = Image.FromStream(ms);
                        this.Invoke((MethodInvoker)delegate
                        {
                            pictureBox1.Image = receivedImage;
                        }); 
                    }
                }

                // Send acknowledgment
                string ack = "[SERVER ACK]\r\n";
                await socket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(ack)), SocketFlags.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                socket.Close();
            }
        }

        private void Close(object sender, FormClosingEventArgs e)
        {
            if (bgWorker.IsBusy)
            {
                bgWorker.CancelAsync();
            }

            // Also close client if we have pressed the client button
            if (m_tcpClient != null)
            {
                m_tcpClient.Close();
            }

            Environment.Exit(1);
        }
    }
}
