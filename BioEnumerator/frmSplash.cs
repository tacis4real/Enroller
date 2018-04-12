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
using BioEnumerator.APIServer.DataLoader;
using BioEnumerator.CommonUtils;
using BioEnumerator.DataAccessManager.CommonUtils;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.Service.Contract;
using BioEnumerator.GSetting;
using BioEnumerator.RemoteHelper.MessengerService;
using MetroFramework;
using XPLUG.WINDOWTOOLS;
using XPLUG.WINDOWTOOLS.Date;

namespace BioEnumerator
{
    public partial class frmSplash : Form
    {


        #region Vars

        private BackgroundWorker _spinnerWatcher;
        private bool _isCorporateInfoReg;

        private bool _appState;
        private bool _isAppConfig;
        private frmCorporateRegistration _frmCorporateRegistration;
        private bool _isUserDBOk;
        private bool _isMainDBOk;
        private bool _registerCompany;
        private bool _isAllConfigSettingsOk;
        private string _mssg = "";

        private frmLogin _frmLogin;

        #endregion

        public frmSplash()
        {
            InitializeComponent();
            EventDefs();
            //SetFileAccess();

        }


        private void SetFileAccess()
        {
            var basePath = InternetCon.GetBasePath() + "\\AppFiles";
            CustomHelper.AccessControl(basePath);
        }



        #region Events Defs

        private void EventDefs()
        {
            SetFullScreen();
            SetLoaderPanelPosition();
            Shown += frmSplash_Shown;
        }

        
        
        #endregion

        #region Set Positions

        private void SetFullScreen()
        {
            var x = Screen.PrimaryScreen.Bounds.Width;
            var y = Screen.PrimaryScreen.Bounds.Height;
            Location = new Point(0, 0);
            Size = new Size(x, y);
        }
        private void SetLoaderPanelPosition()
        {
            var mX = (Width - pnlLoader.Width) / 2;
            var mY = (Height - pnlLoader.Height) / 2;
            pnlLoader.Location = new Point(mX, mY);
        }

        #endregion

        #region Events
            void frmSplash_Shown(object sender, EventArgs e)
            {
                InitSpinnerWatcher();
            }
        #endregion

        #region >- Config
            
