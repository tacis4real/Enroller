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
using BioEnumerator.DataAccessManager.Repository;
using BioEnumerator.GSetting;
using DPFP;
using DPFP.Capture;
using DPFP.Gui.Enrollment;
using DPFP.Gui.Verification;
using DPFP.Verification;
using Newtonsoft.Json;
using Transitions;
using XPLUG.WINDOWTOOLS.Extensions;

namespace BioEnumerator.DataManagement
{
    public partial class UclFingerEnroll : UserControl
    {


        #region Vars
        private FingerEnrollData.AppData _data;					// keeps application-wide data
        private BackgroundWorker _bgWorker;
        private DPFP.Processing.Enrollment _enroller;
        private Verification _verificator;
        private EnrollmentControl _enrollmentControl;
        private readonly ImageConverter _imgConverter = new ImageConverter();

        #endregion

        public UclFingerEnroll(FingerEnrollData.AppData appData)
        {
            //_data = appData;
            InitializeComponent();
            //ExchangeData(true);		// Init data with default control values;
            //_data.OnChange += () => ExchangeData(false);	// Track data changes to keep the form synchronized

            _enrollmentControl = new EnrollmentControl();
            EventHooks();
        }



        private void EventHooks()
        {

            Load += UclFingerEnroll_Load;
            btnCaptureFinger.Click += btnCaptureFinger_Click;
            EnrollmentControl.OnEnroll += EnrollmentControl_OnEnroll;
            EnrollmentControl.OnDelete += EnrollmentControl_OnDelete;
            EnrollmentControl.OnCancelEnroll += EnrollmentControl_OnCancelEnroll;
            EnrollmentControl.OnReaderConnect += EnrollmentControl_OnReaderConnect;
            EnrollmentControl.OnReaderDisconnect += EnrollmentControl_OnReaderDisconnect;
            EnrollmentControl.OnStartEnroll += EnrollmentControl_OnStartEnroll;
            EnrollmentControl.OnFingerRemove += EnrollmentControl_OnFingerRemove;
            EnrollmentControl.OnFingerTouch += EnrollmentControl_OnFingerTouch;
            EnrollmentControl.OnSampleQuality += EnrollmentControl_OnSampleQuality;
            EnrollmentControl.OnComplete += EnrollmentControl_OnComplete;

        }

        

        #region Form Event Handlers:

        void UclFingerEnroll_Load(object sender, EventArgs e)
        {

            //Init();
            //Start();	// Start capture operation.
            //_data = new FingerEnrollData.AppData();		// Create the application data object
            ListEvents.Items.Clear();
            
            //Utils.BiometricInfo.RightThumbPrintTemplateFile = new Template[0];
            //Utils.BiometricInfo.RightIndexPrintTemplateFile = new Template[0];
          
        }
        
        #endregion

        public void SetPanel()
        {

            var mX = (pnlBody.Width - pnlItemBody.Width) / 2;
            pnlItemBody.Location = new Point(mX, pnlItemBody.Location.Y);

            var mX1 = (pnlItemBody.Width - pnlFrame.Width) / 2;
            pnlFrame.Location = new Point(mX1, pnlFrame.Location.Y);

        }


        #region Events

        


        #region Sub Modules

        void btnCaptureFinger_Click(object sender, EventArgs e)
        {

            #region Trying Saving Templates in DB

            var tempx = new FingerTemplateData
            {
                FingerPrintTemplates = Utils.BeneficiaryRegObj.FingerPrintTemplate
            };

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
            var frm = new frmModuleDisplay(2, out flag);
            if (!flag) { return; }
            frm.Show();
            frm.Location = new Point(Width, 0);
            var t = new Transition(new TransitionType_EaseInEaseOut(150));
            t.add(frm, "Left", 0);
            t.run();
        }


        #endregion



        #endregion



        
        #region Enroll Events

