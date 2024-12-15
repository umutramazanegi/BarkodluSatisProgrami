namespace BarkodluSatisProgrami
{
    partial class fLisans
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tLisansNo = new BarkodluSatisProgrami.tNumeric();
            this.lStandart1 = new BarkodluSatisProgrami.lStandart();
            this.lKontrolNo = new BarkodluSatisProgrami.lStandart();
            this.bTamam = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tLisansNo
            // 
            this.tLisansNo.BackColor = System.Drawing.Color.White;
            this.tLisansNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tLisansNo.ForeColor = System.Drawing.Color.Black;
            this.tLisansNo.Location = new System.Drawing.Point(29, 129);
            this.tLisansNo.Name = "tLisansNo";
            this.tLisansNo.Size = new System.Drawing.Size(215, 30);
            this.tLisansNo.TabIndex = 0;
            this.tLisansNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lStandart1
            // 
            this.lStandart1.AutoSize = true;
            this.lStandart1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lStandart1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lStandart1.Location = new System.Drawing.Point(65, 39);
            this.lStandart1.Name = "lStandart1";
            this.lStandart1.Size = new System.Drawing.Size(150, 25);
            this.lStandart1.TabIndex = 1;
            this.lStandart1.Text = "KONTROL NO:";
            // 
            // lKontrolNo
            // 
            this.lKontrolNo.AutoSize = true;
            this.lKontrolNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lKontrolNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lKontrolNo.Location = new System.Drawing.Point(90, 81);
            this.lKontrolNo.Name = "lKontrolNo";
            this.lKontrolNo.Size = new System.Drawing.Size(101, 25);
            this.lKontrolNo.TabIndex = 2;
            this.lKontrolNo.Text = "lStandart2";
            // 
            // bTamam
            // 
            this.bTamam.Location = new System.Drawing.Point(95, 180);
            this.bTamam.Name = "bTamam";
            this.bTamam.Size = new System.Drawing.Size(75, 23);
            this.bTamam.TabIndex = 3;
            this.bTamam.Text = "Tamam";
            this.bTamam.UseVisualStyleBackColor = true;
            this.bTamam.Click += new System.EventHandler(this.bTamam_Click);
            // 
            // fLisans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(270, 215);
            this.Controls.Add(this.bTamam);
            this.Controls.Add(this.lKontrolNo);
            this.Controls.Add(this.lStandart1);
            this.Controls.Add(this.tLisansNo);
            this.Name = "fLisans";
            this.Text = "Linsans İşlemi";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private tNumeric tLisansNo;
        private lStandart lStandart1;
        private System.Windows.Forms.Button bTamam;
        internal lStandart lKontrolNo;
    }
}