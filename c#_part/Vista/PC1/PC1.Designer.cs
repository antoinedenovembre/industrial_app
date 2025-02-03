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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.logTCP = new System.Windows.Forms.TextBox();
            this.camStatus = new System.Windows.Forms.Label();
            this.tcpStatus = new System.Windows.Forms.Label();
            this.arduinoStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.logSerial = new System.Windows.Forms.TextBox();
            this.verdictLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCalibNoir = new System.Windows.Forms.Button();
            this.logCalib = new System.Windows.Forms.TextBox();
            this.lblStatutCalibN = new System.Windows.Forms.Label();
            this.lblStatutCalibB = new System.Windows.Forms.Label();
            this.btnCalibBlanc = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.imageCalibBlanc = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.imageCalibNoir = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageDepart)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageSeuillee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCalibBlanc)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCalibNoir)).BeginInit();
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
            this.panel1.Location = new System.Drawing.Point(8, 217);
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
            this.panel2.Location = new System.Drawing.Point(637, 217);
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
            this.logCam.Location = new System.Drawing.Point(8, 47);
            this.logCam.Multiline = true;
            this.logCam.Name = "logCam";
            this.logCam.Size = new System.Drawing.Size(293, 157);
            this.logCam.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 14);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Caméra";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(324, 14);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "TCP/IP";
            // 
            // logTCP
            // 
            this.logTCP.BackColor = System.Drawing.Color.White;
            this.logTCP.Location = new System.Drawing.Point(328, 47);
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
            this.camStatus.Location = new System.Drawing.Point(195, 14);
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
            this.tcpStatus.Location = new System.Drawing.Point(502, 14);
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
            this.arduinoStatus.Location = new System.Drawing.Point(784, 14);
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
            this.label2.Location = new System.Drawing.Point(633, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "Arduino";
            // 
            // logSerial
            // 
            this.logSerial.BackColor = System.Drawing.Color.White;
            this.logSerial.Location = new System.Drawing.Point(637, 47);
            this.logSerial.Multiline = true;
            this.logSerial.Name = "logSerial";
            this.logSerial.Size = new System.Drawing.Size(253, 157);
            this.logSerial.TabIndex = 19;
            // 
            // verdictLabel
            // 
            this.verdictLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.verdictLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.verdictLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verdictLabel.ForeColor = System.Drawing.Color.White;
            this.verdictLabel.Location = new System.Drawing.Point(1036, 12);
            this.verdictLabel.Name = "verdictLabel";
            this.verdictLabel.Size = new System.Drawing.Size(201, 192);
            this.verdictLabel.TabIndex = 23;
            this.verdictLabel.Text = "N/A";
            this.verdictLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1266, 883);
            this.tabControl1.TabIndex = 24;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.btnCalibNoir);
            this.tabPage2.Controls.Add(this.logCalib);
            this.tabPage2.Controls.Add(this.lblStatutCalibN);
            this.tabPage2.Controls.Add(this.lblStatutCalibB);
            this.tabPage2.Controls.Add(this.btnCalibBlanc);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1258, 850);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Calibration";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCalibNoir
            // 
            this.btnCalibNoir.Enabled = false;
            this.btnCalibNoir.Location = new System.Drawing.Point(12, 106);
            this.btnCalibNoir.Name = "btnCalibNoir";
            this.btnCalibNoir.Size = new System.Drawing.Size(776, 62);
            this.btnCalibNoir.TabIndex = 20;
            this.btnCalibNoir.Text = "Calibration noir";
            this.btnCalibNoir.UseVisualStyleBackColor = true;
            this.btnCalibNoir.Click += new System.EventHandler(this.StartCalibBlack);
            // 
            // logCalib
            // 
            this.logCalib.Location = new System.Drawing.Point(886, 38);
            this.logCalib.Multiline = true;
            this.logCalib.Name = "logCalib";
            this.logCalib.Size = new System.Drawing.Size(341, 163);
            this.logCalib.TabIndex = 19;
            // 
            // lblStatutCalibN
            // 
            this.lblStatutCalibN.AutoSize = true;
            this.lblStatutCalibN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatutCalibN.ForeColor = System.Drawing.Color.Red;
            this.lblStatutCalibN.Location = new System.Drawing.Point(806, 127);
            this.lblStatutCalibN.Name = "lblStatutCalibN";
            this.lblStatutCalibN.Size = new System.Drawing.Size(38, 20);
            this.lblStatutCalibN.TabIndex = 16;
            this.lblStatutCalibN.Text = "N/A";
            // 
            // lblStatutCalibB
            // 
            this.lblStatutCalibB.AutoSize = true;
            this.lblStatutCalibB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatutCalibB.ForeColor = System.Drawing.Color.Red;
            this.lblStatutCalibB.Location = new System.Drawing.Point(806, 59);
            this.lblStatutCalibB.Name = "lblStatutCalibB";
            this.lblStatutCalibB.Size = new System.Drawing.Size(38, 20);
            this.lblStatutCalibB.TabIndex = 15;
            this.lblStatutCalibB.Text = "N/A";
            // 
            // btnCalibBlanc
            // 
            this.btnCalibBlanc.Enabled = false;
            this.btnCalibBlanc.Location = new System.Drawing.Point(12, 38);
            this.btnCalibBlanc.Name = "btnCalibBlanc";
            this.btnCalibBlanc.Size = new System.Drawing.Size(776, 62);
            this.btnCalibBlanc.TabIndex = 14;
            this.btnCalibBlanc.Text = "Calibration blanc";
            this.btnCalibBlanc.UseVisualStyleBackColor = true;
            this.btnCalibBlanc.Click += new System.EventHandler(this.StartCalibWhite);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(648, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(190, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Image de calibration noir :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Image de calibration blanc :";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.verdictLabel);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.arduinoStatus);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.logCam);
            this.tabPage1.Controls.Add(this.logSerial);
            this.tabPage1.Controls.Add(this.logTCP);
            this.tabPage1.Controls.Add(this.tcpStatus);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.camStatus);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1258, 850);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Traitement";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.imageCalibBlanc);
            this.panel3.Location = new System.Drawing.Point(12, 235);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(594, 607);
            this.panel3.TabIndex = 21;
            // 
            // imageCalibBlanc
            // 
            this.imageCalibBlanc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imageCalibBlanc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageCalibBlanc.Cursor = System.Windows.Forms.Cursors.Default;
            this.imageCalibBlanc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageCalibBlanc.ImageLocation = "";
            this.imageCalibBlanc.Location = new System.Drawing.Point(0, 0);
            this.imageCalibBlanc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.imageCalibBlanc.Name = "imageCalibBlanc";
            this.imageCalibBlanc.Size = new System.Drawing.Size(594, 607);
            this.imageCalibBlanc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageCalibBlanc.TabIndex = 0;
            this.imageCalibBlanc.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.imageCalibNoir);
            this.panel4.Location = new System.Drawing.Point(651, 235);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(600, 607);
            this.panel4.TabIndex = 3;
            // 
            // imageCalibNoir
            // 
            this.imageCalibNoir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imageCalibNoir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageCalibNoir.Cursor = System.Windows.Forms.Cursors.Default;
            this.imageCalibNoir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageCalibNoir.ImageLocation = "";
            this.imageCalibNoir.Location = new System.Drawing.Point(0, 0);
            this.imageCalibNoir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.imageCalibNoir.Name = "imageCalibNoir";
            this.imageCalibNoir.Size = new System.Drawing.Size(600, 607);
            this.imageCalibNoir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageCalibNoir.TabIndex = 0;
            this.imageCalibNoir.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(882, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 20);
            this.label4.TabIndex = 22;
            this.label4.Text = "Calibration";
            // 
            // PC1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 895);
            this.Controls.Add(this.tabControl1);
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
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCalibBlanc)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCalibNoir)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ouvrirImage;
        private System.Windows.Forms.PictureBox imageDepart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox imageSeuillee;
        private System.Windows.Forms.TextBox logCam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox logTCP;
        private System.Windows.Forms.Label camStatus;
        private System.Windows.Forms.Label tcpStatus;
        private System.Windows.Forms.Label arduinoStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox logSerial;
        private System.Windows.Forms.Label verdictLabel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnCalibNoir;
        private System.Windows.Forms.TextBox logCalib;
        private System.Windows.Forms.Label lblStatutCalibN;
        private System.Windows.Forms.Label lblStatutCalibB;
        private System.Windows.Forms.Button btnCalibBlanc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox imageCalibNoir;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox imageCalibBlanc;
        private System.Windows.Forms.Label label4;
    }
}

