using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioEnumerator.APIServer.APIObjs;
using BioEnumerator.CommonUtils;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;
using BioEnumerator.GSetting;
using Transitions;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataManagement
{
    public partial class frmModuleDisplay : Form
    {

        #region Vars

        //private SliderPanelDirection _controlBoxSlideDirection = SliderPanelDirection.Down;
       // private SliderPanelDirection _bottomDashboardSlideDirection = SliderPanelDirection.Down;
        //private int _controlBoxTimeout = 0;
        //private bool _isControlBoxPined;
        //private int _pnlControlBoxXPos;
        //private int _pnlControlBoxYPos;
        //private MouseMessageFilter _messageFilter;
        private FingerEnrollData.AppData _data;					// keeps application-wide data
        private DPFP.Template _template;

        public Action<bool> RefreshAutoCompleteAction;
        public Action<int> LoadSubModules;
        private int _callerId;

        #endregion

        public frmModuleDisplay(int callerId, out bool flag)
        {

            //_messageFilter = new MouseMessageFilter();
            //_messageFilter.MouseMove += _messageFilter_MouseMove;
            //_messageFilter.MouseClick += _messageFilter_MouseClick;
            //Application.AddMessageFilter(_messageFilter);

            InitializeComponent();
            try
            {
                #region FrameworkEvents
                //btnMaximize.Click += btnMaximize_Click;
                btnMinimize.Click += btnMinimize_Click;
                //btnClose.Click += btnClose_Click;
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            _data = new FingerEnrollData.AppData();
            
            _data.OnChange += delegate { ExchangeData(false); };	// Track data changes to keep the form synchronized
            ExchangeData(false);	
            SetFullScreen();
            SetLoaderPanel();
            //SetControlBoxPosition();
            defineEvents();
            _callerId = callerId;
            LoadModule(callerId, out flag);
        }



        


        #region Display Settings
        private void SetFullScreen()
        {
            var x = Screen.PrimaryScreen.Bounds.Width;
            var y = Screen.PrimaryScreen.Bounds.Height;
            Location = new Point(0, 0);
            Size = new Size(x, y);

        }


        #region Event Definitions
        private void defineEvents()
        {
            btnBack.Click += btnBack_Click;
            //Load += frmModuleDisplay_Load;
        }

        void frmModuleDisplay_Load(object sender, EventArgs e)
        {
            //new UclCaptureImage().CloseCamera();
        }

        #endregion



        private void SetLoaderPanel()
        {
            pnlLoader.Location = new Point(Width, 0);
            pnlLoader.Size = new Size(pnlBody.Width, pnlBody.Height);
        }

        #endregion

        #region Control Box


        //void _messageFilter_MouseMove(object source, MouseEventArgs e)
        //{

        //    try
        //    {
        //        if (e.Y >= 0 && e.Y <= 15)
        //        {
        //            var t = new Transition(new TransitionType_EaseInEaseOut(100));
        //            t.add(pnlControlBox, "Top", 0);
        //            t.run();
        //            _controlBoxTimeout = 0;
        //            _controlBoxSlideDirection = SliderPanelDirection.Down;
        //            cbox_timer.Start();
        //        }

        //        if (e.Y > (pnlControlBox.Height))
        //        {
        //            if (_controlBoxSlideDirection != SliderPanelDirection.Up && !_isControlBoxPined)
        //            {
        //                _controlBoxSlideDirection = SliderPanelDirection.Up;
        //                var t = new Transition(new TransitionType_EaseInEaseOut(100));
        //                t.add(pnlControlBox, "Top", -pnlControlBox.Height);
        //                t.run();
        //            }

        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }

        //}

        //private void btnMaximize_Click(object sender, EventArgs e)
        //{
        //    WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        //}

        //private void btnClose_Click(object sender, EventArgs e)
        //{
        //    Application.Exit();
        //}

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        //private void SetControlBoxPosition()
        //{

        //    var x = Width;
        //    _pnlControlBoxXPos = 0;
        //    _pnlControlBoxYPos = -pnlControlBox.Height;

        //    pnlControlBox.Size = new Size(x, pnlControlBox.Height);
        //    pnlControlBox.Location = new Point(_pnlControlBoxXPos, 0);

        //}


        ////private void cbox_timer_Tick(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (_controlBoxTimeout < 500)
        ////        {
        ////            _controlBoxTimeout++;
        ////            return;
        ////        }
        ////        if (_controlBoxTimeout != 500) return;
        ////        if (_isControlBoxPined) { return; }
        ////        if (_controlBoxSlideDirection != SliderPanelDirection.Down)
        ////        {
        ////            _controlBoxTimeout = 0;
        ////            cbox_timer.Stop();
        ////            return;
        ////        }
        ////        var distance = -pnlControlBox.Height;
        ////        var t = new Transition(new TransitionType_EaseInEaseOut(100));
        ////        t.add(pnlControlBox, "Top", distance);
        ////        t.run();
        ////        _controlBoxSlideDirection = SliderPanelDirection.Up;
        ////        _controlBoxTimeout = 0;
        ////        cbox_timer.Stop();
        ////    }
        ////    catch (Exception)
        ////    {

        ////    }
        ////}

        #endregion

        #region Framework Events
        void btnBack_Click(object sender, EventArgs e)
        {
            //new UclCaptureImage().CloseCamera();
            //new UclCaptureImage().btnStartCamera_Click(null, null);
            var t = new Transition(new TransitionType_EaseInEaseOut(300));
            t.add(this, "Left", Width);
            t.run();
            t.TransitionCompletedEvent += t_TransitionCompletedEvent;
        }


        void t_TransitionCompletedEvent(object sender, Transition.Args e)
        {
            if (RefreshAutoCompleteAction != null)
            {
                RefreshAutoCompleteAction(true);
            }

            //new UclCaptureImage().CloseCamera();
            Close();
        }


        private void LoadModule(int callerId, out bool flag)
        {
            pnlLoader.Controls.Clear();
            try
            {
                switch (callerId)
                {
                    case 1:

                        //var dataModule = new UclEnroll();
                        //pnlLoader.Controls.Add(dataModule);
                        //dataModule.Dock = DockStyle.Fill;
                        //dataModule.SetPanel();
                        //lblTitle.Text = @"Personal Data Capture";


                        var dataModule = new UclDataCapture();
                        pnlLoader.Controls.Add(dataModule);
                        dataModule.Dock = DockStyle.Fill;
                        dataModule.SetPanel();
                        lblTitle.Text = @"Beneficiary Data Capture";
                        break;

                    case 2:
                       
                        var editModule = new UclEditData();
                        pnlLoader.Controls.Add(editModule);
                        editModule.Dock = DockStyle.Fill;
                        editModule.SetPanel();
                        lblTitle.Text = @"Edit Data Capture";
                        break;
                        //var fingerModule = new UclCaptureFinger();
                        //pnlLoader.Controls.Add(fingerModule);
                        //fingerModule.Dock = DockStyle.Fill;
                        //fingerModule.SetPanel();
                        //ExchangeData(true);		// transfer values from the main form to the data object
                        //lblTitle.Text = @"FingerPrint Data Capture";
                        ////new UclCaptureImage().CloseCamera();
                        //break;
                        

                    case 3:

                        var rptModule = new UclLocalDataReport();
                        pnlLoader.Controls.Add(rptModule);
                        rptModule.Dock = DockStyle.Fill;
                        rptModule.SetPanel();
                        lblTitle.Text = @"Data Report Generation";
                        break;

                        //var imageModule = new UclPreview();
                        //pnlLoader.Controls.Add(imageModule);
                        //imageModule.Dock = DockStyle.Fill;
                        //imageModule.SetPanel();
                        //lblTitle.Text = @"Captured Info Preview";

                        //var imageModule = new UclCaptureImage();
                        //pnlLoader.Controls.Add(imageModule);
                        //imageModule.Dock = DockStyle.Fill;
                        //imageModule.SetPanel();
                        //lblTitle.Text = @"Image Capture";
                        break;
                        

                    case 4:
                        var uploadModule = new UclUploadData();
                        pnlLoader.Controls.Add(uploadModule);
                        uploadModule.Dock = DockStyle.Fill;
                        uploadModule.SetPanel();
                        lblTitle.Text = @"Data Upload Center";
                        break;


                    case 5:
                        //var rptModule = new UclLocalDataReport();
                        //pnlLoader.Controls.Add(rptModule);
                        //rptModule.Dock = DockStyle.Fill;
                        //rptModule.SetPanel();
                        //lblTitle.Text = @"Data Report Generation";
                        //break;


                    //case 3:
                    //    var searchModule = new UclSearchData();
                    //    pnlLoader.Controls.Add(searchModule);
                    //    searchModule.Dock = DockStyle.Fill;
                    //    searchModule.SetPanel();
                    //    lblTitle.Text = @"Search For Business Information";
                    //    break;
                    //case 4:
                    //    var impModule = new UclImportData();
                    //    pnlLoader.Controls.Add(impModule);
                    //    impModule.Dock = DockStyle.Fill;
                    //    impModule.SetPanel();
                    //    lblTitle.Text = @"Import Bulk Data";
                    //    break;
                    //case 5:
                    //    var rptModule = new UclLocalDataReport();
                    //    pnlLoader.Controls.Add(rptModule);
                    //    rptModule.Dock = DockStyle.Fill;
                    //    rptModule.SetPanel();
                    //    lblTitle.Text = @"Data Report Generation";
                    //    break;
                    //case 6:
                    //    var synModule = new UclSyncParameters();
                    //    pnlLoader.Controls.Add(synModule);
                    //    synModule.Dock = DockStyle.Fill;
                    //    synModule.SetPanel();
                    //    lblTitle.Text = @"Remote Data Synchronization";
                    //    break;

                    //case 7:
                    //    var uploadModule = new UclUploadData();
                    //    pnlLoader.Controls.Add(uploadModule);
                    //    uploadModule.Dock = DockStyle.Fill;
                    //    uploadModule.SetPanel();
                    //    lblTitle.Text = @"Data Upload Center";
                    //    break;

                    //case 8:
                    //    var downModule = new UclDownloadTRN();
                    //    pnlLoader.Controls.Add(downModule);
                    //    downModule.Dock = DockStyle.Fill;
                    //    downModule.SetPanel();
                    //    lblTitle.Text = @"Data Download Center";
                    //    break;

                    case 9: //Stock Requisition Issuance
                        //var stockRequisitionIssuanceModule = new UclStockRequisitionIssuance();
                        //pnlLoader.Controls.Add(stockRequisitionIssuanceModule);
                        //stockRequisitionIssuanceModule.Dock = DockStyle.Fill;
                        //stockRequisitionIssuanceModule.SetPanel();
                        break;

                    case 10: //Payment
                        //var paymentModule = new UclPayment();
                        //pnlLoader.Controls.Add(paymentModule);
                        //paymentModule.Dock = DockStyle.Fill;
                        //paymentModule.SetPanel();
                        break;

                    case 11: //Purchased Items Delivery
                        //var purchasedItemDeliveryModule = new UclPurchasedItemDeliverys();
                        //pnlLoader.Controls.Add(purchasedItemDeliveryModule);
                        //purchasedItemDeliveryModule.Dock = DockStyle.Fill;
                        //purchasedItemDeliveryModule.SetPanel();
                        break;

                }


            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }
            flag = true;
        }



        #region Enrollment

        private void OnTemplate(DPFP.Template template)
        {
            Invoke(new Function(delegate()
            {
                _template = template;

                //VerifyButton.Enabled = SaveButton.Enabled = (Template != null);
                if (_template != null)
                    MessageBox.Show(@"The fingerprint template is ready for fingerprint verification.", @"Fingerprint Enrollment");
                else
                    MessageBox.Show(@"The fingerprint template is not valid. Repeat fingerprint enrollment.", @"Fingerprint Enrollment");
            }));
        }



        #endregion

        #endregion


        #region Enrollment Events

        private void ExchangeData(bool read)
        {
            if (read)
            {	// read values from the form's controls to the data object
                _data.EnrolledFingersMask = 0;
                _data.MaxEnrollFingerCount = 10;
                _data.IsEventHandlerSucceeds = true;
                _data.Update();
            }
        }


        #endregion



    }
}
