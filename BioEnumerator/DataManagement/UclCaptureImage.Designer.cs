namespace BioEnumerator.DataManagement
{
    partial class UclCaptureImage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label PromptLabel;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlItemBody = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlFrame = new System.Windows.Forms.Panel();
            this.btnCaptureImage = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cameraControl = new Camera_NET.CameraControl();
            this.cmbCameraList = new MetroFramework.Controls.MetroComboBox();
            this.cmbResolutionList = new MetroFramework.Controls.MetroComboBox();
            this.btnStartCamera = new System.Windows.Forms.Button();
            PromptLabel = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.pnlBody.SuspendLayout();
            this.pnlItemBody.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlFrame.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.pnlItemBody);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(1241, 817);
            this.pnlBody.TabIndex = 0;
            // 
            // pnlItemBody
            // 
            this.pnlItemBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlItemBody.Controls.Add(this.panel1);
            this.pnlItemBody.Controls.Add(this.panel2);
            this.pnlItemBody.Location = new System.Drawing.Point(110, 61);
            this.pnlItemBody.Name = "pnlItemBody";
            this.pnlItemBody.Size = new System.Drawing.Size(1043, 592);
            this.pnlItemBody.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pnlFrame);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1041, 556);
            this.panel1.TabIndex = 1;
            // 
            // pnlFrame
            // 
            this.pnlFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFrame.Controls.Add(this.cmbResolutionList);
            this.pnlFrame.Controls.Add(this.cmbCameraList);
            this.pnlFrame.Controls.Add(label3);
            this.pnlFrame.Controls.Add(this.panel3);
            this.pnlFrame.Controls.Add(label2);
            this.pnlFrame.Controls.Add(PromptLabel);
            this.pnlFrame.Controls.Add(this.btnStartCamera);
            this.pnlFrame.Controls.Add(this.btnCaptureImage);
            this.pnlFrame.Location = new System.Drawing.Point(25, 14);
            this.pnlFrame.Name = "pnlFrame";
            this.pnlFrame.Size = new System.Drawing.Size(990, 522);
            this.pnlFrame.TabIndex = 0;
            // 
            // btnCaptureImage
            // 
            this.btnCaptureImage.Image = global::BioEnumerator.Properties.Resources.ic_save_small;
            this.btnCaptureImage.Location = new System.Drawing.Point(733, 453);
            this.btnCaptureImage.Name = "btnCaptureImage";
            this.btnCaptureImage.Size = new System.Drawing.Size(234, 51);
            this.btnCaptureImage.TabIndex = 15;
            this.btnCaptureImage.Text = "Submit";
            this.btnCaptureImage.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnCaptureImage.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(62)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1041, 34);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(6, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Image Capturing";
            // 
            // PromptLabel
            // 
            PromptLabel.AutoSize = true;
            PromptLabel.Location = new System.Drawing.Point(740, 11);
            PromptLabel.Name = "PromptLabel";
            PromptLabel.Size = new System.Drawing.Size(73, 13);
            PromptLabel.TabIndex = 103;
            PromptLabel.Text = "Camera Face:";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.cameraControl);
            this.panel3.Location = new System.Drawing.Point(505, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(462, 369);
            this.panel3.TabIndex = 104;
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
            // cmbCameraList
            // 
            this.cmbCameraList.BackColor = System.Drawing.Color.GhostWhite;
            this.cmbCameraList.DisplayMember = "Name";
            this.cmbCameraList.FormattingEnabled = true;
            this.cmbCameraList.ItemHeight = 23;
            this.cmbCameraList.Location = new System.Drawing.Point(18, 28);
            this.cmbCameraList.Name = "cmbCameraList";
            this.cmbCameraList.Size = new System.Drawing.Size(423, 29);
            this.cmbCameraList.TabIndex = 105;
            this.cmbCameraList.UseSelectable = true;
            this.cmbCameraList.ValueMember = "OccupationId";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(15, 12);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(93, 13);
            label2.TabIndex = 103;
            label2.Text = "Camera Selection:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(15, 78);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(107, 13);
            label3.TabIndex = 103;
            label3.Text = "Resolution Selection:";
            // 
            // cmbResolutionList
            // 
            this.cmbResolutionList.BackColor = System.Drawing.Color.GhostWhite;
            this.cmbResolutionList.DisplayMember = "Name";
            this.cmbResolutionList.FormattingEnabled = true;
            this.cmbResolutionList.ItemHeight = 23;
            this.cmbResolutionList.Location = new System.Drawing.Point(18, 94);
            this.cmbResolutionList.Name = "cmbResolutionList";
            this.cmbResolutionList.Size = new System.Drawing.Size(423, 29);
            this.cmbResolutionList.TabIndex = 105;
            this.cmbResolutionList.UseSelectable = true;
            this.cmbResolutionList.ValueMember = "OccupationId";
            // 
            // btnStartCamera
            // 
            this.btnStartCamera.Image = global::BioEnumerator.Properties.Resources.ic_save_small;
            this.btnStartCamera.Location = new System.Drawing.Point(569, 453);
            this.btnStartCamera.Name = "btnStartCamera";
            this.btnStartCamera.Size = new System.Drawing.Size(158, 51);
            this.btnStartCamera.TabIndex = 15;
            this.btnStartCamera.Text = "Stop Camera";
            this.btnStartCamera.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnStartCamera.UseVisualStyleBackColor = true;
            // 
            // UclCaptureImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBody);
            this.Name = "UclCaptureImage";
            this.Size = new System.Drawing.Size(1241, 817);
            this.pnlBody.ResumeLayout(false);
            this.pnlItemBody.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlFrame.ResumeLayout(false);
            this.pnlFrame.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlItemBody;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlFrame;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCaptureImage;
        private System.Windows.Forms.Panel panel3;
        private Camera_NET.CameraControl cameraControl;
        private MetroFramework.Controls.MetroComboBox cmbCameraList;
        private MetroFramework.Controls.MetroComboBox cmbResolutionList;
        private System.Windows.Forms.Button btnStartCamera;
    }
}
