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
using BioEnumerator.DataManagement;
using BioEnumerator.DataManagement.PopUps;
using BioEnumerator.GSetting;
using DevComponents.DotNetBar;
using DPFP;
using Transitions;

namespace BioEnumerator
{
    public partial class frmHome : Form
    {

        #region Vars


        private SliderPanelDirection _controlBoxSlideDirection = SliderPanelDirection.Down;
        private SliderPanelDirection _bottomDashboardSlideDirection = SliderPanelDirection.Down;
        private int _controlBoxTimeout = 0;
        private bool _isControlBoxPined;
        private int _pnlControlBoxXPos;
        private int _pnlControlBoxYPos;
        private MouseMessageFilter _messageFilter;
        private readonly UserProfile _staffRegistration;



        #endregion


        public frmHome()
        {

            _messageFilter = new MouseMessageFilter();
            _messageFilter.MouseMove += _messageFilter_MouseMove;
            //_messageFilter.MouseClick += _messageFilter_MouseClick;
            Application.AddMessageFilter(_messageFilter);

            InitializeComponent();
            try
            {

                if (Utils.CurrentUser == null) { return; }
                if (Utils.CurrentUser.UserProfile == null) { return; }
                _staffRegistration = Utils.CurrentUser.UserProfile;
                lblUserInfo.Text = _staffRegistration.FirstName + @" " + _staffRegistration.Surname + @" - " + _staffRegistration.ProfileNumber;
                mtlEnroll.Text = @"New Enroll";
                mtlUpload.Text = @"Upload Data To Server";
                mtlReports.Text = @"View Local Reports";
                mtlEditInfo.Text = @"Edit Enrolment";



                #region Display Settings

                SetFullScreen();
                SetTileBodyPosition();
                SetControlBoxPosition();
                SetTilesPosition();
                SetHeaderPanelPosition();

                #endregion

                #region FrameworkEvents
                btnMaximize.Click += btnMaximize_Click;
                btnMinimize.Click += btnMinimize_Click;
                btnClose.Click += btnClose_Click;
                btnUserInfo.Click += btnUserInfo_Click;
                btnSignOut.Click +=btnSignOut_Click;
                btnChangePassword.Click += btnChangePassword_Click;
                btnHostServerSetting.Click += btnHostServerSetting_Click;
                mtlEnroll.Click += mtlEnroll_Click;
                mtlEditInfo.Click += mtlEditInfo_Click;
                mtlReports.Click += mtlReports_Click;
                mtlUpload.Click += mtlUpload_Click;
                #endregion


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        

        


        #region User Navigation Events
        void btnUserInfo_Click(object sender, EventArgs e)
        {

            if (mpnTest.Visible)
            {
                mpnTest.Visible = false;
                return;
            }
            
            mpnTest.Location = new Point(btnUserInfo.PointToScreen(Point.Empty).X, btnUserInfo.PointToScreen(Point.Empty).Y + btnUserInfo.Height + 2);
            mpnTest.Visible = true;

            btnSignOut.Text = @"Sign Out";
            btnChangePassword.Text = @"Change Password";
            btnHostServerSetting.Text = @"Host Server Setting";
        }

        void btnChangePassword_Click(object sender, EventArgs e)
        {
            mpnTest.Visible = false;

            
            var frm = new frmChangePassword(Utils.CurrentUser.UserName);
            frm.ShowDialog();
        }

        void btnHostServerSetting_Click(object sender, EventArgs e)
        {
            if (Utils.CurrentUser.RoleId != 1)
            {
                MessageBox.Show(@"Sorry, you do not have access to this module", @"Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            mpnTest.Visible = false;
            var frm = new frmUpdateHostSetting();
            frm.ShowDialog();
        }

        void btnSignOut_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show(@"This will close the application! Are you sure?", @"Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            Application.Exit();
        }


        #endregion

        

        void _messageFilter_MouseMove(object source, MouseEventArgs e)
        {

            try
            {
                if (e.Y >= 0 && e.Y <= 15)
                {
                    var t = new Transition(new TransitionType_EaseInEaseOut(100));
                    t.add(pnlControlBox, "Top", 0);
                    t.run();
                    _controlBoxTimeout = 0;
                    _controlBoxSlideDirection = SliderPanelDirection.Down;
                    cbox_timer.Start();
                }

                if (e.Y > (pnlControlBox.Height))
                {
                    if (_controlBoxSlideDirection != SliderPanelDirection.Up && !_isControlBoxPined)
                    {
                        _controlBoxSlideDirection = SliderPanelDirection.Up;
                        var t = new Transition(new TransitionType_EaseInEaseOut(100));
                        t.add(pnlControlBox, "Top", -pnlControlBox.Height);
                        t.run();
                    }

                }
            }
            catch (Exception)
            {

            }

        }


        #region Display Settings

        private void SetTileBodyPosition()
        {
            var x = Width;
            var xPos = 0;
            var mX = (Width - mpnlBody.Width) / 2;
            var mY = (Height - mpnlBody.Height) / 2;

            mpnlBody.Size = new Size(Width, mpnlBody.Height);
            mpnlBody.Location = new Point(xPos, mY);

            var title = string.Format("<span style=\"color:#333; font-size:15pt; valign:middle; text-align:right; font-weight:bold\"></span>");

        }

        private void SetFullScreen()
        {
            var x = Screen.PrimaryScreen.Bounds.Width;
            var y = Screen.PrimaryScreen.Bounds.Height;
            Location = new Point(0, 0);
            Size = new Size(x, y);
        }

        private void SetTilesPosition()
        {

            var mX = (mpnlBody.Width - pnlTiles.Width) / 2;
            var mY = (mpnlBody.Height - pnlTiles.Height) / 2;
            pnlTiles.Location = new Point(mX, mY);
            //pnlTitleHeader.Location = new Point(pnlTiles.Left - 3, mpnlBody.Top - 20);

            //lblTitleMessage.Text = lblTitleMessage.Text.Replace("{}", DateTime.Now.Year.ToString(CultureInfo.InvariantCulture));

            //var title = string.Format("<span style=\"color:#333; font-size:15pt; valign:middle; text-align:right; font-weight:bold\"> {0} </span>", _staffRegistration.FirstName + " " + _staffRegistration.Surname); // 
            //btnUserInfo.Text = title;

            //if (Utils.CurrentUser.RoleId != 1)
            //{
            //    Point outPt;
            //    Point userPt;
            //    if (Utils.CurrentUser.RoleId == 2)
            //    {
            //        //outPt = new Point(btnManageUsers.Location.X, btnManageUsers.Location.Y);
            //        //userPt = new Point(btnManageLRSystem.Location.X, btnManageLRSystem.Location.Y);

            //        //btnManageUsers.Location = userPt;
            //        //btnSignout.Location = outPt;
            //        //mpnTest.Height = mpnTest.Height - 50;
            //        //btnManageLRSystem.Visible = false;
            //        //btnManageUsers.Visible = true;

            //    }
            //    else
            //    {
            //        //outPt = new Point(btnManageLRSystem.Location.X, btnManageLRSystem.Location.Y);
            //        //mpnTest.Height = mpnTest.Height - 100;
            //        //btnSignout.Location = outPt;
            //        //btnManageUsers.Visible = false;
            //        //btnManageLRSystem.Visible = false;
            //    }

            //}

            //pnlCopyright.Size = new Size(pnlTiles.Width, pnlCopyright.Height);
            //pnlCopyright.Location = new Point(pnlTiles.PointToScreen(Point.Empty).X, mpnlBody.PointToScreen(Point.Empty).Y + mpnlBody.Height );
        }

        private void SetHeaderPanelPosition()
        {
            var mX = (Width - pnlTitleHeader.Width) / 2;
            var mY = 60;
            pnlTitleHeader.Location = new Point(mX, mY);
        }

        #endregion

        #region Control Box

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void SetControlBoxPosition()
        {

            var x = Width;
            _pnlControlBoxXPos = 0;
            _pnlControlBoxYPos = -pnlControlBox.Height;

            pnlControlBox.Size = new Size(x, pnlControlBox.Height);
            pnlControlBox.Location = new Point(_pnlControlBoxXPos, 0);

        }


        private void cbox_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_controlBoxTimeout < 500)
                {
                    _controlBoxTimeout++;
                    return;
                }
                if (_controlBoxTimeout != 500) return;
                if (_isControlBoxPined) { return; }
                if (_controlBoxSlideDirection != SliderPanelDirection.Down)
                {
                    _controlBoxTimeout = 0;
                    cbox_timer.Stop();
                    return;
                }
                var distance = -pnlControlBox.Height;
                var t = new Transition(new TransitionType_EaseInEaseOut(100));
                t.add(pnlControlBox, "Top", distance);
                t.run();
                _controlBoxSlideDirection = SliderPanelDirection.Up;
                _controlBoxTimeout = 0;
                cbox_timer.Stop();
            }
            catch (Exception)
            {

            }
        }

        #endregion



        #region Metro Tile Events

        private void mtlEnroll_Click(object sender, EventArgs e)
        {

            #region Choose Scanner

            //try
            //{
            //    var chooseScannerForm = new frmChooseScanner();
            //    if (chooseScannerForm.ShowDialog() == DialogResult.OK)
            //    {
            //        try
            //        {
            //            //engine = new Neurotec.Biometrics.Nffv(chooseScannerForm.FingerprintDatabase, chooseScannerForm.FingerprintDatabasePassword, chooseScannerForm.ScannerString);
            //        }
            //        catch (Exception)
            //        {
            //            MessageBox.Show(@"Failed to initialize Nffv or create/load database.\r\n" +
            //            @"Please check if:\r\n - Provided password is correct;\r\n - Database filename is correct;\r\n" +
            //            @" - Scanners are used properly.\r\n", @"Nffv C# Sample", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //        //Application.Run(new MainForm(engine, chooseScannerForm.UserDatabase));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(
            //        string.Format("An error has occured: {0}", ex.Message), @"Nffv C# Sample",
            //        MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    if (engine != null)
            //    {
            //        engine.Dispose();
            //    }
            //}

            #endregion


            bool flag;
            var frm = new frmModuleDisplay(1, out flag);
            if (!flag) { return; }
            Utils.CapturedTemplates = new Template[10];
            Utils.CaptureImage = null;
            Utils.BiometricInfo = new BiometricInfo();
            Utils.PreviewBeneficiaryRegObj = new PreviewBeneficiaryRegObj();
            Utils.BeneficiaryRegObj = new BeneficiaryRegObj
            {
                FingerPrintTemplate = new List<byte[]>(),
                CapturedFingerPrintTemplate = new Template[10]
            };
            frm.Show();
            frm.Location = new Point(Width, 0);
            var t = new Transition(new TransitionType_EaseInEaseOut(150));
            t.add(frm, "Left", 0);
            t.run();
        }
        void mtlEditInfo_Click(object sender, EventArgs e)
        {
            bool flag;
            var frm = new frmModuleDisplay(2, out flag);
            if (!flag) { return; }
            frm.Show();
            frm.Location = new Point(Width, 0);
            var t = new Transition(new TransitionType_EaseInEaseOut(150));
            t.add(frm, "Left", 0);
            t.run();
        }

        void mtlReports_Click(object sender, EventArgs e)
        {
            bool flag;
            var frm = new frmModuleDisplay(3, out flag);
            if (!flag) { return; }
            frm.Show();
            frm.Location = new Point(Width, 0);
            var t = new Transition(new TransitionType_EaseInEaseOut(150));
            t.add(frm, "Left", 0);
            t.run();
        }


        void mtlUpload_Click(object sender, EventArgs e)
        {
            bool flag;
            var frm = new frmModuleDisplay(4, out flag);
            if (!flag) { return; }
            frm.Show();
            frm.Location = new Point(Width, 0);
            var t = new Transition(new TransitionType_EaseInEaseOut(150));
            t.add(frm, "Left", 0);
            t.run();
        }

        #endregion

        

    }
}
