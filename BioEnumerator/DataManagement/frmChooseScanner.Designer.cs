namespace BioEnumerator.DataManagement
{
    partial class frmChooseScanner
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
            this.label2 = new System.Windows.Forms.Label();
            this.clbScannerList = new System.Windows.Forms.CheckedListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(405, 49);
            this.label2.TabIndex = 1;
            this.label2.Text = "Choose scanner modules to load. More modules require more time to load. Some modu" +
    "les might conflict. It is recommended to use only the modules which are required" +
    ".\r\n";
            // 
            // clbScannerList
            // 
            this.clbScannerList.CheckOnClick = true;
            this.clbScannerList.Location = new System.Drawing.Point(15, 61);
            this.clbScannerList.Name = "clbScannerList";
            this.clbScannerList.Size = new System.Drawing.Size(402, 184);
            this.clbScannerList.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(312, 359);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 51);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(201, 359);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(105, 51);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "Ok";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // frmChooseScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 422);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.clbScannerList);
            this.Controls.Add(this.label2);
            this.Name = "frmChooseScanner";
            this.Text = "Choose Scanner";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox clbScannerList;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}