            private void CheckConfigurations()
            {

                #region Latest


                //if (!_isMainDBOk)
                //{
                //    _spinnerWatcher.ReportProgress(5); //5
                //    int status;

                //    AppConfigSettings(out status);
                //    if (status < 0)
                //    {
                //        _isAllConfigSettingsOk = false;
                //        MetroMessageBox.Show(this, _mssg.Length > 0 ? _mssg : "This application has been interrupted.", "App Setting Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //        return;
                //    }

                //    if (Utils.AppConfigSettingInfo.FirstTimeLaunch)
                //    {
                //        // Call Controller to Download and Update LocalDB with Remote User
                //        string msg;
                //        var ret = Controller.DownLoadRemoteUserInfos(out msg);
                //        if (ret == ProcessingStatus.OperationTerminated)
                //        {
                //            _mssg = msg;
                //            _isAllConfigSettingsOk = false;
                //            MetroMessageBox.Show(this, _mssg.Length > 0 ? _mssg : "This application has been interrupted.", "App Setting Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //            return;
                //        }
                //    }

                    #region Old Pattern 2

                    //var checkSetting = AppConfigSetting(out status);
                    //if (status < 0)
                    //{
                    //    _isAllConfigSettingsOk = false;
                    //    MetroMessageBox.Show(this, _mssg.Length > 0 ? _mssg : "This application has been interrupted.", "App Setting Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //    return;
                    //}

                    //if (checkSetting)
                    //{
                    //    // Call Controller to Download and Update LocalDB with Remote User
                    //    string msg;
                    //    var ret = Controller.DownLoadRemoteUserInfos(out msg);
                    //    if (ret == ProcessingStatus.OperationTerminated)
                    //    {
                    //        _mssg = msg;
                    //        _isAllConfigSettingsOk = false;
                    //        MetroMessageBox.Show(this, _mssg.Length > 0 ? _mssg : "This application has been interrupted.", "App Setting Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //        return;
                    //    }
                    //}

                    #endregion

                    #region Old Pattern 1

                    //if (AppConfigSetting(out status))
                    //{

                    //    // Call Controller to Download and Update LocalDB with Remote User
                    //    string msg;
                    //    var ret = Controller.DownLoadRemoteUserInfos(out msg);
                    //    if (ret == ProcessingStatus.OperationTerminated)
                    //    {
                    //        _mssg = msg;
                    //        _isAllConfigSettingsOk = false;
                    //        MetroMessageBox.Show(this, _mssg.Length > 0 ? _mssg : "This application has been interrupted.", "App Setting Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //        return;
                    //    }
                    //}

                    //if (status < 0)
                    //{
                    //    _isAllConfigSettingsOk = false;
                    //    MetroMessageBox.Show(this, _mssg.Length > 0 ? _mssg : "This application has been interrupted.", "App Setting Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //}

                    #endregion


                    // AllConfigure is OK
                    //_isAllConfigSettingsOk = true;

                //}

                #endregion
                
                #region Old Pattern

                if (!_isUserDBOk)
                {
                    if (!CheckMainDB())
                    {
                        _isAllConfigSettingsOk = false;
                        MetroMessageBox.Show(this, _mssg.Length > 0 ? _mssg : "This application is unable to communicate with the registered database. Kindly ensure that all connection parameters are properly configured", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }


                #region Locally Setup Flow

                //if (!_isCorporateInfoReg)
                //{
                //    if (!CheckCorporateReg())
                //    {
                //        _isAllConfigSettingsOk = false;
                //        if (!_registerCompany)
                //        {
                //            MetroMessageBox.Show(this, "You cannot continue with this application until you have fully registered your station information! Click OK to exit...", "Exiting Application ...", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //        }
                //        return;
                //    }
                //}

                #endregion
                
                
                // AllConfigure is OK
                _isAllConfigSettingsOk = true;
                #endregion

            }


            #region >- DB Validation

            private bool AppConfigSetting(out int status)
            {

                try
                {
                    status = 0;
                    _spinnerWatcher.ReportProgress(10); //65
                    var appConfigSettings =
                            ServiceProvider.Instance().GetAppConfigSettingService().GetAppConfigSettings();
                    if (appConfigSettings == null) { return false; }
                    if (!appConfigSettings.Any()) { return false; }
                    if (appConfigSettings.Count != 1)
                    {
                        status = -1;
                        _isMainDBOk = false;
                        return false;
                    }
                    if (appConfigSettings[0].AppConfigSettingId < 1)
                    {
                        _isMainDBOk = false;
                        return false;
                    }

                    if (!appConfigSettings[0].FirstTimeLaunch)
                    {
                        _isMainDBOk = true;
                        return false;
                    }

                    _isMainDBOk = true;
                    Utils.AppConfigSettingInfo = appConfigSettings[0];
                    return true;

                }
                catch (Exception)
                {
                    status = -1;
                    return false;
                }
            }

            private void AppConfigSettings(out int status)
            {

                try
                {
                    status = 0;
                    _spinnerWatcher.ReportProgress(10); //65
                    var appConfigSettings =
                            ServiceProvider.Instance().GetAppConfigSettingService().GetAppConfigSettings();
                    if (appConfigSettings == null)
                    {
                        return;
                    }
                    if (!appConfigSettings.Any()) { return; }
                    if (appConfigSettings.Count != 1)
                    {
                        status = -1;
                        _isMainDBOk = false;
                        return;
                    }
                    if (appConfigSettings[0].AppConfigSettingId < 1)
                    {
                        status = -1;
                        _isMainDBOk = false;
                        return;
                    }

                    //if (!appConfigSettings[0].FirstTimeLaunch)
                    //{

                    //    _isMainDBOk = true;
                    //    return false;
                    //}

                    _isMainDBOk = true;
                    Utils.AppConfigSettingInfo = appConfigSettings[0];
                    //return true;

                }
                catch (Exception)
                {
                    _isMainDBOk = false;
                    status = -1;
                }
            }
              

            private bool CheckMainDB()
            {

                try
                {
                    string msg;

                    //if (!eDataAdminLite.API.MigrationAssistance.Migrate(out msg))
                    //{
                    //    _mssg = msg;
                    //    _isMainDBOk = false;
                    //    return false;
                    //}

                    //User Admin
                    var role = UserManager.GetRole("User Admin");
                    if (role == null || role.RoleId < 1)
                    {
                        //_mssg = msg;
                        _isMainDBOk = false;
                        return false;
                    }

                    _spinnerWatcher.ReportProgress(5);
                    _isUserDBOk = true;
                    return true;

                }
                catch (Exception)
                {
                    _isMainDBOk = false;
                    return false;
                }

            }

            private bool CheckAppDB()
            {

                try
                {
                    string msg;

                    //if (!ServiceProvider.Instance().GetMigrationServices().Update(out msg))
                    //{
                    //    _mssg = msg;

                    //    return false;
                    //}
                    return true;
                }
                catch (Exception)
                {
                    _isMainDBOk = false;
                    return false;
                }

            }


            #endregion


            #region >- Company Validation (15)
            private bool CheckCorporateReg()
            {
                _spinnerWatcher.ReportProgress(5); //5
                int status;
                if (!CheckBasicSetup(out status))
                {
                    if (status < 0)
                    {
                        _isCorporateInfoReg = false;
                        return false;
                    }
                    if (MetroMessageBox.Show(this, "You have not set up your station information!" + "\r\n" +
                                                    "This must be set up before loading the app" + "\r\n" +
                                                    "Do you want to set up your station information now?", "Station Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        _registerCompany = false;
                        _isCorporateInfoReg = false;
                        return false;
                    }

                    _registerCompany = true;
                    _isCorporateInfoReg = false;
                    return false;

                }
                _spinnerWatcher.ReportProgress(5);
                _registerCompany = false;
                _isCorporateInfoReg = true;
                return true;
            }
            private bool CheckBasicSetup(out int status)
            {
                try
                {
                    status = 0;
                    _spinnerWatcher.ReportProgress(10); //65
                    var companyInfo = UserManager.GetCompanyInfos();
                    if (companyInfo == null) { return false; }
                    if (!companyInfo.Any()) { return false; }
                    if (companyInfo.Count != 1)
                    {
                        status = -1;
                        return false;
                    }
                    if (companyInfo[0].CompanyInfoId < 1)
                    {
                        return false;
                    }
                    return companyInfo[0].StationName.Length >= 3;
                }
                catch (Exception)
                {
                    status = -1;
                    return false;
                }
            }

            #endregion


        #endregion

        #region Background Stuffs

        private void InitSpinnerWatcher()
        {
            lblProgress.Visible = true;
            _spinnerWatcher = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            _spinnerWatcher.DoWork += SpinnerWatcherOnDoWork;
            _spinnerWatcher.RunWorkerCompleted += SpinnerWatcherOnRunWorkerCompleted;
            _spinnerWatcher.ProgressChanged += _spinnerWatcher_ProgressChanged;
            _spinnerWatcher.RunWorkerAsync();
        }

        void _spinnerWatcher_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                //mpsLoader.Value += e.ProgressPercentage;
                //mblPerc.Text = string.Format("{0}%", mpsLoader.Value);

            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);

            }
        }