        // Simple dialog data exchange (DDX) implementation.
        private void ExchangeData(bool read)
        {
            if (read)
            {	// read values from the form's controls to the data object
              
                _data.IsEventHandlerSucceeds = true;
                _data.EnrolledFingersMask = 0;
                _data.MaxEnrollFingerCount = _data.MaxEnrollFingerCount;
                _data.Update();
            }
            else
            {	// read values from the data object to the form's controls
                //EnrollmentControl.EnrolledFingerMask = _data.EnrolledFingersMask;
                EnrollmentControl.MaxEnrollFingerCount = _data.MaxEnrollFingerCount;
            }
            //else
            //{	// read valuse from the data object to the form's controls
            //    Mask.Value = _data.EnrolledFingersMask;
            //    MaxFingers.Value = Data.MaxEnrollFingerCount;
            //    IsSuccess.Checked = Data.IsEventHandlerSucceeds;
            //    IsFailure.Checked = !IsSuccess.Checked;
            //    IsFeatureSetMatched.Checked = Data.IsFeatureSetMatched;
            //    FalseAcceptRate.Text = Data.FalseAcceptRate.ToString();
            //    VerifyButton.Enabled = Data.EnrolledFingersMask > 0;
            //}
        }
        
        // event handling
        public void EnrollmentControl_OnEnroll(Object control, int finger, Template template, ref DPFP.Gui.EventHandlerStatus status)
        {

            #region Without DataExchange

            var fingerprintData = new MemoryStream();

            #region Adding Templates of Byte Array to List

            //_enrollmentControl.Capture;
            //_enrollmentControl.
            template.Serialize(fingerprintData);
            Utils.RightThumbPrintTemplateFile = template;
            Utils.BiometricInfo.RightThumbPrintTemplateFile = template;
            Utils.BiometricInfo.RightThumbPrintTemplate = fingerprintData.ToArray();
            Utils.BeneficiaryRegObj.RightThumbPrintTemplate = fingerprintData.ToArray();
            Utils.BeneficiaryRegObj.FingerPrintTemplate[finger - 1] = fingerprintData.ToArray();
            //Utils.BeneficiaryRegObj.FingerPrintTemplate.Add(fingerprintData.ToArray());

            #endregion

            #endregion


            //if (_data.IsEventHandlerSucceeds)
            //{

                //var fingerprintData = new MemoryStream();

                //#region Adding Templates of Byte Array to List

                //template.Serialize(fingerprintData);
                //Utils.RightThumbPrintTemplateFile = template;
                //Utils.BiometricInfo.RightThumbPrintTemplateFile = template;
                //Utils.BiometricInfo.RightThumbPrintTemplate = fingerprintData.ToArray();
                //Utils.BeneficiaryRegObj.RightThumbPrintTemplate = fingerprintData.ToArray();
                //Utils.BeneficiaryRegObj.FingerPrintTemplate.Add(fingerprintData.ToArray());

                //#endregion

                #region Storing Each Finger Templates
                
                //switch (finger)
                //{
                //    case 1:
                        
                //        template.Serialize(fingerprintData);
                //        Utils.RightThumbPrintTemplateFile = template;
                //        Utils.BiometricInfo.RightThumbPrintTemplateFile = template;
                //        Utils.BiometricInfo.RightThumbPrintTemplate = fingerprintData.ToArray();
                //        Utils.BeneficiaryRegObj.RightThumbPrintTemplate = fingerprintData.ToArray();
                //        Utils.BeneficiaryRegObj.FingerPrintTemplate.Add(fingerprintData.ToArray());

                        

                //        #region Image 

                //        //using (var ms = new MemoryStream(Utils.BiometricInfo.RightThumbPrintTemplate.ToArray()))
                //        //{
                //        //    // new Bitmap(Image.FromStream(new MemoryStream(bytes)
                //        //    using (var img = new Bitmap(Image.FromStream(ms, true)))
                //        //    {
                //        //        picFinger.Image = img;
                //        //    }
                //        //}

                //        //using (var ms = GetImageFromByteArray(Utils.BiometricInfo.RightThumbPrintTemplate))
                //        //{
                //        //    picFinger.Image = ms;
                //        //}

                //        //var img = EnrollmentControl.OnSampleQuality;
                //        #endregion


                //        // For Verification Purpose Later
                //        var dataStream = new MemoryStream(Utils.BiometricInfo.RightThumbPrintTemplate.ToArray());
                //        var rightThumbTemp = new Template();
                //        rightThumbTemp.DeSerialize(dataStream);
                        
                //        break;

                //    case 2:

                //        template.Serialize(fingerprintData);
                //        Utils.RightIndexPrintTemplateFile = template;
                //        Utils.BiometricInfo.RightIndexPrintTemplateFile = template;
                //        Utils.BiometricInfo.RightIndexPrintTemplate = fingerprintData.ToArray();
                //        Utils.BeneficiaryRegObj.RightIndexPrintTemplate = fingerprintData.ToArray();
                //        break;
                //}


                //Init();
                //Start();	// Start capture operation.


                #endregion

                //_data.Templates[finger - 1] = template;		    // store a finger template
                //ExchangeData(true);								// update other data

                //ListEvents.Items.Insert(0, String.Format("OnEnroll: finger {0}", finger));
           // }
            //else
            //    status = DPFP.Gui.EventHandlerStatus.Failure;	// force a "failure" status
        }
        
