namespace BioEnumerator.DataManagement.PopUps
{
    partial class frmCaptureImage
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
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label PromptLabel;
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnCloseWind = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picSnapShot = new System.Windows.Forms.PictureBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnSnapShot = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cameraControl = new Camera_NET.CameraControl();
            this.cmbResolutionList = new MetroFramework.Controls.MetroComboBox();
            this.cmbCameraList = new MetroFramework.Controls.MetroComboBox();
            this.pnlBody = new System.Windows.Forms.Panel();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            PromptLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSnapShot)).BeginInit();
            this.panel3.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(14, 65);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(107, 13);
            label3.TabIndex = 106;
            label3.Text = "Resolution Selection:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(14, 9);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(93, 13);
            label2.TabIndex = 107;
            label2.Text = "Camera Selection:";
            // 
            // PromptLabel
            // 
            PromptLabel.AutoSize = true;
            PromptLabel.Location = new System.Drawing.Point(644, 9);
            PromptLabel.Name = "PromptLabel";
            PromptLabel.Size = new System.Drawing.Size(73, 13);
            PromptLabel.TabIndex = 110;
            PromptLabel.Text = "Camera Face:";
            // 
            // lblHeader
            // 
            this.lblHeader.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.lblHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(202)))));
            this.lblHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(919, 38);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Capture Image";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCloseWind
            // 
            this.btnCloseWind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(202)))));
            this.btnCloseWind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCloseWind.FlatAppearance.BorderSize = 0;
            this.btnCloseWind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseWind.Image = global::BioEnumerator.Properties.Resources.ic_cancel_small;
            this.btnCloseWind.Location = new System.Drawing.Point(874, 2);
            this.btnCloseWind.Name = "btnCloseWind";
            this.btnCloseWind.Size = new System.Drawing.Size(41, 34);
            this.btnCloseWind.TabIndex = 2;
            this.btnCloseWind.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.btnSnapShot);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(PromptLabel);
            this.panel1.Controls.Add(this.cmbResolutionList);
            this.panel1.Controls.Add(this.cmbCameraList);
            this.panel1.Controls.Add(label3);
            this.panel1.Controls.Add(label2);
            this.panel1.Location = new System.Drawing.Point(3, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(913, 460);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.picSnapShot);
            this.panel2.Location = new System.Drawing.Point(17, 126);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(374, 297);
            this.panel2.TabIndex = 114;
            // 
            // picSnapShot
            // 
            this.picSnapShot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picSnapShot.Location = new System.Drawing.Point(0, 0);
            this.picSnapShot.Name = "picSnapShot";
            this.picSnapShot.Size = new System.Drawing.Size(372, 295);
            this.picSnapShot.TabIndex = 112;
            this.picSnapShot.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(734, 399);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(170, 51);
            this.btnOk.TabIndex = 113;
            this.btnOk.Text = "Ok";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnSnapShot
            // 
            this.btnSnapShot.Image = global::BioEnumerator.Properties.Resources.ic_save_small;
            this.btnSnapShot.Location = new System.Drawing.Point(443, 399);
            this.btnSnapShot.Name = "btnSnapShot";
            this.btnSnapShot.Size = new System.Drawing.Size(285, 51);
            this.btnSnapShot.TabIndex = 113;
            this.btnSnapShot.Text = "Snap Shot";
            this.btnSnapShot.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnSnapShot.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.cameraControl);
            this.panel3.Location = new System.Drawing.Point(443, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(462, 369);
            this.panel3.TabIndex = 111;
            // 
            // cameraControl
            // 
            this.cameraControl.DirectShowLogFilepath = "";
            this.cameraControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraControl.Location = new System.Drawing.Point(0, 0);
            this.cameraControl.Name = "cameraControl";
            this.cameraControl.Size = new System.Drawing.Size(460, 367);
            this.cameraControl.TabIndex = 0;
            // 
            // cmbResolutionList
            // 
            this.cmbResolutionList.BackColor = System.Drawing.Color.GhostWhite;
            this.cmbResolutionList.DisplayMember = "Name";
            this.cmbResolutionList.FormattingEnabled = true;
            this.cmbResolutionList.ItemHeight = 23;
            this.cmbResolutionList.Location = new System.Drawing.Point(17, 81);
            this.cmbResolutionList.Name = "cmbResolutionList";
            this.cmbResolutionList.Size = new System.Drawing.Size(374, 29);
            this.cmbResolutionList.TabIndex = 108;
            this.cmbResolutionList.UseSelectable = true;
            this.cmbResolutionList.ValueMember = "OccupationId";
            // 
            // cmbCameraList
            // 
            this.cmbCameraList.BackColor = System.Drawing.Color.GhostWhite;
            this.cmbCameraList.DisplayMember = "Name";
            this.cmbCameraList.FormattingEnabled = true;
            this.cmbCameraList.ItemHeight = 23;
            this.cmbCameraList.Location = new System.Drawing.Point(17, 25);
            this.cmbCameraList.Name = "cmbCameraList";
            this.cmbCameraList.Size = new System.Drawing.Size(374, 29);
            this.cmbCameraList.TabIndex = 109;
            this.cmbCameraList.UseSelectable = true;
            this.cmbCameraList.ValueMember = "OccupationId";
            // 
            // pnlBody
            // 
            this.pnlBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBody.Controls.Add(this.btnCloseWind);
            this.pnlBody.Controls.Add(this.lblHeader);
            this.pnlBody.Controls.Add(this.panel1);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(921, 520);
            this.pnlBody.TabIndex = 4;
            // 
            // frmCaptureImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 520);
            this.Controls.Add(this.pnlBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCaptureImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCaptureImage";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSnapShot)).EndInit();
            this.panel3.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnCloseWind;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlBody;
        private MetroFramework.Controls.MetroComboBox cmbResolutionList;
        private MetroFramework.Controls.MetroComboBox cmbCameraList;
        private System.Windows.Forms.Panel panel3;
        private Camera_NET.CameraControl cameraControl;
        private System.Windows.Forms.PictureBox picSnapShot;
        private System.Windows.Forms.Button btnSnapShot;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel2;
    }
}