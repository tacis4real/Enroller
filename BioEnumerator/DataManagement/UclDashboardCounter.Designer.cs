namespace BioEnumerator.DataManagement
{
    partial class UclDashboardCounter
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblErrorData = new System.Windows.Forms.Label();
            this.lblPendingData = new System.Windows.Forms.Label();
            this.lblUploadedData = new System.Windows.Forms.Label();
            this.lblTotalData = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 110);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.lblErrorData, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblPendingData, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblUploadedData, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTotalData, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(260, 110);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblErrorData
            // 
            this.lblErrorData.AutoSize = true;
            this.lblErrorData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblErrorData.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorData.Location = new System.Drawing.Point(159, 84);
            this.lblErrorData.Name = "lblErrorData";
            this.lblErrorData.Size = new System.Drawing.Size(97, 25);
            this.lblErrorData.TabIndex = 8;
            this.lblErrorData.Text = "0";
            this.lblErrorData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPendingData
            // 
            this.lblPendingData.AutoSize = true;
            this.lblPendingData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPendingData.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingData.Location = new System.Drawing.Point(159, 58);
            this.lblPendingData.Name = "lblPendingData";
            this.lblPendingData.Size = new System.Drawing.Size(97, 25);
            this.lblPendingData.TabIndex = 7;
            this.lblPendingData.Text = "0";
            this.lblPendingData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUploadedData
            // 
            this.lblUploadedData.AutoSize = true;
            this.lblUploadedData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUploadedData.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploadedData.Location = new System.Drawing.Point(159, 31);
            this.lblUploadedData.Name = "lblUploadedData";
            this.lblUploadedData.Size = new System.Drawing.Size(97, 26);
            this.lblUploadedData.TabIndex = 6;
            this.lblUploadedData.Text = "0";
            this.lblUploadedData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalData
            // 
            this.lblTotalData.AutoSize = true;
            this.lblTotalData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalData.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalData.Location = new System.Drawing.Point(159, 1);
            this.lblTotalData.Name = "lblTotalData";
            this.lblTotalData.Size = new System.Drawing.Size(97, 29);
            this.lblTotalData.TabIndex = 5;
            this.lblTotalData.Text = "0";
            this.lblTotalData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "All Registered Beneficiary";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Uploaded Beneficiary";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Pending Uploads";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(4, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Data With Error";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 15000;
            // 
            // UclDashboardCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UclDashboardCounter";
            this.Size = new System.Drawing.Size(260, 110);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblErrorData;
        private System.Windows.Forms.Label lblPendingData;
        private System.Windows.Forms.Label lblUploadedData;
        private System.Windows.Forms.Label lblTotalData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer;
    }
}
