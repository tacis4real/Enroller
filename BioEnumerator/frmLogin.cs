using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioEnumerator.API;
using BioEnumerator.APIServer.APIObjs;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.Service.Contract;
using BioEnumerator.GSetting;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator
{
    public partial class frmLogin : Form
    {

        public Action<User, StationInfo, bool> UserLoginAction;
        private StationInfo _stationInfo;
        public frmLogin(out bool flag)
        {
            InitializeComponent();
            EventHandler();
            flag = true;
            _stationInfo = new StationInfo();

            #region Locally Setup
            //if (!loadSettings())
            //{
            //    flag = false;
            //}
            //else
            //{
            //    EventHandler();
            //    flag = true;
            //}
            #endregion
            
        }


        #region Event Handler

        private void EventHandler()
        {
            txtPassword.PasswordChar = '*';
            txtUsername.Text = @"shelshel";
            txtPassword.Text = @"Password";
            txtUsername.Focus();
            btnClose.Click += btnClose_Click;
            btnLogin.Click += btnLogin_Click;
        }

        #endregion
        
        #region Events

        void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
                {
                    MessageBox.Show(@"Please supply your username", @"Validation Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                else if (txtUsername.Text.Trim().Length < 8)
                {
                    MessageBox.Show(@"Invalid username", @"Validation Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                else if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    MessageBox.Show(@"Please supply your password", @"Validation Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                else if (txtPassword.Text.Trim().Length < 8)
                {
                    MessageBox.Show(@"Invalid password", @"Validation Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
                else
                {
                    //var current = new StationInfo();

                    // Steps:

                    // 1. Check Username Existence
                    var check = UserManager.GetUser(txtUsername.Text.Trim());
                    if (check != null && check.UserId > 0)
                    {
                        var userInformation = UserManager.LoginUser(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                        if (userInformation == null)
                        {
                            MessageBox.Show(@"Error occurred! Unable to login locally | Incorrect Password or Username", @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }

                        if (!userInformation.Status.IsSuccessful)
                        {
                            MessageBox.Show(
                                string.IsNullOrEmpty(userInformation.Status.Message.FriendlyMessage)
                                    ? "Unable to login locally! Please try later"
                                    : (userInformation.Status).Message.FriendlyMessage, @"Process Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }

                        _stationInfo =
                            ServiceProvider.Instance()
                                .GetStationInfoService()
                                .GetStationInfo(userInformation.UserInfo.UserProfile.StationInfoId);
                        if (_stationInfo == null || _stationInfo.StationInfoId < 1)
                        {
                            MessageBox.Show(@"No Station Info Found! Please attach user to a particular station", @"Incomplete Process",
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            //return false;
                        }

                        if (UserLoginAction != null)
                        {
                            if (_stationInfo != null)
                            {
                                Utils.UploadStationInfo = new UploadStationInfo
                                {
                                    StationInfoId = _stationInfo.StationInfoId,
                                    EnrollerRegId = userInformation.UserInfo.UserProfile.ProfileNumber,
                                    StationName = _stationInfo.StationName,
                                    StationKey = _stationInfo.StationKey,
                                    HostServerAddress = _stationInfo.HostServerAddress,
                                    APIAccessKey = _stationInfo.APIAccessKey,
                                    Status = _stationInfo.Status,
                                };
                            }
                            UserLoginAction(userInformation.UserInfo, _stationInfo, true);
                        }

                        MessageBox.Show(@"Welcome " + (userInformation.UserInfo.UserProfile.Surname) + @" " + (userInformation.UserInfo.UserProfile.FirstName), @"Welocme", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        DialogResult = DialogResult.OK;
                        Close();

                    }
                    else
                    {
                        
                        // 2. Check Host Server Input
                        if (string.IsNullOrEmpty(txtHostServer.Text.Trim()))
                        {
                            MessageBox.Show(@"Please supply your Host Server Address", @"Validation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            // 3. Remotely Login & Return User(Operator) Details
                            var userInformation = UserManager.RemoteLoginUser(txtUsername.Text.Trim(), txtPassword.Text.Trim(), txtHostServer.Text.Trim());
                            if (userInformation == null)
                            {
                                MessageBox.Show(@"Error occurred! Unable to login remotely | Incorrect Password or Username", @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                return;
                            }

                            if (!userInformation.Status.IsSuccessful)
                            {
                                MessageBox.Show(
                                    string.IsNullOrEmpty(userInformation.Status.Message.FriendlyMessage)
                                        ? "Unable to login remotely! Please try later"
                                        : (userInformation.Status).Message.FriendlyMessage, @"Process Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                return;
                            }

                            _stationInfo =
                            ServiceProvider.Instance()
                                .GetStationInfoService()
                                .GetStationInfo(userInformation.UserInfo.UserProfile.StationInfoId);
                            if (_stationInfo == null || _stationInfo.StationInfoId < 1)
                            {
                                MessageBox.Show(@"No Station Info Found! Please attach user to a particular station", @"Incomplete Process",
                                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                //return false;
                            }

                            if (UserLoginAction != null)
                            {
                                if (_stationInfo != null)
                                {
                                    Utils.UploadStationInfo = new UploadStationInfo
                                    {
                                        StationInfoId = _stationInfo.StationInfoId,
                                        EnrollerRegId = userInformation.UserInfo.UserProfile.ProfileNumber,
                                        StationName = _stationInfo.StationName,
                                        StationKey = _stationInfo.StationKey,
                                        HostServerAddress = _stationInfo.HostServerAddress,
                                        APIAccessKey = _stationInfo.APIAccessKey,
                                        Status = _stationInfo.Status,
                                    };
                                }
                                
                                UserLoginAction(userInformation.UserInfo, _stationInfo, true);
                            }

                            MessageBox.Show(@"Welcome " + (userInformation.UserInfo.UserProfile.Surname) + @" " + (userInformation.UserInfo.UserProfile.FirstName), @"Welocme", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            DialogResult = DialogResult.OK;
                            Close();
                        }

                    }
                    

                    #region Loading Station
                    //var current = bdsStationInfo.Current as StationInfo;
                    //if (current == null || current.StationInfoId < 1)
                    //{
                    //    MessageBox.Show(@"Please select valid Station Information", @"Process Validation",
                    //        MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    //}
                    #endregion
                    
                    //else
                    //{
                        
                    //}
                    
                }

            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                MessageBox.Show(@"Error occurred! Unable to login", @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        void btnClose_Click(object sender, EventArgs e)
        {

            if (
                MessageBox.Show(@"This will close the application! Are you sure?", @"Exit Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            DialogResult = DialogResult.Cancel;

            if (UserLoginAction != null)
            {
                UserLoginAction(null, null, false);
                Close();
            }
        }

        #endregion

        private bool loadSettings()
        {
            bdsStationInfo.DataSource = new List<StationInfo>();
            var stationInfos = ServiceProvider.Instance().GetStationInfoService().GetStationInfos();
            if (stationInfos == null || !stationInfos.Any())
            {
                MessageBox.Show(@"No Host Server Setting Found! Please add new setting", @"Process Completed",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            bdsStationInfo.DataSource = stationInfos;
            return true;
        }

        public void SetPanels()
        {
            pnlAdmin.Location = new Point((Width - pnlAdmin.Width) / 2, (Height - pnlAdmin.Height) / 2);
        }

    }
}
