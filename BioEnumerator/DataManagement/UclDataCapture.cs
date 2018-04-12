using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioEnumerator.APIServer.APIObjs;
using BioEnumerator.CommonUtils;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;
using BioEnumerator.DataAccessManager.Service.Contract;
using BioEnumerator.DataManagement.PopUps;
using BioEnumerator.GSetting;
using DPFP;
using DPFP.Capture;
using Transitions;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataManagement
{
    public partial class UclDataCapture : UserControl, DPFP.Capture.EventHandler
    {


        #region Vars

        #region FingerPrint

        public delegate void OnTemplateEventHandler(Template template);
        public event UclCaptureFinger.OnTemplateEventHandler OnTemplate;
        //public Action<User, StationInfo, bool> UserLoginAction;
        private DPFP.Processing.Enrollment _enroller;
        private DPFP.Verification.Verification _verificator;
        private Capture _capturer;
        private int _sampleTaken;
        private Button _btnFingerClicked;
        private int _enrollFinger;
        private int _totalFingerEnrolled;
        private int _totalFingerStored;
        private string _enrollFingerName;
        private bool _fingerStoredForDb;
        private Template[] _capturedFingerTemplates = new Template[10];

        #endregion

        #region Personal Info

        private bool _bDrawMouseSelection = false;

        private BackgroundWorker _bgWorker;
        private List<State> _states;
        private List<LocalArea> _localAreas;
        private List<MaritalStatus> _maritalStatus;
        private List<NameAndValueObject> _marital;
        private List<Occupation> _occupations;

        // Zooming
        private bool _bZoomed = false;
        private double _fZoomValue = 1.0;

        #endregion

        #endregion


        public UclDataCapture()
        {
            InitializeComponent();
            EventHooks();
            InitDropDowns();
        }


        private void EventHooks()
        {
            FingerButtonInitialization();

            //pnlPersonalInfo.Hide();
            pnlFingerPrint.Visible = false;
            pnlPreviewInfo.Visible = false;
            //pnlFingerPrint.Hide();
            //pnlPreviewInfo.Hide();

            Load += UclDataCapture_Load;
            btnCaptureInfo.Click += btnCaptureInfo_Click;
            btnBackPersonal.Click += btnBackPersonal_Click;
            btnCaptureFingerInfo.Click += btnCaptureFingerInfo_Click;
            btnBackFingerPrint.Click += btnBackFingerPrint_Click;
            btnSubmit.Click += btnSubmit_Click;
            cmbState.SelectedIndexChanged += cmbState_SelectedIndexChanged;
            cmbLocalArea.Enabled = false;
            //btnCaptureImage.Click += btnCaptureImage_Click;
            picCapture.Click += picCapture_Click;
            dtpDateOfBirth.Format = DateTimePickerFormat.Custom;
            dtpDateOfBirth.CustomFormat = @"yyyy/MM/dd";

            
            //cmbLocationType.SelectedIndexChanged += cmbLocationType_SelectedIndexChanged;
            //btnSave.Click += btnSave_Click;
            //btnReset.Click += btnReset_Click;
        }

        void UclDataCapture_Load(object sender, EventArgs e)
        {
            
            //Init();
            //Start();	
        }
        


        public void SetPanel()
        {

            var mX = (pnlBody.Width - pnlPersonalInfo.Width) / 2;
            pnlPersonalInfo.Location = new Point(mX, pnlPersonalInfo.Location.Y);
            pnlFingerPrint.Location = new Point(mX, pnlFingerPrint.Location.Y);
            pnlPreviewInfo.Location = new Point(mX, pnlPreviewInfo.Location.Y);


            var mX1 = (pnlPersonalInfo.Width - pnlPersonalFrame.Width) / 2;
            pnlPersonalFrame.Location = new Point(mX1, pnlPersonalFrame.Location.Y);
            pnlFingerFrame.Location = new Point(mX1, pnlFingerFrame.Location.Y);
            pnlFingerFrame.Location = new Point(mX1, pnlFingerFrame.Location.Y);

        }


        #region Personal Info & Picture

        void picCapture_Click(object sender, EventArgs e)
        {
            var frmCap = new frmCaptureImage();
            frmCap.ShowDialog();

            if (Utils.CaptureImage != null)
            {
                picImage.Image = new Bitmap(Utils.CaptureImage);
                picImage.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            
        }
        void btnCaptureInfo_Click(object sender, EventArgs e)
        {

            //FingerButtonInitialization();
            try
            {
                if (!ValidatePageOne())
                    return;

                CacheInfo();
                
                pnlPersonalInfo.Visible = false;
                pnlFingerPrint.Visible = true;
                
                
                
                //FingerButtonInitialization();

            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                MessageBox.Show(@"Error Occurred! Please try again later", @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            //var fingers = Utils.CapturedTemplates;

            //if (!ValidateSubmission())
            //    return;

            //CacheInfo();
            //var frmCap = new frmCaptureImage();
            //if (frmCap.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}


            //bool flag;
            //var frm = new frmModuleDisplay(2, out flag);
            //if (!flag) { return; }


            //var ck = Utils.BeneficiaryRegObj;

            //frm.Show();
            //frm.Location = new Point(Width, 0);
            //var t = new Transition(new TransitionType_EaseInEaseOut(150));
            //t.add(frm, "Left", 0);
            //t.run();
        }

        void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {

            bdsLocalAreas.DataSource = new List<LocalArea>();
            cmbLocalArea.Enabled = false;

            if (cmbState.SelectedValue == null) { return; }
            if (int.Parse(cmbState.SelectedValue.ToString()) < 1) return;
            var filteredLocalAreas =
                _localAreas.FindAll(m => m.StateId == int.Parse(cmbState.SelectedValue.ToString()));

            filteredLocalAreas.Insert(0, new LocalArea { LocalAreaId = 0, Name = @"-- Please Select --" });
            bdsLocalAreas.DataSource = new List<LocalArea>();
            bdsLocalAreas.DataSource = filteredLocalAreas;
            cmbLocalArea.Enabled = true;
            //cmbBusinessLocation.SelectedValue = Utils.StationInfo.BusinessLocationId;
        }
        private bool ValidatePageOne()
        {
            try
            {

                if (txtSurname.Text.Trim().Length < 3)
                {
                    MessageBox.Show(@"Surname cannot be empty", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    txtSurname.Focus();
                    return false;
                }
                if (txtFirstName.Text.Trim().Length < 3)
                {
                    MessageBox.Show(@"First name cannot be empty", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    txtFirstName.Focus();
                    return false;
                }

                // Validate D.O.B
                if (dtpDateOfBirth.Value.Date == DateTime.Now.Date)
                {
                    MessageBox.Show(@"Please select valid date of birth", @"Process Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                // Validate Gender
                if (!rbbFemale.Checked && !rbbMale.Checked)
                {
                    MessageBox.Show(@"Please select valid gender", @"Process Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                if (cmbOccupation.SelectedValue == null)
                {
                    MessageBox.Show(@"Please select valid occupation", @"Process Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    cmbOccupation.Focus();
                    return false;
                }

                if (cmbOccupation.SelectedValue != null && int.Parse(cmbOccupation.SelectedValue.ToString()) < 1)
                {
                    MessageBox.Show(@"Please select valid occupation", @"Process Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    cmbOccupation.Focus();
                    return false;
                }

                if (cmbMaritalStatus.SelectedValue == null)
                {
                    MessageBox.Show(@"Please select valid marital status", @"Process Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    cmbMaritalStatus.Focus();
                    return false;
                }

                if (cmbMaritalStatus.SelectedValue != null && int.Parse(cmbMaritalStatus.SelectedValue.ToString()) < 1)
                {
                    MessageBox.Show(@"Please select valid marital status", @"Process Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    cmbMaritalStatus.Focus();
                    return false;
                }

                if (txtHomeAddress.Text.Trim().Length < 5)
                {
                    MessageBox.Show(@"Residential address cannot be empty and must be more than 5 character long", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    txtHomeAddress.Focus();
                    return false;
                }

                if (cmbState.SelectedValue == null)
                {
                    MessageBox.Show(@"Please select valid state of origin", @"Process Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    cmbState.Focus();
                    return false;
                }

                if (cmbState.SelectedValue != null && int.Parse(cmbState.SelectedValue.ToString()) < 1)
                {
                    MessageBox.Show(@"Please select valid state of origin", @"Process Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    cmbState.Focus();
                    return false;
                }

                if (cmbLocalArea.SelectedValue == null)
                {
                    MessageBox.Show(@"Please select valid LGA", @"Process Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    cmbLocalArea.Focus();
                    return false;
                }

                if (cmbLocalArea.SelectedValue != null && int.Parse(cmbLocalArea.SelectedValue.ToString()) < 1)
                {
                    MessageBox.Show(@"Please select valid LGA", @"Process Validation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    cmbLocalArea.Focus();
                    return false;
                }


                //if (txtMobileNo.Text.Trim().Equals(string.Empty))
                //{
                //    MessageBox.Show(@"Mobile number cannot be empty", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //    txtMobileNo.Focus();
                //    return false;
                //}

                //if (!GSMHelper.ValidateMobileNumber(this.txtMobileNumber.Text))
                //{
                //    MessageBox.Show("Invalid Station Mobile Number", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //    return false;
                //}

                //if (txtMobileNo.Text.Length >= 7 && txtMobileNo.Text.Length <= 15)
                //    return true;
                //    MessageBox.Show(@"Invalid Mobile Number", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //    txtMobileNo.Focus();
                if (picImage.Image != null)
                    return true;
                MessageBox.Show(@"Please take the image of the person", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                MessageBox.Show(@"Unable to validate your inputs. Please check all inputs and try again later", @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }
        private void CacheInfo()
        {

            Utils.BeneficiaryRegObj.Surname = txtSurname.Text.Trim();
            Utils.BeneficiaryRegObj.FirstName = txtFirstName.Text.Trim();
            Utils.BeneficiaryRegObj.Othernames = txtOtherName.Text.Trim();
            Utils.BeneficiaryRegObj.DateOfBirth = dtpDateOfBirth.Value.ToString("yyyy/MM/dd");
            Utils.BeneficiaryRegObj.Sex = rbbMale.Checked ? 1 : 2;
            Utils.BeneficiaryRegObj.OccupationId = int.Parse(cmbOccupation.SelectedValue.ToString());
            Utils.BeneficiaryRegObj.MaritalStatus = int.Parse(cmbOccupation.SelectedValue.ToString());
            Utils.BeneficiaryRegObj.ResidentialAddress = txtHomeAddress.Text.Trim();
            Utils.BeneficiaryRegObj.OfficeAddress = txtOfficeAddress.Text.Trim();
            Utils.BeneficiaryRegObj.StateId = int.Parse(cmbState.SelectedValue.ToString());
            Utils.BeneficiaryRegObj.LocalAreaId = int.Parse(cmbLocalArea.SelectedValue.ToString());
            Utils.BeneficiaryRegObj.MobileNumber = txtMobileNo.Text.Trim();
            Utils.BeneficiaryRegObj.Image = new Bitmap(Utils.CaptureImage);

            #region Preview

            Utils.PreviewBeneficiaryRegObj.Surname = txtSurname.Text.Trim();
            Utils.PreviewBeneficiaryRegObj.FirstName = txtFirstName.Text.Trim();
            Utils.PreviewBeneficiaryRegObj.Othernames = txtOtherName.Text.Trim();
            Utils.PreviewBeneficiaryRegObj.DateOfBirth = dtpDateOfBirth.Value.ToString("yyyy/MM/dd");
            Utils.PreviewBeneficiaryRegObj.Sex = rbbMale.Checked ? rbbMale.Text : rbbFemale.Text;
            Utils.PreviewBeneficiaryRegObj.Occupation = cmbOccupation.Text;
            Utils.PreviewBeneficiaryRegObj.MaritalStatus = cmbMaritalStatus.Text;
            Utils.PreviewBeneficiaryRegObj.ResidentialAddress = txtHomeAddress.Text.Trim();
            Utils.PreviewBeneficiaryRegObj.OfficeAddress = txtOfficeAddress.Text.Trim();
            Utils.PreviewBeneficiaryRegObj.State = cmbState.Text;
            Utils.PreviewBeneficiaryRegObj.LocalArea = cmbLocalArea.Text;
            Utils.PreviewBeneficiaryRegObj.MobileNumber = txtMobileNo.Text.Trim();

            #endregion


        }


        #region Background Stuffs
        private void InitDropDowns()
        {
            _bgWorker = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            _bgWorker.DoWork += SpinnerWatcherOnDoWork;
            _bgWorker.RunWorkerCompleted += SpinnerWatcherOnRunWorkerCompleted;
            _bgWorker.RunWorkerAsync();
        }
        private void SpinnerWatcherOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            try
            {

                if (_states == null || !_states.Any())
                {
                    MessageBox.Show(@"State list is empty!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_localAreas == null || !_localAreas.Any())
                {
                    MessageBox.Show(@"Local govt list is empty!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_occupations == null || !_occupations.Any())
                {
                    MessageBox.Show(@"Occupation list is empty!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_marital == null || !_marital.Any())
                {
                    MessageBox.Show(@"Marital Status list is empty!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _states.Insert(0, new State { StateId = 0, Name = @"-- Please Select --" });
                bdsState.DataSource = new List<State>();
                bdsState.DataSource = _states;
                cmbState.SelectedIndex = 0;

                _occupations.Insert(0, new Occupation { OccupationId = 0, Name = @"-- Please Select --" });
                bdsOccupation.DataSource = new List<Occupation>();
                bdsOccupation.DataSource = _occupations;
                cmbOccupation.SelectedIndex = 0;

                _marital.Insert(0, new NameAndValueObject { Id = 0, Name = @"-- Please Select --" });
                bdsMaritalStatus.DataSource = new List<NameAndValueObject>();
                bdsMaritalStatus.DataSource = _marital;
                cmbMaritalStatus.SelectedIndex = 0;

                //_maritalStatus.Insert(0, new MaritalStatus { MaritalStatusId = 0, Name = @"-- Please Select --" });
                //bdsMaritalStatus.DataSource = new List<MaritalStatus>();
                //bdsMaritalStatus.DataSource = _maritalStatus;
                //cmbMaritalStatus.SelectedIndex = 0;

                //RetrieveCacheInfo();

            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }

        }
        private void SpinnerWatcherOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {

            _states = ServiceProvider.Instance().GetStateService().GetStates().OrderBy(m => m.Name).ToList();
            _localAreas = ServiceProvider.Instance().GetLocalAreaService().GetLocalAreas().OrderBy(m => m.Name).ToList();
            _occupations = new List<Occupation>
            {
                new Occupation{ OccupationId = 1, Name = "Teacher"},
                new Occupation{ OccupationId = 2, Name = "Civil Servant"},
                new Occupation{ OccupationId = 3, Name = "Computer Operator"}
            }.ToList();

            _marital = new List<NameAndValueObject>
            {
                new NameAndValueObject{ Id = 1, Name = "Single"},
                new NameAndValueObject{ Id = 2, Name = "Married"},
                new NameAndValueObject{ Id = 3, Name = "Divorced"}
            }.ToList();

            //_maritalStatus = new List<MaritalStatus>
            //{
            //    new MaritalStatus{ MaritalStatusId = 1, Name = "Single"},
            //    new MaritalStatus{ MaritalStatusId = 2, Name = "Married"},
            //    new MaritalStatus{ MaritalStatusId = 3, Name = "Divorced"}
            //}.ToList();
        }

        #endregion


        #endregion


        #region FingerPrint Info

        void btnBackPersonal_Click(object sender, EventArgs e)
        {
            pnlPersonalInfo.Visible = true;
            pnlFingerPrint.Visible = false;
        }
        void btnCaptureFingerInfo_Click(object sender, EventArgs e)
        {
            var validate = ValidateFinger();
            if (validate)
            {
                MessageBox.Show(@"No Finger Capture for the enrollment! Please capture at least two fingers", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            pnlFingerPrint.Visible = false;
            pnlPreviewInfo.Visible = true;
            Utils.BeneficiaryRegObj.CapturedFingerPrintTemplate = Utils.CapturedTemplates;

            Preview();

        }


        private bool ValidateFinger()
        {
            try
            {
                var check = Array.TrueForAll(Utils.CapturedTemplates, x => x == null);
                return check;
            }
            catch (Exception ex)
            {
                return true;
            }
        }


        #region FingerPrint Enrollment Initialization

        protected virtual void Init()
        {
            try
            {

                ResetSignalSample();

                _capturer = new Capture();				// Create a capture operation.

                if (null != _capturer)
                {
                    _capturer.EventHandler = this; // Subscribe for capturing events.
                }
                else
                {
                    SetPrompt("Can't initiate capture operation!");
                }

                _enroller = new DPFP.Processing.Enrollment();			// Create an enrollment.
                //_verificator = new DPFP.Verification.Verification();		// Create a fingerprint template verificator
                UpdateStatus();

            }
            catch
            {
                MessageBox.Show(@"Can't initiate capture operation!", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected void Start()
        {
            if (null != _capturer)
            {
                try
                {
                    _capturer.StartCapture();
                    SetPrompt("Using the fingerprint reader, scan your fingerprint.");
                }
                catch
                {
                    SetPrompt("Can't initiate capture!");
                }
            }
        }

        protected void Stop()
        {
            if (null != _capturer)
            {
                try
                {
                    _capturer.StopCapture();
                }
                catch
                {
                    SetPrompt("Can't terminate capture!");
                }
            }
        }


        #endregion

        #region Capture Events

        public void OnComplete(object capture, string readerSerialNumber, Sample sample)
        {
            MakeReport("The fingerprint sample was captured.");
            SetPrompt("Scan the same fingerprint again.");
            Process(sample);
        }

        public void OnFingerGone(object capture, string readerSerialNumber)
        {
            MakeReport("The finger was removed from the fingerprint reader.");
        }

        public void OnFingerTouch(object capture, string readerSerialNumber)
        {
            MakeReport("The fingerprint reader was touched.");
        }

        public void OnReaderConnect(object capture, string readerSerialNumber)
        {
            MakeReport("The fingerprint reader was connected.");
        }

        public void OnReaderDisconnect(object capture, string readerSerialNumber)
        {
            MakeReport("The fingerprint reader was disconnected.");
        }

        public void OnSampleQuality(object capture, string readerSerialNumber, CaptureFeedback captureFeedback)
        {
            MakeReport(captureFeedback == DPFP.Capture.CaptureFeedback.Good
                ? "The quality of the fingerprint sample is good."
                : "The quality of the fingerprint sample is poor.");
        }

        #endregion

        #region Enrollment Operation

        private void ResetSignalSample()
        {
            _sampleTaken = 0;

            lblSampleOne.BackColor = Color.Transparent;
            lblSampleTwo.BackColor = Color.Transparent;
            lblSampleThree.BackColor = Color.Transparent;
            lblSampleFour.BackColor = Color.Transparent;
        }
        private void SignalSample(int sample)
        {
            switch (sample)
            {
                case 1:
                    lblSampleOne.BackColor = Color.FromArgb(42, 94, 114);
                    break;
                case 2:
                    lblSampleTwo.BackColor = Color.FromArgb(42, 94, 114);
                    break;
                case 3:
                    lblSampleThree.BackColor = Color.FromArgb(42, 94, 114);
                    break;
                case 4:
                    lblSampleFour.BackColor = Color.FromArgb(42, 94, 114);
                    break;

            }
        }

        protected virtual void Process(Sample sample)
        {
            // Draw fingerprint sample image.
            DrawPicture(ConvertSampleToBitmap(sample));

            // Process the sample and create a feature set for the enrollment purpose.
            var features = ExtractFeatures(sample, DPFP.Processing.DataPurpose.Enrollment);

            // Check quality of the sample and add to enroller if it's good
            if (features != null) try
                {
                    MakeReport("The fingerprint feature set was created.");
                    _enroller.AddFeatures(features);		// Add feature set to template.
                }
                finally
                {
                    UpdateStatus();

                    // Check if template has been created.
                    switch (_enroller.TemplateStatus)
                    {
                        case DPFP.Processing.Enrollment.Status.Ready:	// report success and stop capturing

                            _btnFingerClicked.BackColor = Color.Green;
                            #region Capture Templates Here

                            var fingerprintData = new MemoryStream();
                            var templateCreated = _enroller.Template;
                            templateCreated.Serialize(fingerprintData);
                            Utils.CapturedTemplates[_enrollFinger - 1] = _enroller.Template;
                            _totalFingerEnrolled++;
                            // Add the created template to List of Templates that is going to Database
                            //Utils.BeneficiaryRegObj.FingerPrintTemplate.Add(fingerprintData.ToArray());



                            #endregion


                            //OnTemplate(_enroller.Template);
                            //SetPrompt("Click Close, and then click Fingerprint Verification.");
                            Stop();
                            break;

                        case DPFP.Processing.Enrollment.Status.Failed:	// report failure and restart capturing
                            _enroller.Clear();
                            Stop();
                            UpdateStatus();
                            //OnTemplate(null);
                            Start();
                            break;
                    }
                }


        }
        protected Bitmap ConvertSampleToBitmap(Sample sample)
        {
            var convertor = new SampleConversion();	// Create a sample convertor.
            Bitmap bitmap = null;												            // TODO: the size doesn't matter
            convertor.ConvertToPicture(sample, ref bitmap);									// TODO: return bitmap as a result
            return bitmap;
        }

        protected void SetStatus(string status)
        {
            Invoke(new Function(delegate()
            {
                StatusLine.Text = status;
            }));
        }

        protected void SetPrompt(string prompt)
        {
            Invoke(new Function(delegate()
            {
                Prompt.Text = prompt;
            }));
        }

        protected void MakeReport(string message)
        {
            //if (!StatusText.IsHandleCreated)
            //{
            //    this.CreateHandle();
            //}
            Invoke(new Function(() => StatusText.AppendText(message + "\r\n")));
        }


        private void UpdateStatus()
        {
            // Show number of samples needed.
            SetStatus(String.Format("Fingerprint samples needed: {0}", _enroller.FeaturesNeeded));
        }

        private void UpdateStatus(int far)
        {
            // Show "False accept rate" value
            SetStatus(String.Format("False Accept Rate (FAR) = {0}", far));
        }

        protected FeatureSet ExtractFeatures(Sample sample, DPFP.Processing.DataPurpose purpose)
        {
            var extractor = new DPFP.Processing.FeatureExtraction();	// Create a feature extractor
            var feedback = DPFP.Capture.CaptureFeedback.None;
            var features = new FeatureSet();
            extractor.CreateFeatureSet(sample, purpose, ref feedback, ref features);			// TODO: return features as a result?

            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }


        private void DrawPicture(Bitmap bitmap)
        {

            _sampleTaken++;
            SignalSample(_sampleTaken);

            Invoke(new Function(delegate()
            {
                picFingerImage.Image = new Bitmap(bitmap, picFingerImage.Size);	// fit the image into the picture box
            }));
        }


        #endregion

        private void FingerButtonInitialization()
        {

            #region Left Hand
            btnLeftPinky.Click += btnLeftPinky_Click;
            btnLeftPinky.FlatStyle = FlatStyle.Flat;
            btnLeftPinky.FlatAppearance.BorderSize = 0;

            btnLeftIndex.Click += btnLeftPinky_Click;
            btnLeftIndex.FlatStyle = FlatStyle.Flat;
            btnLeftIndex.FlatAppearance.BorderSize = 0;

            btnLeftMiddle.Click += btnLeftPinky_Click;
            btnLeftMiddle.FlatStyle = FlatStyle.Flat;
            btnLeftMiddle.FlatAppearance.BorderSize = 0;

            btnLeftRing.Click += btnLeftPinky_Click;
            btnLeftRing.FlatStyle = FlatStyle.Flat;
            btnLeftRing.FlatAppearance.BorderSize = 0;

            btnLeftThumb.Click += btnLeftPinky_Click;
            btnLeftThumb.FlatStyle = FlatStyle.Flat;
            btnLeftThumb.FlatAppearance.BorderSize = 0;

            #endregion

            #region Right Hand
            btnRightPinky.Click += btnLeftPinky_Click;
            btnRightPinky.FlatStyle = FlatStyle.Flat;
            btnRightPinky.FlatAppearance.BorderSize = 0;

            btnRightIndex.Click += btnLeftPinky_Click;
            btnRightIndex.FlatStyle = FlatStyle.Flat;
            btnRightIndex.FlatAppearance.BorderSize = 0;

            btnRightMiddle.Click += btnLeftPinky_Click;
            btnRightMiddle.FlatStyle = FlatStyle.Flat;
            btnRightMiddle.FlatAppearance.BorderSize = 0;

            btnRightRing.Click += btnLeftPinky_Click;
            btnRightRing.FlatStyle = FlatStyle.Flat;
            btnRightRing.FlatAppearance.BorderSize = 0;

            btnRightThumb.Click += btnLeftPinky_Click;
            btnRightThumb.FlatStyle = FlatStyle.Flat;
            btnRightThumb.FlatAppearance.BorderSize = 0;

            #endregion

        }

        #region Fingers Capturing Events

        void btnLeftPinky_Click(object sender, EventArgs e)
        {

            var btn = (Button)sender;
            _btnFingerClicked = btn;
            if (_sampleTaken == 4)
            {
                Stop();
            }

            Init();
            PreparedDeleteFingers(btn);
            if (_sampleTaken == 0 && !FingerEnrolled(_enrollFinger))
            {
                Start();
            }

            DisEnroll(_enrollFinger, _enrollFingerName, btn);
            ResetSignalSample();

        }


        private bool FingerEnrolled(int finger)
        {
            if (Utils.CapturedTemplates[finger - 1] == null)
            {
                return false;
            }

            return true;
        }

        private void PreparedDeleteFingers(Button btn)
        {
            #region Assigning Fingers Index

            switch (btn.Name)
            {
                case "btnRightThumb":
                    _enrollFinger = 1;
                    _enrollFingerName = "Right Thumb";
                    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    break;
                case "btnRightIndex":
                    _enrollFinger = 2;
                    _enrollFingerName = "Right Index";
                    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    break;
                case "btnRightMiddle":
                    _enrollFinger = 3;
                    _enrollFingerName = "Right Middle";
                    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    break;
                case "btnRightRing":
                    _enrollFinger = 4;
                    _enrollFingerName = "Right Ring";
                    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    break;
                case "btnRightPinky":
                    _enrollFinger = 5;
                    _enrollFingerName = "Right Little";
                    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    break;
                case "btnLeftThumb":
                    _enrollFinger = 6;
                    _enrollFingerName = "Left Thumb";
                    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    break;
                case "btnLeftIndex":
                    _enrollFinger = 7;
                    _enrollFingerName = "Left Index";
                    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    break;
                case "btnLeftMiddle":
                    _enrollFinger = 8;
                    _enrollFingerName = "Left Middle";
                    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    break;
                case "btnLeftRing":
                    _enrollFinger = 9;
                    _enrollFingerName = "Left Ring";
                    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    break;
                case "btnLeftPinky":
                    _enrollFinger = 10;
                    _enrollFingerName = "Left Little";
                    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    break;

            }

            #endregion
        }

        private void DisEnroll(int finger, string btnName, Button btn)
        {

            if (Utils.CapturedTemplates[finger - 1] == null)
            {
                return;
            }

            if (
                MessageBox.Show(@"Are you sure want to delete " + btnName + @" fingerprint?", @"Delete Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            Utils.CapturedTemplates[finger - 1] = null;
            btn.BackColor = Color.White;
            //if (_totalFingerEnrolled > 0)
            //{
            //    _totalFingerEnrolled--;
            //}
            //if (Utils.CapturedTemplates[finger - 1] == null)
            //{
            //    return true;
            //}

            //return false;
        }
        #endregion


        #endregion

        #region Preview Info

        void btnBackFingerPrint_Click(object sender, EventArgs e)
        {
            pnlFingerPrint.Visible = true;
            pnlPreviewInfo.Visible = false;
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateSubmission()) { return; }


            var retVal = ServiceProvider.Instance().GetBeneficiaryService().AddBeneciary(Utils.BeneficiaryRegObj);
            if (!retVal.IsSuccessful)
            {
                MessageBox.Show(@"Error: " + (string.IsNullOrEmpty(retVal.Message.FriendlyMessage) ? " Process Failed! Please try again later" : retVal.Message.FriendlyMessage), @"Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation);
                return;
            }

            MessageBox.Show(@"Process Completed! Data was saved successfully", @"Process Completed", MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
            Reset();
        }



        private void Preview()
        {
            picCaptureImage.Image = new Bitmap(Utils.CaptureImage);
            picCaptureImage.SizeMode = PictureBoxSizeMode.StretchImage;

            lblSurname.Text = Utils.PreviewBeneficiaryRegObj.Surname;
            lblFirstName.Text = Utils.PreviewBeneficiaryRegObj.FirstName;
            lblOtherName.Text = Utils.PreviewBeneficiaryRegObj.Othernames;
            lblGender.Text = Utils.PreviewBeneficiaryRegObj.Sex;
            lblDateOfBirth.Text = Utils.PreviewBeneficiaryRegObj.DateOfBirth;
            lblMaritalStatus.Text = Utils.PreviewBeneficiaryRegObj.MaritalStatus;
            lblHomeAddress.Text = Utils.PreviewBeneficiaryRegObj.ResidentialAddress;
            lblOfficeAddress.Text = Utils.PreviewBeneficiaryRegObj.OfficeAddress;
            lblState.Text = Utils.PreviewBeneficiaryRegObj.State;
            lblLga.Text = Utils.PreviewBeneficiaryRegObj.LocalArea;
            lblMobileNo.Text = Utils.PreviewBeneficiaryRegObj.MobileNumber;
            lblOccupation.Text = Utils.PreviewBeneficiaryRegObj.Occupation;
        }
        private bool ValidateSubmission()
        {
            try
            {
                if (Utils.BeneficiaryRegObj.Image == null)
                {
                    MessageBox.Show(@"No picture capture for the registrant!",
                            @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false; ;
                }

                if (CustomHelper.ValidateArray(Utils.BeneficiaryRegObj.CapturedFingerPrintTemplate))
                {
                    MessageBox.Show(@"No fingerprint capture for the registrant!",
                            @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false; ;
                }

                return true;

                // Utils.BeneficiaryRegObj.CapturedFingerPrintTemplate
            }
            catch (Exception)
            {
                MessageBox.Show(@"Error Occurred! Please try again later", @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void Reset()
        {

            //EventHooks();

            cmbState.SelectedIndex = 0;
            cmbOccupation.SelectedIndex = 0;
            cmbMaritalStatus.SelectedIndex = 0;

            if (cmbLocalArea.Enabled)
            {
                cmbLocalArea.Enabled = false;
                cmbLocalArea.SelectedIndex = -1;

            }

            Stop();
            //FingerButtonInitialization();
            Utils.PreviewBeneficiaryRegObj = new PreviewBeneficiaryRegObj();
            Utils.BeneficiaryRegObj = new BeneficiaryRegObj
            {
                FingerPrintTemplate = new List<byte[]>(),
                CapturedFingerPrintTemplate = new Template[10]
            };
            Utils.CapturedTemplates = new Template[10];

            StatusText.Text = "";
            Prompt.Text = "";
            picImage.Image = null;
            picFingerImage.Image = null;
            txtMobileNo.Text = "";
            rbbMale.Checked = false;
            rbbFemale.Checked = false;
            txtOfficeAddress.Text = "";
            txtFirstName.Text = "";
            txtOtherName.Text = "";
            txtHomeAddress.Text = "";
            txtOfficeAddress.Text = "";
            txtSurname.Text = "";
            dtpDateOfBirth.Value = DateTime.Now;

            pnlFingerPrint.Visible = false;
            pnlPreviewInfo.Visible = false;
            pnlPersonalInfo.Visible = true;

            btnLeftPinky.BackColor = Color.White;
            btnLeftRing.BackColor = Color.White;
            btnLeftMiddle.BackColor = Color.White;
            btnLeftIndex.BackColor = Color.White;
            btnLeftThumb.BackColor = Color.White;

            btnRightPinky.BackColor = Color.White;
            btnRightRing.BackColor = Color.White;
            btnRightMiddle.BackColor = Color.White;
            btnRightIndex.BackColor = Color.White;
            btnRightThumb.BackColor = Color.White;

            lblSampleOne.BackColor = Color.Transparent;
            lblSampleTwo.BackColor = Color.Transparent;
            lblSampleThree.BackColor = Color.Transparent;
            lblSampleFour.BackColor = Color.Transparent;
        }

        #endregion


    }
}
