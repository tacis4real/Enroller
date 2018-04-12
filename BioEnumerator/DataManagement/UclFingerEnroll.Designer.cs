namespace BioEnumerator.DataManagement
{
    partial class UclFingerEnroll
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
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlItemBody = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlFrame = new System.Windows.Forms.Panel();
            this.EnrollmentControl = new DPFP.Gui.Enrollment.EnrollmentControl();
            this.GroupEvents = new System.Windows.Forms.GroupBox();
            this.ListEvents = new System.Windows.Forms.ListBox();
            this.btnCaptureFinger = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBody.SuspendLayout();
            this.pnlItemBody.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlFrame.SuspendLayout();
            this.GroupEvents.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.pnlItemBody.Location = new System.Drawing.Point(106, 57);
            this.pnlItemBody.Name = "pnlItemBody";
            this.pnlItemBody.Size = new System.Drawing.Size(1043, 592);
            this.pnlItemBody.TabIndex = 3;
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
            this.pnlFrame.Controls.Add(this.EnrollmentControl);
            this.pnlFrame.Controls.Add(this.GroupEvents);
            this.pnlFrame.Controls.Add(this.btnCaptureFinger);
            this.pnlFrame.Location = new System.Drawing.Point(25, 14);
            this.pnlFrame.Name = "pnlFrame";
            this.pnlFrame.Size = new System.Drawing.Size(990, 522);
            this.pnlFrame.TabIndex = 0;
            // 
            // EnrollmentControl
            // 
            this.EnrollmentControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.EnrollmentControl.EnrolledFingerMask = 0;
            this.EnrollmentControl.Location = new System.Drawing.Point(254, 21);
            this.EnrollmentControl.MaxEnrollFingerCount = 10;
            this.EnrollmentControl.Name = "EnrollmentControl";
            this.EnrollmentControl.ReaderSerialNumber = "00000000-0000-0000-0000-000000000000";
            this.EnrollmentControl.Size = new System.Drawing.Size(492, 314);
            this.EnrollmentControl.TabIndex = 105;
            // 
            // GroupEvents
            // 
            this.GroupEvents.Controls.Add(this.ListEvents);
            this.GroupEvents.Location = new System.Drawing.Point(254, 341);
            this.GroupEvents.Name = "GroupEvents";
            this.GroupEvents.Size = new System.Drawing.Size(492, 146);
            this.GroupEvents.TabIndex = 104;
            this.GroupEvents.TabStop = false;
            this.GroupEvents.Text = "Events";
            // 
            // ListEvents
            // 
            this.ListEvents.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ListEvents.FormattingEnabled = true;
            this.ListEvents.Location = new System.Drawing.Point(16, 19);
            this.ListEvents.Name = "ListEvents";
            this.ListEvents.Size = new System.Drawing.Size(459, 108);
            this.ListEvents.TabIndex = 0;
            // 
            // btnCaptureFinger
            // 
            this.btnCaptureFinger.Image = global::BioEnumerator.Properties.Resources.ic_save_small;
            this.btnCaptureFinger.Location = new System.Drawing.Point(800, 455);
            this.btnCaptureFinger.Name = "btnCaptureFinger";
            this.btnCaptureFinger.Size = new System.Drawing.Size(174, 51);
            this.btnCaptureFinger.TabIndex = 14;
            this.btnCaptureFinger.Text = "Save and Continue";
            this.btnCaptureFinger.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnCaptureFinger.UseVisualStyleBackColor = true;
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
            this.label1.Size = new System.Drawing.Size(173, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "FingerPrint Enrollment";
            // 
            // UclFingerEnroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBody);
            this.Name = "UclFingerEnroll";
            this.Size = new System.Drawing.Size(1241, 817);
            this.pnlBody.ResumeLayout(false);
            this.pnlItemBody.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlFrame.ResumeLayout(false);
            this.GroupEvents.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlItemBody;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlFrame;
        private System.Windows.Forms.Button btnCaptureFinger;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private DPFP.Gui.Enrollment.EnrollmentControl EnrollmentControl;
        private System.Windows.Forms.GroupBox GroupEvents;
        private System.Windows.Forms.ListBox ListEvents;
    }
}
