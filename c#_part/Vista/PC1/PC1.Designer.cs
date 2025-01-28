namespace PC1_Sender
{
    partial class PC1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PC1));
            this.ouvrirImage = new System.Windows.Forms.OpenFileDialog();
            this.imageDepart = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imageSeuillee = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.logCam = new System.Windows.Forms.TextBox();
            this.connectToServer = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.logTCP = new System.Windows.Forms.TextBox();
            this.camStatus = new System.Windows.Forms.Label();
            this.tcpStatus = new System.Windows.Forms.Label();
            this.arduinoStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.logSerial = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageDepart)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageSeuillee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ouvrirImage
            // 
            this.ouvrirImage.Filter = "Tous Fichiers | *.*";
            this.ouvrirImage.InitialDirectory = "C:\\Hubert_KONIK\\Recherche\\Database\\Images";
            // 
            // imageDepart
            // 
            this.imageDepart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imageDepart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageDepart.Cursor = System.Windows.Forms.Cursors.Default;
            this.imageDepart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageDepart.ImageLocation = "";
            this.imageDepart.Location = new System.Drawing.Point(0, 0);
            this.imageDepart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.imageDepart.Name = "imageDepart";
            this.imageDepart.Size = new System.Drawing.Size(600, 615);
            this.imageDepart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageDepart.TabIndex = 0;
            this.imageDepart.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.imageDepart);
            this.panel1.Location = new System.Drawing.Point(18, 231);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 615);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.imageSeuillee);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(647, 231);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 615);
            this.panel2.TabIndex = 3;
            // 
            // imageSeuillee
            // 
            this.imageSeuillee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageSeuillee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageSeuillee.Location = new System.Drawing.Point(0, 0);
            this.imageSeuillee.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.imageSeuillee.Name = "imageSeuillee";
            this.imageSeuillee.Size = new System.Drawing.Size(600, 615);
            this.imageSeuillee.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageSeuillee.TabIndex = 1;
            this.imageSeuillee.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(0, 0);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // logCam
            // 
            this.logCam.BackColor = System.Drawing.Color.White;
            this.logCam.Location = new System.Drawing.Point(18, 61);
            this.logCam.Multiline = true;
            this.logCam.Name = "logCam";
            this.logCam.Size = new System.Drawing.Size(293, 157);
            this.logCam.TabIndex = 9;
            // 
            // connectToServer
            // 
            this.connectToServer.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.connectToServer.Location = new System.Drawing.Point(999, 61);
            this.connectToServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.connectToServer.Name = "connectToServer";
            this.connectToServer.Size = new System.Drawing.Size(248, 157);
            this.connectToServer.TabIndex = 13;
            this.connectToServer.Text = "Connexion serveur";
            this.connectToServer.UseVisualStyleBackColor = false;
            this.connectToServer.Click += new System.EventHandler(this.connectToServer_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 28);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Caméra";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(334, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "TCP/IP";
            // 
            // logTCP
            // 
            this.logTCP.BackColor = System.Drawing.Color.White;
            this.logTCP.Location = new System.Drawing.Point(338, 61);
            this.logTCP.Multiline = true;
            this.logTCP.Name = "logTCP";
            this.logTCP.Size = new System.Drawing.Size(280, 157);
            this.logTCP.TabIndex = 15;
            // 
            // camStatus
            // 
            this.camStatus.AutoSize = true;
            this.camStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.camStatus.ForeColor = System.Drawing.Color.Red;
            this.camStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.camStatus.Location = new System.Drawing.Point(205, 28);
            this.camStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.camStatus.Name = "camStatus";
            this.camStatus.Size = new System.Drawing.Size(106, 20);
            this.camStatus.TabIndex = 17;
            this.camStatus.Text = "Déconnecté";
            this.camStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tcpStatus
            // 
            this.tcpStatus.AutoSize = true;
            this.tcpStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcpStatus.ForeColor = System.Drawing.Color.Red;
            this.tcpStatus.Location = new System.Drawing.Point(512, 28);
            this.tcpStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tcpStatus.Name = "tcpStatus";
            this.tcpStatus.Size = new System.Drawing.Size(106, 20);
            this.tcpStatus.TabIndex = 18;
            this.tcpStatus.Text = "Déconnecté";
            this.tcpStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // arduinoStatus
            // 
            this.arduinoStatus.AutoSize = true;
            this.arduinoStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arduinoStatus.ForeColor = System.Drawing.Color.Red;
            this.arduinoStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.arduinoStatus.Location = new System.Drawing.Point(834, 28);
            this.arduinoStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.arduinoStatus.Name = "arduinoStatus";
            this.arduinoStatus.Size = new System.Drawing.Size(106, 20);
            this.arduinoStatus.TabIndex = 21;
            this.arduinoStatus.Text = "Déconnecté";
            this.arduinoStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(643, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "Arduino";
            // 
            // logSerial
            // 
            this.logSerial.BackColor = System.Drawing.Color.White;
            this.logSerial.Location = new System.Drawing.Point(647, 61);
            this.logSerial.Multiline = true;
            this.logSerial.Name = "logSerial";
            this.logSerial.Size = new System.Drawing.Size(293, 157);
            this.logSerial.TabIndex = 19;
            // 
            // PC1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 864);
            this.Controls.Add(this.arduinoStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.logSerial);
            this.Controls.Add(this.tcpStatus);
            this.Controls.Add(this.camStatus);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.logTCP);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.connectToServer);
            this.Controls.Add(this.logCam);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PC1";
            this.Text = "Sender";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Close);
            ((System.ComponentModel.ISupportInitialize)(this.imageDepart)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageSeuillee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ouvrirImage;
        private System.Windows.Forms.PictureBox imageDepart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox imageSeuillee;
        private System.Windows.Forms.TextBox logCam;
        private System.Windows.Forms.Button connectToServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox logTCP;
        private System.Windows.Forms.Label camStatus;
        private System.Windows.Forms.Label tcpStatus;
        private System.Windows.Forms.Label arduinoStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox logSerial;
    }
}

