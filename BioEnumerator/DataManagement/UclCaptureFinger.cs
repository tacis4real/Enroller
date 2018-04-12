using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioEnumerator.CommonUtils;
using BioEnumerator.DataAccessManager.Repository.Helpers;
using BioEnumerator.GSetting;
using DPFP;
using DPFP.Capture;
using Transitions;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataManagement
{
    public partial class UclCaptureFinger : UserControl, DPFP.Capture.EventHandler
    {

        #region Vars
        private BackgroundWorker _bgWorker;
        public delegate void OnTemplateEventHandler(Template template);
        public event OnTemplateEventHandler OnTemplate;
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

        public UclCaptureFinger()
        {
            InitializeComponent();
            EventHooks();
            InitDropDowns();
        }


        private void EventHooks()
        {
            Load += UclCaptureFinger_Load;
            //btnVerify.Click += btnVerify_Click;
            btnCaptureFingerInfo.Click += btnCaptureFingerInfo_Click;
            FingerButtonInitialization();
        }

        
        


        #region Sub Modules


        void btnCaptureFingerInfo_Click(object sender, EventArgs e)
        {
            
            var validate = ValidateFinger();
            if (validate)
            {
                MessageBox.Show(@"No Finger Capture for the enrollment! Please capture at least two fingers", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Utils.BeneficiaryRegObj.CapturedFingerPrintTemplate = Utils.CapturedTemplates;

            //if (!StoredFingerTemplates())
            //{
            //    return;
            //}



            #region Trying Saving Templates in DB

            //var fingerByteLists = EnrollHelper.ExtractFingerTemplates();
            //if (!fingerByteLists.Any())
            //{
            //    MessageBox.Show(@"No Finger Capture for the enrollment! Please capture at least two fingers", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //var temp = Utils.CapturedTemplates;

            //var tempx = new FingerTemplateData
            //{
            //    FingerPrintTemplates = Utils.BeneficiaryRegObj.FingerPrintTemplate
            //};

            //var helper = new BeneficiaryBiometric
            //{
            //    BeneficiaryId = 2,
            //    RightThumbPrintTemplate = null,
            //    RightIndexPrintTemplate = null,
            //    ImageFileName = "",
            //    _Template = JsonConvert.SerializeObject(tempx)
            //};
            //var retId = new BeneficiaryRepository().AddBeneciaryBiometric(helper);
            //if (retId.IsSuccessful)
            //{
            //    MessageBox.Show(@"State list is empty!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            #endregion
            
            bool flag;
            var frm = new frmModuleDisplay(3, out flag);
            if (!flag) { return; }
            frm.Show();
            frm.Location = new Point(Width, 0);
            var t = new Transition(new TransitionType_EaseInEaseOut(150));
            t.add(frm, "Left", 0);
            t.run();
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

        private bool StoredFingerTemplates()
        {
            try
            {

                if (!_fingerStoredForDb || (_totalFingerEnrolled != _totalFingerStored))
                {
                    if (Utils.CapturedTemplates == null)
                    {
                        return false;
                    }

                    var templates = Utils.CapturedTemplates;
                    if (templates == null) return false;
                    var fingerprintData = new MemoryStream();

                    foreach (var template in templates)
                    {
                        if (template != null)
                        {
                            template.Serialize(fingerprintData);
                            Utils.BeneficiaryRegObj.FingerPrintTemplate.Add(fingerprintData.ToArray());
                            _totalFingerStored++;
                        }
                    }
                    if (!Utils.BeneficiaryRegObj.FingerPrintTemplate.Any() ||
                        Utils.BeneficiaryRegObj.FingerPrintTemplate.Count == 0)
                    {
                        return false;
                    }

                    _fingerStoredForDb = true;
                    //return true;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void RetrieveCapturedFingers()
        {
            //var fingerTemplates = Utils.CapturedTemplates;
            //if(fingerTemplates == null) return;

            //if (Array.TrueForAll(Utils.CapturedTemplates, x => x == null)) return;

            //foreach (var fingerTemplate in fingerTemplates)
            //{
            //    if (fingerTemplate != null)
            //    {
            //        switch (RightToLeft)
            //        {
                            
            //        }
            //    }
            //}

            for (int i = 0; i < _capturedFingerTemplates.Length; i++)
            {
                if (_capturedFingerTemplates[i] != null)
                {

                    //case "btnRightThumb":
                    //_enrollFinger = 1;
                    //_enrollFingerName = "Right Thumb";
                    ////DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    //break;
                    //case "btnRightIndex":
                    //    _enrollFinger = 2;
                    //    _enrollFingerName = "Right Index";
                    //    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    //    break;
                    //case "btnRightMiddle":
                    //    _enrollFinger = 3;
                    //    _enrollFingerName = "Right Middle";
                    //    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    //    break;
                    //case "btnRightRing":
                    //    _enrollFinger = 4;
                    //    _enrollFingerName = "Right Ring";
                    //    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    //    break;
                    //case "btnRightPinky":
                    //    _enrollFinger = 5;
                    //     _enrollFingerName = "Right Little";
                    //     //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    //    break;
                    //case "btnLeftThumb":
                    //    _enrollFinger = 6;
                    //    _enrollFingerName = "Left Thumb";
                    //    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    //    break;
                    //case "btnLeftIndex":
                    //    _enrollFinger = 7;
                    //    _enrollFingerName = "Left Index";
                    //    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    //    break;
                    //case "btnLeftMiddle":
                    //    _enrollFinger = 8;
                    //    _enrollFingerName = "Left Middle";
                    //    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    //    break;
                    //case "btnLeftRing":
                    //    _enrollFinger = 9;
                    //    _enrollFingerName = "Left Ring";
                    //    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    //    break;
                    //case "btnLeftPinky":
                    //    _enrollFinger = 10;
                    //    _enrollFingerName = "Left Little";
                    //    //DisEnroll(_enrollFinger, _enrollFingerName, btn);
                    //    break;

                    switch (i)
                    {
                        case 0:
                            btnRightThumb.BackColor = Color.Green;
                            break;
                        case 1:
                            btnRightIndex.BackColor = Color.Green;
                            break;
                        case 2:
                            btnRightMiddle.BackColor = Color.Green;
                            break;
                        case 3:
                            btnRightRing.BackColor = Color.Green;
                            break;
                        case 4:
                            btnRightPinky.BackColor = Color.Green;
                            break;
                        case 5:
                            btnLeftThumb.BackColor = Color.Green;
                            break;
                        case 6:
                            btnLeftIndex.BackColor = Color.Green;
                            break;
                        case 7:
                            btnLeftMiddle.BackColor = Color.Green;
                            break;
                        case 8:
                            btnLeftRing.BackColor = Color.Green;
                            break;
                        case 9:
                            btnLeftPinky.BackColor = Color.Green;
                            break;
                    }
                }
            }
        }

        #endregion



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
        

        #region Form Event Handlers:


        void UclCaptureFinger_Load(object sender, EventArgs e)
        {
            //Utils.CapturedTemplates = new Template[10];
            //_capturedFingerTemplates = new Template[10];
        }


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

        //private void CaptureForm_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    Stop();
        //}
        #endregion

        

        public void SetPanel()
        {

            var mX = (pnlBody.Width - pnlItemBody.Width) / 2;
            pnlItemBody.Location = new Point(mX, pnlItemBody.Location.Y);

            var mX1 = (pnlItemBody.Width - pnlFrame.Width) / 2;
            pnlFrame.Location = new Point(mX1, pnlFrame.Location.Y);

        }
        

        #region Background Stuffs

        private void InitDropDowns()
        {
            //lblProgress.Visible = true;
            _bgWorker = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            _bgWorker.DoWork += SpinnerWatcherOnDoWork;
            _bgWorker.RunWorkerCompleted += SpinnerWatcherOnRunWorkerCompleted;
            _bgWorker.RunWorkerAsync();
        }


        private void SpinnerWatcherOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            try
            {

                if (Array.TrueForAll(_capturedFingerTemplates, x => x == null)) return;

                RetrieveCapturedFingers();
                //lblProgress.Visible = false;


                //if (_locationTypes == null || !_locationTypes.Any())
                //{
                //    MessageBox.Show(@"Business Location Type list is empty!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //_locationTypes.Insert(0, new BusinessLocationType { BusinessLocationTypeId = 0, Name = @"-- Please Select --" });
                //bdsLocationType.DataSource = new List<BusinessLocationType>();
                //bdsLocationType.DataSource = _locationTypes;

                //_firstLoad = false;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }

        }
        private void SpinnerWatcherOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            _capturedFingerTemplates = Utils.CapturedTemplates;
            //_locationTypes = ServiceProvider.Instance().GetBusinessLocationTypeServices().GetBusinessLocationTypes().OrderBy(m => m.Name).ToList();
        }

        #endregion

        #region Verification

        void btnVerify_Click(object sender, EventArgs e)
        {
            Stop();  // Stop the enrollment...
            UpdateStatus(0);
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
            //Invoke(new Function(() => StatusText.AppendText(message + "\r\n")));
            StatusText.AppendText(message + "\r\n");

           
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

        

    }
}