        public void EnrollmentControl_OnDelete(Object Control, int Finger, ref DPFP.Gui.EventHandlerStatus Status)
        {
            if (_data.IsEventHandlerSucceeds)
            {
                _data.Templates[Finger - 1] = null;			    // clear the finger template
                ExchangeData(true);								// update other data

                ListEvents.Items.Insert(0, String.Format("OnDelete: finger {0}", Finger));
            }
            else
                Status = DPFP.Gui.EventHandlerStatus.Failure;	// force a "failure" status
        }

        private void EnrollmentControl_OnCancelEnroll(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnCancelEnroll: {0}, finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnReaderConnect(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnReaderConnect: {0}, finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnReaderDisconnect(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnReaderDisconnect: {0}, finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnStartEnroll(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnStartEnroll: {0}, finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnFingerRemove(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnFingerRemove: {0}, finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnFingerTouch(object Control, string ReaderSerialNumber, int Finger)
        {
            ListEvents.Items.Insert(0, String.Format("OnFingerTouch: {0}, finger {1}", ReaderSerialNumber, Finger));
        }

        private void EnrollmentControl_OnSampleQuality(object Control, string ReaderSerialNumber, int Finger, CaptureFeedback captureFeedback)
        {
            ListEvents.Items.Insert(0, String.Format("OnSampleQuality: {0}, finger {1}, {2}", ReaderSerialNumber, Finger, captureFeedback));
        }

        private void EnrollmentControl_OnComplete(object Control, string ReaderSerialNumber, int Finger)
        {
            //var sample = Sample;
            ListEvents.Items.Insert(0, String.Format("OnComplete: {0}, finger {1}", ReaderSerialNumber, Finger));
        }
        
        #endregion


        #region Byte Conversion

        public byte[] ListConvertByte(List<byte[]> bufferList)
        {
            var flattenedList = bufferList.SelectMany(bytes => bytes);
            var byteArray = flattenedList.ToArray();

            return byteArray;
        }

        public byte[] ConvertListToByte(List<byte[]> bufferList)
        {
            var totalLength = bufferList.Sum(buffer => buffer.Length);
            var fullBuffer = new byte[totalLength];

            int insertPosition = 0;
            foreach (var buffer in bufferList)
            {
                buffer.CopyTo(fullBuffer, insertPosition);
                insertPosition += buffer.Length;
            }

            return fullBuffer;
        }


        public void ByteListConvert(List<byte[]> bufferList)
        {
            var buffers = new List<byte[]>();
            var totalLength = buffers.Sum(buffer => buffer.Length);
            var fullBuffer = new byte[totalLength];

            int insertPosition = 0;
            foreach (var buffer in buffers)
            {
                buffer.CopyTo(fullBuffer, insertPosition);
                insertPosition += buffer.Length;
            }
            
        }


        private byte[] SerializeTemplates(Template[] templates)
        {
            //var fingerprintData = new MemoryStream();
            //templates.Serialize(fingerprintData);
            return null;
        }


        public Image ConvertByteToImage(byte[] bytes)
        {
            return new Bitmap(Image.FromStream(new MemoryStream(bytes)));
        }

        static public Bitmap BitmapFromBitmapData(byte[] bitmapData)
        {
            try
            {
                var ms = new MemoryStream(bitmapData);
                return (new Bitmap(ms));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public Bitmap GetImageFromByteArray(byte[] byteArray)
        {
            var bm = (Bitmap)_imgConverter.ConvertFrom(byteArray);

            if (bm != null && (bm.HorizontalResolution != (int)bm.HorizontalResolution ||
                               bm.VerticalResolution != (int)bm.VerticalResolution))
            {
                // Correct a strange glitch that has been observed in the test program when converting 
                //  from a PNG file image created by CopyImageToByteArray() - the dpi value "drifts" 
                //  slightly away from the nominal integer value
                bm.SetResolution((int)(bm.HorizontalResolution + 0.5f),
                                 (int)(bm.VerticalResolution + 0.5f));
            }

            return bm;
        }

        #endregion
       



    }
}