        private void SpinnerWatcherOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {

            //mpsLoader.Value = 0;
            //mpsLoader.Visible = false;

            lblProgress.Visible = false;
            
            if (_isAllConfigSettingsOk)
            {
                LoadLoginForm();
                //return;
            }

            //Close();
            //Application.Exit();
            #region Locally Setup Flow

            //if (!_registerCompany)
            //{
            //    Close();
            //    Application.Exit();
            //    return;
            //}

            //loadRegistration();

            #endregion
            

        }

        private void SpinnerWatcherOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            CheckConfigurations();
        }

        #endregion

        #region >- Login

        private void LoadLoginForm()
        {
            bool flag;
            _frmLogin = new frmLogin(out flag);

            if (!flag)
            {
                return;
            }

            _frmLogin.UserLoginAction += UserLoginAction;
            SetLoginPosition();
            _frmLogin.SetPanels();
            _frmLogin.ShowDialog();

        }
        private void UserLoginAction(User user, StationInfo stationInfo, bool b)
        {
            if (!b)
            {
                Close();
                Application.Exit();
                return;
            }

            #region Newest

            //var thisAppConfig =
            //    ServiceProvider.Instance()
            //        .GetAppConfigSettingService()
            //        .GetAppConfigSetting(Utils.AppConfigSettingInfo.AppConfigSettingId);

            //if (thisAppConfig.AppConfigSettingId < 1)
            //{
            //    Application.Exit();
            //}

            //thisAppConfig.FirstTimeLaunch = false;
            //var retId = ServiceProvider.Instance()
            //    .GetAppConfigSettingService()
            //    .UpdateAppConfigSettingInfo(thisAppConfig);
            //if (!retId.IsSuccessful)
            //{
            //    Application.Exit();
            //}

            //Utils.AppConfigSettingInfo.FirstTimeLaunch = false;
            #endregion
            
            Utils.CurrentUser = user;
            Utils.StationInfo = stationInfo;
            var companyInfos = ServiceProvider.Instance().GetCorporateInfoService().GetCompanyInfos();
            Utils.CorporateInfo = companyInfos.Any() ? companyInfos[0] : new CompanyInfo();

            //if (user.IsFirstTimeLogin)
            //{
            //    LoadPasswordForm(user.UserName);
            //    return;
            //}

            #region Old Pattern

            //if (!CheckAppDB())
            //{
            //    MetroMessageBox.Show(this, _mssg.Length > 0 ? _mssg : "This application is unable to communicate with the registered database. Kindly ensure that all connection parameters are properly configured", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Close();
            //    Application.Exit();
            //    return;
            //}

            #endregion
            
            Close();


        }
        private void SetLoginPosition()
        {
            //if (_frmLogin == null) { return; }
            //_frmLogin.Size = new Size(Width, _frmLogin.Height);
            var mX = (Width - _frmLogin.Width) / 2;
            var mY = (Height - _frmLogin.Height) / 2;
            _frmLogin.Location = new Point(mX, mY);

        }
        #endregion


