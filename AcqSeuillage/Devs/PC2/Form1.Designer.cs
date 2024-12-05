namespace PC2
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
            this.pictureBoxReceived = new System.Windows.Forms.PictureBox();
            this.panel = new System.Windows.Forms.Panel();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.logbox = new System.Windows.Forms.TextBox();
            this.labelStatusTitle = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReceived)).BeginInit();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxReceived
            // 
            this.pictureBoxReceived.Location = new System.Drawing.Point(17, 23);
            this.pictureBoxReceived.Name = "pictureBoxReceived";
            this.pictureBoxReceived.Size = new System.Drawing.Size(1035, 560);
            this.pictureBoxReceived.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxReceived.TabIndex = 0;
            this.pictureBoxReceived.TabStop = false;
            // 
            // panel
            // 
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.pictureBoxReceived);
            this.panel.Location = new System.Drawing.Point(47, 207);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1073, 600);
            this.panel.TabIndex = 1;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(47, 43);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(242, 89);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Start server";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // logbox
            // 
            this.logbox.Location = new System.Drawing.Point(638, 24);
            this.logbox.Multiline = true;
            this.logbox.Name = "logbox";
            this.logbox.Size = new System.Drawing.Size(461, 152);
            this.logbox.TabIndex = 3;
            // 
            // labelStatusTitle
            // 
            this.labelStatusTitle.AutoSize = true;
            this.labelStatusTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatusTitle.Location = new System.Drawing.Point(326, 67);
            this.labelStatusTitle.Name = "labelStatusTitle";
            this.labelStatusTitle.Size = new System.Drawing.Size(59, 20);
            this.labelStatusTitle.TabIndex = 6;
            this.labelStatusTitle.Text = "Statut";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStatus.Location = new System.Drawing.Point(482, 67);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(113, 20);
            this.labelStatus.TabIndex = 7;
            this.labelStatus.Text = "Not connected";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 841);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelStatusTitle);
            this.Controls.Add(this.logbox);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.panel);
            this.Name = "Form1";
            this.Text = "Receiver";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxReceived)).EndInit();
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxReceived;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox logbox;
        private System.Windows.Forms.Label labelStatusTitle;
        private System.Windows.Forms.Label labelStatus;
    }
}

