namespace BioEnumerator.DataManagement.PopUps
{
    partial class frmEditDataPopUp
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
            this.components = new System.ComponentModel.Container();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnCloseWind = new System.Windows.Forms.Button();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlItemBody = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlFrame = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbMaritalStatus = new MetroFramework.Controls.MetroComboBox();
            this.bdsMaritalStatus = new System.Windows.Forms.BindingSource(this.components);
            this.cmbOccupation = new MetroFramework.Controls.MetroComboBox();
            this.bdsOccupation = new System.Windows.Forms.BindingSource(this.components);
            this.cmbLocalArea = new MetroFramework.Controls.MetroComboBox();
            this.bdsLocalAreas = new System.Windows.Forms.BindingSource(this.components);
            this.cmbState = new MetroFramework.Controls.MetroComboBox();
            this.bdsState = new System.Windows.Forms.BindingSource(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbbFemale = new System.Windows.Forms.RadioButton();
            this.rbbMale = new System.Windows.Forms.RadioButton();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtOfficeAddress = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtHomeAddress = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtOtherName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHeader = new System.Windows.Forms.Button();
            this.pnlBody.SuspendLayout();
            this.pnlItemBody.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMaritalStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsOccupation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsLocalAreas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsState)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.lblHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(202)))));
            this.lblHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(3, 383);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(593, 38);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Capture Image";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCloseWind
            // 
            this.btnCloseWind.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCloseWind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCloseWind.FlatAppearance.BorderSize = 0;
            this.btnCloseWind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseWind.Image = global::BioEnumerator.Properties.Resources.ic_cancel_small;
            this.btnCloseWind.Location = new System.Drawing.Point(1017, 1);
            this.btnCloseWind.Name = "btnCloseWind";
            this.btnCloseWind.Size = new System.Drawing.Size(41, 34);
            this.btnCloseWind.TabIndex = 2;
            this.btnCloseWind.UseVisualStyleBackColor = false;
            // 
            // pnlBody
            // 
            this.pnlBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBody.Controls.Add(this.btnCloseWind);
            this.pnlBody.Controls.Add(this.pnlItemBody);
            this.pnlBody.Controls.Add(this.btnHeader);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(1064, 585);
            this.pnlBody.TabIndex = 4;
            // 
            // pnlItemBody
            // 
            this.pnlItemBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlItemBody.Controls.Add(this.panel1);
            this.pnlItemBody.Controls.Add(this.panel2);
            this.pnlItemBody.Location = new System.Drawing.Point(9, 48);
            this.pnlItemBody.Name = "pnlItemBody";
            this.pnlItemBody.Size = new System.Drawing.Size(1043, 515);
            this.pnlItemBody.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pnlFrame);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1041, 485);
            this.panel1.TabIndex = 1;
            // 
            // pnlFrame
            // 
            this.pnlFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFrame.Controls.Add(this.lblHeader);
            this.pnlFrame.Controls.Add(this.label22);
            this.pnlFrame.Controls.Add(this.label20);
            this.pnlFrame.Controls.Add(this.label16);
            this.pnlFrame.Controls.Add(this.label12);
            this.pnlFrame.Controls.Add(this.label21);
            this.pnlFrame.Controls.Add(this.label19);
            this.pnlFrame.Controls.Add(this.label11);
            this.pnlFrame.Controls.Add(this.label15);
            this.pnlFrame.Controls.Add(this.cmbMaritalStatus);
            this.pnlFrame.Controls.Add(this.cmbOccupation);
            this.pnlFrame.Controls.Add(this.cmbLocalArea);
            this.pnlFrame.Controls.Add(this.cmbState);
            this.pnlFrame.Controls.Add(this.label13);
            this.pnlFrame.Controls.Add(this.txtMobileNo);
            this.pnlFrame.Controls.Add(this.label14);
            this.pnlFrame.Controls.Add(this.label7);
            this.pnlFrame.Controls.Add(this.label8);
            this.pnlFrame.Controls.Add(this.groupBox1);
            this.pnlFrame.Controls.Add(this.btnUpdate);
            this.pnlFrame.Controls.Add(this.dtpDateOfBirth);
            this.pnlFrame.Controls.Add(this.label2);
            this.pnlFrame.Controls.Add(this.label3);
            this.pnlFrame.Controls.Add(this.label10);
            this.pnlFrame.Controls.Add(this.label17);
            this.pnlFrame.Controls.Add(this.txtOfficeAddress);
            this.pnlFrame.Controls.Add(this.label9);
            this.pnlFrame.Controls.Add(this.txtHomeAddress);
            this.pnlFrame.Controls.Add(this.label18);
            this.pnlFrame.Controls.Add(this.label5);
            this.pnlFrame.Controls.Add(this.label23);
            this.pnlFrame.Controls.Add(this.txtOtherName);
            this.pnlFrame.Controls.Add(this.label4);
            this.pnlFrame.Controls.Add(this.txtFirstName);
            this.pnlFrame.Controls.Add(this.label24);
            this.pnlFrame.Controls.Add(this.label25);
            this.pnlFrame.Controls.Add(this.txtSurname);
            this.pnlFrame.Controls.Add(this.label26);
            this.pnlFrame.Controls.Add(this.label6);
            this.pnlFrame.Location = new System.Drawing.Point(25, 15);
            this.pnlFrame.Name = "pnlFrame";
            this.pnlFrame.Size = new System.Drawing.Size(990, 450);
            this.pnlFrame.TabIndex = 0;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Gainsboro;
            this.label22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label22.Location = new System.Drawing.Point(48, 294);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(104, 29);
            this.label22.TabIndex = 98;
            this.label22.Text = "Marital Status";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Gainsboro;
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label20.Location = new System.Drawing.Point(47, 253);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(104, 29);
            this.label20.TabIndex = 98;
            this.label20.Text = "Occupation";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Gainsboro;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label16.Location = new System.Drawing.Point(533, 253);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(104, 29);
            this.label16.TabIndex = 98;
            this.label16.Text = "LGA";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Gainsboro;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label12.Location = new System.Drawing.Point(535, 203);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 29);
            this.label12.TabIndex = 98;
            this.label12.Text = "State";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Gainsboro;
            this.label21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Image = global::BioEnumerator.Properties.Resources.ic_user_16;
            this.label21.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label21.Location = new System.Drawing.Point(21, 294);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(27, 29);
            this.label21.TabIndex = 97;
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Gainsboro;
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label19.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Image = global::BioEnumerator.Properties.Resources.icc_corporate;
            this.label19.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label19.Location = new System.Drawing.Point(20, 253);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(27, 29);
            this.label19.TabIndex = 97;
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Gainsboro;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Image = global::BioEnumerator.Properties.Resources.ic_city_location;
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label11.Location = new System.Drawing.Point(506, 253);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 29);
            this.label11.TabIndex = 97;
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Gainsboro;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Image = global::BioEnumerator.Properties.Resources.ic_city_location;
            this.label15.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label15.Location = new System.Drawing.Point(508, 203);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 29);
            this.label15.TabIndex = 97;
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbMaritalStatus
            // 
            this.cmbMaritalStatus.BackColor = System.Drawing.Color.GhostWhite;
            this.cmbMaritalStatus.DataSource = this.bdsMaritalStatus;
            this.cmbMaritalStatus.DisplayMember = "Name";
            this.cmbMaritalStatus.FormattingEnabled = true;
            this.cmbMaritalStatus.ItemHeight = 23;
            this.cmbMaritalStatus.Location = new System.Drawing.Point(153, 294);
            this.cmbMaritalStatus.Name = "cmbMaritalStatus";
            this.cmbMaritalStatus.Size = new System.Drawing.Size(326, 29);
            this.cmbMaritalStatus.TabIndex = 8;
            this.cmbMaritalStatus.UseSelectable = true;
            this.cmbMaritalStatus.ValueMember = "Id";
            // 
            // bdsMaritalStatus
            // 
            this.bdsMaritalStatus.DataSource = typeof(XPLUG.WINDOWTOOLS.NameAndValueObject);
            // 
            // cmbOccupation
            // 
            this.cmbOccupation.BackColor = System.Drawing.Color.GhostWhite;
            this.cmbOccupation.DataSource = this.bdsOccupation;
            this.cmbOccupation.DisplayMember = "Name";
            this.cmbOccupation.FormattingEnabled = true;
            this.cmbOccupation.ItemHeight = 23;
            this.cmbOccupation.Location = new System.Drawing.Point(152, 253);
            this.cmbOccupation.Name = "cmbOccupation";
            this.cmbOccupation.Size = new System.Drawing.Size(327, 29);
            this.cmbOccupation.TabIndex = 7;
            this.cmbOccupation.UseSelectable = true;
            this.cmbOccupation.ValueMember = "OccupationId";
            // 
            // bdsOccupation
            // 
            this.bdsOccupation.DataSource = typeof(BioEnumerator.DataAccessManager.DataContract.Occupation);
            // 
            // cmbLocalArea
            // 
            this.cmbLocalArea.BackColor = System.Drawing.Color.GhostWhite;
            this.cmbLocalArea.DataSource = this.bdsLocalAreas;
            this.cmbLocalArea.DisplayMember = "Name";
            this.cmbLocalArea.FormattingEnabled = true;
            this.cmbLocalArea.ItemHeight = 23;
            this.cmbLocalArea.Location = new System.Drawing.Point(639, 253);
            this.cmbLocalArea.Name = "cmbLocalArea";
            this.cmbLocalArea.Size = new System.Drawing.Size(327, 29);
            this.cmbLocalArea.TabIndex = 12;
            this.cmbLocalArea.UseSelectable = true;
            this.cmbLocalArea.ValueMember = "LocalAreaId";
            // 
            // bdsLocalAreas
            // 
            this.bdsLocalAreas.DataSource = typeof(BioEnumerator.DataAccessManager.DataContract.LocalArea);
            // 
            // cmbState
            // 
            this.cmbState.BackColor = System.Drawing.Color.GhostWhite;
            this.cmbState.DataSource = this.bdsState;
            this.cmbState.DisplayMember = "Name";
            this.cmbState.FormattingEnabled = true;
            this.cmbState.ItemHeight = 23;
            this.cmbState.Location = new System.Drawing.Point(640, 203);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(327, 29);
            this.cmbState.TabIndex = 11;
            this.cmbState.UseSelectable = true;
            this.cmbState.ValueMember = "StateId";
            // 
            // bdsState
            // 
            this.bdsState.DataSource = typeof(BioEnumerator.DataAccessManager.DataContract.State);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Gainsboro;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label13.Location = new System.Drawing.Point(534, 294);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 29);
            this.label13.TabIndex = 88;
            this.label13.Text = "Mobile Number";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.BackColor = System.Drawing.Color.GhostWhite;
            this.txtMobileNo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo.ForeColor = System.Drawing.Color.Black;
            this.txtMobileNo.Location = new System.Drawing.Point(640, 294);
            this.txtMobileNo.MaxLength = 15;
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(327, 29);
            this.txtMobileNo.TabIndex = 13;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Gainsboro;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Image = global::BioEnumerator.Properties.Resources.ic_phone;
            this.label14.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label14.Location = new System.Drawing.Point(507, 294);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(27, 29);
            this.label14.TabIndex = 87;
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Gainsboro;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(47, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 29);
            this.label7.TabIndex = 85;
            this.label7.Text = "Sex";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Gainsboro;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Image = global::BioEnumerator.Properties.Resources.icc_Referral;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Location = new System.Drawing.Point(20, 208);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 29);
            this.label8.TabIndex = 84;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbbFemale);
            this.groupBox1.Controls.Add(this.rbbMale);
            this.groupBox1.Location = new System.Drawing.Point(152, 200);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 37);
            this.groupBox1.TabIndex = 83;
            this.groupBox1.TabStop = false;
            // 
            // rbbFemale
            // 
            this.rbbFemale.AutoSize = true;
            this.rbbFemale.Location = new System.Drawing.Point(248, 15);
            this.rbbFemale.Name = "rbbFemale";
            this.rbbFemale.Size = new System.Drawing.Size(59, 17);
            this.rbbFemale.TabIndex = 6;
            this.rbbFemale.TabStop = true;
            this.rbbFemale.Text = "Female";
            this.rbbFemale.UseVisualStyleBackColor = true;
            // 
            // rbbMale
            // 
            this.rbbMale.AutoSize = true;
            this.rbbMale.Checked = true;
            this.rbbMale.Location = new System.Drawing.Point(19, 15);
            this.rbbMale.Name = "rbbMale";
            this.rbbMale.Size = new System.Drawing.Size(48, 17);
            this.rbbMale.TabIndex = 5;
            this.rbbMale.TabStop = true;
            this.rbbMale.Text = "Male";
            this.rbbMale.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Image = global::BioEnumerator.Properties.Resources.ic_save_small;
            this.btnUpdate.Location = new System.Drawing.Point(731, 377);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(234, 51);
            this.btnUpdate.TabIndex = 14;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateOfBirth.Location = new System.Drawing.Point(152, 162);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Size = new System.Drawing.Size(327, 27);
            this.dtpDateOfBirth.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(48, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 29);
            this.label2.TabIndex = 79;
            this.label2.Text = "Date Of Birth";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Image = global::BioEnumerator.Properties.Resources.icc_date_birth;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(21, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 29);
            this.label3.TabIndex = 78;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Gainsboro;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Location = new System.Drawing.Point(534, 121);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 29);
            this.label10.TabIndex = 77;
            this.label10.Text = "Office Address";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Gainsboro;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label17.Location = new System.Drawing.Point(534, 39);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(104, 29);
            this.label17.TabIndex = 77;
            this.label17.Text = "Home Address";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOfficeAddress
            // 
            this.txtOfficeAddress.BackColor = System.Drawing.Color.GhostWhite;
            this.txtOfficeAddress.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOfficeAddress.ForeColor = System.Drawing.Color.Black;
            this.txtOfficeAddress.Location = new System.Drawing.Point(639, 121);
            this.txtOfficeAddress.Multiline = true;
            this.txtOfficeAddress.Name = "txtOfficeAddress";
            this.txtOfficeAddress.Size = new System.Drawing.Size(326, 68);
            this.txtOfficeAddress.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Gainsboro;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Image = global::BioEnumerator.Properties.Resources.ic_city_location;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Location = new System.Drawing.Point(507, 121);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 29);
            this.label9.TabIndex = 76;
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHomeAddress
            // 
            this.txtHomeAddress.BackColor = System.Drawing.Color.GhostWhite;
            this.txtHomeAddress.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHomeAddress.ForeColor = System.Drawing.Color.Black;
            this.txtHomeAddress.Location = new System.Drawing.Point(639, 39);
            this.txtHomeAddress.Multiline = true;
            this.txtHomeAddress.Name = "txtHomeAddress";
            this.txtHomeAddress.Size = new System.Drawing.Size(326, 69);
            this.txtHomeAddress.TabIndex = 9;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Gainsboro;
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Image = global::BioEnumerator.Properties.Resources.ic_city_location;
            this.label18.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label18.Location = new System.Drawing.Point(507, 39);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(27, 29);
            this.label18.TabIndex = 76;
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Gainsboro;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(48, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 29);
            this.label5.TabIndex = 75;
            this.label5.Text = "Other Names";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.Gainsboro;
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label23.Location = new System.Drawing.Point(48, 81);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(103, 29);
            this.label23.TabIndex = 75;
            this.label23.Text = "First Name";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOtherName
            // 
            this.txtOtherName.BackColor = System.Drawing.Color.GhostWhite;
            this.txtOtherName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtherName.ForeColor = System.Drawing.Color.Black;
            this.txtOtherName.Location = new System.Drawing.Point(152, 121);
            this.txtOtherName.Name = "txtOtherName";
            this.txtOtherName.Size = new System.Drawing.Size(327, 27);
            this.txtOtherName.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Gainsboro;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Image = global::BioEnumerator.Properties.Resources.icc_person;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Location = new System.Drawing.Point(21, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 29);
            this.label4.TabIndex = 74;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFirstName
            // 
            this.txtFirstName.BackColor = System.Drawing.Color.GhostWhite;
            this.txtFirstName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.ForeColor = System.Drawing.Color.Black;
            this.txtFirstName.Location = new System.Drawing.Point(152, 81);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(327, 27);
            this.txtFirstName.TabIndex = 2;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Gainsboro;
            this.label24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label24.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Image = global::BioEnumerator.Properties.Resources.icc_person;
            this.label24.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label24.Location = new System.Drawing.Point(21, 81);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(27, 29);
            this.label24.TabIndex = 74;
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.Gainsboro;
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label25.Location = new System.Drawing.Point(48, 39);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(103, 29);
            this.label25.TabIndex = 73;
            this.label25.Text = "Surname";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSurname
            // 
            this.txtSurname.BackColor = System.Drawing.Color.GhostWhite;
            this.txtSurname.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSurname.ForeColor = System.Drawing.Color.Black;
            this.txtSurname.Location = new System.Drawing.Point(152, 39);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(327, 27);
            this.txtSurname.TabIndex = 1;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.Gainsboro;
            this.label26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label26.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Image = global::BioEnumerator.Properties.Resources.icc_person;
            this.label26.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label26.Location = new System.Drawing.Point(21, 39);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(27, 29);
            this.label26.TabIndex = 72;
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(19, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 15);
            this.label6.TabIndex = 67;
            this.label6.Text = "PERSONAL INFORMATION";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(62)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1041, 28);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(6, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Personal Data Capture";
            // 
            // btnHeader
            // 
            this.btnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHeader.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHeader.Location = new System.Drawing.Point(0, 0);
            this.btnHeader.Name = "btnHeader";
            this.btnHeader.Size = new System.Drawing.Size(1062, 37);
            this.btnHeader.TabIndex = 99;
            this.btnHeader.Text = "Beneficiary Information";
            this.btnHeader.UseVisualStyleBackColor = true;
            // 
            // frmEditDataPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(245)))), ((int)(((byte)(204)))));
            this.ClientSize = new System.Drawing.Size(1064, 585);
            this.Controls.Add(this.pnlBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEditDataPopUp";
            this.Text = "frmCaptureImage";
            this.pnlBody.ResumeLayout(false);
            this.pnlItemBody.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlFrame.ResumeLayout(false);
            this.pnlFrame.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdsMaritalStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsOccupation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsLocalAreas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsState)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnCloseWind;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlItemBody;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlFrame;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private MetroFramework.Controls.MetroComboBox cmbMaritalStatus;
        private MetroFramework.Controls.MetroComboBox cmbOccupation;
        private MetroFramework.Controls.MetroComboBox cmbLocalArea;
        private MetroFramework.Controls.MetroComboBox cmbState;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbbFemale;
        private System.Windows.Forms.RadioButton rbbMale;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtOfficeAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtHomeAddress;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtOtherName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHeader;
        private System.Windows.Forms.BindingSource bdsState;
        private System.Windows.Forms.BindingSource bdsOccupation;
        private System.Windows.Forms.BindingSource bdsMaritalStatus;
        private System.Windows.Forms.BindingSource bdsLocalAreas;
    }
}