        #region Corporate Registration

        private void SetRegPanelPosition()
        {
            if (_frmCorporateRegistration == null) { return; }
            _frmCorporateRegistration.Size = new Size(Width, _frmCorporateRegistration.Height);
            var mX = (Width - _frmCorporateRegistration.Width) / 2;
            var mY = (Height - _frmCorporateRegistration.Height) / 2;
            _frmCorporateRegistration.Location = new Point(0, mY);

        }

        private void loadRegistration()
        {
            _frmCorporateRegistration = new frmCorporateRegistration();
            _frmCorporateRegistration.RegistrationStatusAction += RegistrationStatusAction;
            SetRegPanelPosition();
            _frmCorporateRegistration.SetPanels();
            _frmCorporateRegistration.ShowDialog();
        }


        private void RegistrationStatusAction(CompanyInfo companyInfo, User user, StationInfo stationInfo, bool b)
        {
            if (!b)
            {
                Close();
                Application.Exit();
                return;
            }


            Utils.CorporateInfo = companyInfo;
            Utils.CurrentUser = user;
            Utils.StationInfo = stationInfo;

            if (!CheckAppDB())
            {
                MetroMessageBox.Show(this, _mssg.Length > 0 ? _mssg : "This application is unable to communicate with the registered database. Kindly ensure that all connection parameters are properly configured", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                Application.Exit();
                return;
            }

            //Update local Corporate Info
            //var corporateInfo = new CorporateInfo
            //{
            //    StationName = companyInfo.StationName,
            //    Address = companyInfo.Address,
            //    StationKey = companyInfo.StationKey,
            //    HostServerAddress = companyInfo.HostServerAddress,
            //    PhoneNumbers = companyInfo.PhoneNumbers,
            //    MobileNumber = companyInfo.MobileNumber,
            //    TimeStampRegistered = DateMap.CurrentTimeStamp(),
            //};

            string msg;
            //var retVal = ServiceProvider.Instance().GetCorporateInfoServices().AddCorporateInfo(corporateInfo, out msg);
            _frmCorporateRegistration.Close();
            _frmCorporateRegistration.Visible = false;


            Close();
        }

        #endregion


    }
}
