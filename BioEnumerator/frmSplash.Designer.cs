namespace BioEnumerator
{
    partial class frmSplash
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
            this.pnlLoader = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pnlLoader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLoader
            // 
            this.pnlLoader.Controls.Add(this.panel1);
            this.pnlLoader.Controls.Add(this.lblProgress);
            this.pnlLoader.Location = new System.Drawing.Point(347, 177);
            this.pnlLoader.Name = "pnlLoader";
            this.pnlLoader.Size = new System.Drawing.Size(451, 276);
            this.pnlLoader.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::BioEnumerator.Properties.Resources.finger_print;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(132, 109);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 127);
            this.panel1.TabIndex = 2;
            // 
            // lblProgress
            // 
            this.lblProgress.Image = global::BioEnumerator.Properties.Resources.ProgressLoader;
            this.lblProgress.Location = new System.Drawing.Point(180, 50);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(89, 53);
            this.lblProgress.TabIndex = 0;
            this.lblProgress.Visible = false;
            // 
            // frmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(245)))), ((int)(((byte)(204)))));
            this.ClientSize = new System.Drawing.Size(1155, 560);
            this.Controls.Add(this.pnlLoader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSplash";
            this.Text = "frmSplash";
            this.pnlLoader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLoader;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Panel panel1;
    }
}