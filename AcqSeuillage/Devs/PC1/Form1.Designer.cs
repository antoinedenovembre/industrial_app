namespace seuilAuto
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ouvrirImage = new System.Windows.Forms.OpenFileDialog();
            this.imageDepart = new System.Windows.Forms.PictureBox();
            this.buttonOuvrir = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imageSeuillee = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.valeurSeuilAuto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_objetA = new System.Windows.Forms.TextBox();
            this.txt_objetB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_info = new System.Windows.Forms.TextBox();
            this.txt_objetC = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.connectToServer = new System.Windows.Forms.Button();
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
            // buttonOuvrir
            // 
            this.buttonOuvrir.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonOuvrir.Location = new System.Drawing.Point(703, 130);
            this.buttonOuvrir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonOuvrir.Name = "buttonOuvrir";
            this.buttonOuvrir.Size = new System.Drawing.Size(197, 50);
            this.buttonOuvrir.TabIndex = 1;
            this.buttonOuvrir.Text = "Start";
            this.buttonOuvrir.UseVisualStyleBackColor = false;
            this.buttonOuvrir.Click += new System.EventHandler(this.buttonStart_Click);
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
            this.panel2.Location = new System.Drawing.Point(715, 231);
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
            // valeurSeuilAuto
            // 
            this.valeurSeuilAuto.Location = new System.Drawing.Point(1208, 133);
            this.valeurSeuilAuto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.valeurSeuilAuto.Name = "valeurSeuilAuto";
            this.valeurSeuilAuto.ReadOnly = true;
            this.valeurSeuilAuto.Size = new System.Drawing.Size(91, 26);
            this.valeurSeuilAuto.TabIndex = 0;
            this.valeurSeuilAuto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.valeurSeuilAuto.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(967, 136);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Valeur seuillage : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(967, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nombre d\'objets A détectés :";
            // 
            // txt_objetA
            // 
            this.txt_objetA.Location = new System.Drawing.Point(1208, 25);
            this.txt_objetA.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_objetA.Name = "txt_objetA";
            this.txt_objetA.ReadOnly = true;
            this.txt_objetA.Size = new System.Drawing.Size(91, 26);
            this.txt_objetA.TabIndex = 6;
            this.txt_objetA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_objetA.Visible = false;
            // 
            // txt_objetB
            // 
            this.txt_objetB.Location = new System.Drawing.Point(1208, 61);
            this.txt_objetB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_objetB.Name = "txt_objetB";
            this.txt_objetB.ReadOnly = true;
            this.txt_objetB.Size = new System.Drawing.Size(91, 26);
            this.txt_objetB.TabIndex = 8;
            this.txt_objetB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_objetB.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(967, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Nombre d\'objets B détectés :";
            // 
            // txt_info
            // 
            this.txt_info.Location = new System.Drawing.Point(18, 23);
            this.txt_info.Multiline = true;
            this.txt_info.Name = "txt_info";
            this.txt_info.Size = new System.Drawing.Size(600, 157);
            this.txt_info.TabIndex = 9;
            // 
            // txt_objetC
            // 
            this.txt_objetC.Location = new System.Drawing.Point(1208, 97);
            this.txt_objetC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_objetC.Name = "txt_objetC";
            this.txt_objetC.ReadOnly = true;
            this.txt_objetC.Size = new System.Drawing.Size(91, 26);
            this.txt_objetC.TabIndex = 12;
            this.txt_objetC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_objetC.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(967, 100);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Nombre d\'objets C détectés :";
            // 
            // connectToServer
            // 
            this.connectToServer.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.connectToServer.Location = new System.Drawing.Point(703, 34);
            this.connectToServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.connectToServer.Name = "connectToServer";
            this.connectToServer.Size = new System.Drawing.Size(197, 50);
            this.connectToServer.TabIndex = 13;
            this.connectToServer.Text = "Connect";
            this.connectToServer.UseVisualStyleBackColor = false;
            this.connectToServer.Click += new System.EventHandler(this.connectToServer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 900);
            this.Controls.Add(this.connectToServer);
            this.Controls.Add(this.txt_objetC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_info);
            this.Controls.Add(this.txt_objetB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_objetA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.valeurSeuilAuto);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonOuvrir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
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
        private System.Windows.Forms.Button buttonOuvrir;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox imageSeuillee;
        private System.Windows.Forms.TextBox valeurSeuilAuto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_objetA;
        private System.Windows.Forms.TextBox txt_objetB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_info;
        private System.Windows.Forms.TextBox txt_objetC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button connectToServer;
    }
}

