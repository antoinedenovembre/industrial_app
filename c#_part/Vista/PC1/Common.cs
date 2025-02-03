using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PC1_Sender

{
    class Common
    {
        public static void AppendLog(TextBox textBox, string message)
        {
            if (textBox.InvokeRequired)
            {
                // Use Invoke to update the UI safely
                textBox.Invoke((Action)(() =>
                {
                    textBox.AppendText(message + "\r\n");
                }));
            }
            else
            {
                // If already on the UI thread, update directly
                textBox.AppendText(message + "\r\n");
            }
        }

        public static void SetStatus(Label label, bool status)
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

        public static void SetVerdict(Label label, int verdict)
        {
            if (label.InvokeRequired)
            {
                label.Invoke((Action)(() =>
                {
                    label.Text = ConvertVerdictToString(verdict);
                    label.BackColor = ConvertVerdictToColor(verdict);

                    // After 1sec, bring back the undefined color
                    Timer timer = new Timer();
                    timer.Interval = 3000;
                    timer.Tick += (sender, e) =>
                    {
                        label.Text = ConvertVerdictToString(0);
                        label.BackColor = ConvertVerdictToColor(0);
                        timer.Stop();
                    };
                    timer.Start();
                }));
            }
            else
            {
                label.Text = ConvertVerdictToString(verdict);
                label.BackColor = ConvertVerdictToColor(verdict);

                // After 1sec, bring back the undefined color
                Timer timer = new Timer();
                timer.Interval = 3000;
                timer.Tick += (sender, e) =>
                {
                    label.Text = ConvertVerdictToString(0);
                    label.BackColor = ConvertVerdictToColor(0);
                    timer.Stop();
                };
                timer.Start();
            }
        }

        private static string ConvertVerdictToString(int verdict)
        {
            switch(verdict)
            {
                case 1:
                    return "Blanc";
                case 2:
                    return "Gris";
                case 3:
                    return "Noir";
                default:
                    return "N/A";
            }
        }

        private static Color ConvertVerdictToColor(int verdict)
        {
            switch (verdict)
            {
                case 1:
                    return Color.White;
                case 2:
                    return Color.Gray;
                case 3:
                    return Color.Black;
                default:
                    return Color.Orange;
            }
        }

        public static List<string> GetDevicesOnNetwork()
        {
            List<string> ipList = new List<string>();

            // Run "arp -a" to get a list of known devices on the network
            Process p = new Process
            {
                StartInfo =
            {
                FileName = "arp",
                Arguments = "-a",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
            };
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            // Extract IPs using regex
            MatchCollection matches = Regex.Matches(output, @"\d+\.\d+\.\d+\.\d+");

            foreach (Match match in matches)
            {
                string ip = match.Value;
                if (ip.StartsWith("172.20"))  // Filter based on your requirement
                {
                    ipList.Add(ip);
                }
            }

            return ipList;
        }

        public static Bitmap ResizeBitmap(Bitmap original, int newWidth, int newHeight)
        {
            Bitmap resized = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
                g.DrawImage(original, 0, 0, newWidth, newHeight);
            }
            return resized;
        }

        public static string SelectCOMPort(string[] ports)
        {
            Form form = new Form();
            form.Text = "Sélectionner le port COM";
            form.Size = new Size(300, 100);
            form.StartPosition = FormStartPosition.CenterScreen;

            ComboBox comboBox = new ComboBox();
            comboBox.Location = new Point(10, 10);
            comboBox.Size = new Size(200, 20);
            comboBox.Items.AddRange(ports);
            comboBox.SelectedIndex = 0;
            form.ShowDialog();

            return comboBox.SelectedItem.ToString();
        }
    }
}
