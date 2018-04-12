using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioEnumerator.APIServer.APIObjs;
using BioEnumerator.APIServer.DataLoader;
using BioEnumerator.CommonUtils;
using BioEnumerator.DataAccessManager.CommonUtils;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.Repository;
using BioEnumerator.DataAccessManager.Service;
using BioEnumerator.DataAccessManager.Service.Contract;
using BioEnumerator.GSetting;
using DPFP;
using DPFP.Gui.Verification;
using DPFP.Processing;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator
{
    public partial class Form1 : Form
    {

        string _api = ConfigurationManager.AppSettings["localAddr"];
        WebAPIHelper _apiHelper;
        private readonly VerificationControl _verificationControl;
        private FingerEnrollData.AppData _data;					// keeps application-wide data

        public Form1()
        {
            InitializeComponent();

            //SetFileAccess();
            //_verificationControl = new VerificationControl();
            //_verificationControl.OnComplete += _verificationControl_OnComplete;
            //Intialiazer();
            EventHooks();
           

        }


        private void SetFileAccess()
        {
            var basePath = InternetCon.GetBasePath() + "\\AppFiles";
            //var resourceFiles = InternetCon.GetFromResources(basePath + "\\AppFiles");

            CustomHelper.AccessControl(basePath);
        }


        #region EventDefs

        private void EventHooks()
        {
            //VerificationControl.OnComplete += VerificationControl_OnComplete;
            //_verificationControl.OnComplete += OnComplete;
            btnImage.Click += btnImage_Click;
        }

        void btnImage_Click(object sender, EventArgs e)
        {
            var ben = ServiceProvider.Instance().GetBeneficiaryService().GetBeneficiaryObj(1);
            picImage.Image = new Bitmap(CustomHelper.ConvertBase64StringToImage(ben.ImageByteString));
            picImage.SizeMode = PictureBoxSizeMode.StretchImage;

            //RetrieveImage();
        }


        private void RetrieveImage()
        {


            #region Folder Structure For Storage

            var stationKey = (ServiceProvider.Instance().GetStationInfoService().GetStationInfos() ?? new List<StationInfo>())[0].StationKey;
            var username = "useradmin";
            var year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);

            #endregion

            const string imgName = "636549040395366983";
            const string fileName = imgName + ".jpeg";
            //var fileName = imgName;
            //TODO:Resourse path
            string folderPath = "/Station-" + stationKey + "/" + username + "/" + year + "/" + "/Image/";
            var imageResPath = folderPath + fileName;
            var dir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BioResource"];
            var imagePath = Path.GetFullPath(dir + imageResPath);

            var imageFileName = Path.GetFileName(imagePath);

            //var img = Image.FromFile(imagePath);
            using (var img = Image.FromFile(imagePath))
            {
                picImage.Image = new Bitmap(img);
            }

            
        }






        

        #endregion

        void VerificationControl_OnComplete(object control, FeatureSet featureSet, ref DPFP.Gui.EventHandlerStatus eventHandlerStatus)
        {
            // Verify Here
            var ver = new DPFP.Verification.Verification();
            var res = new DPFP.Verification.Verification.Result();
            var fingerTemplates = new BeneficiaryRepository().GetFingerTemplates();

            // Compare feature set with all stored templates.


            foreach (var template in fingerTemplates)
            {
                // Get template from storage.
                if (template != null)
                {
                    // Compare feature set with particular template.
                    ver.Verify(featureSet, template, ref res);
                    //_data.IsFeatureSetMatched = res.Verified;
                    //_data.FalseAcceptRate = res.FARAchieved;
                    if (res.Verified)
                        break; // success
                }
            }

            if (!res.Verified)
                eventHandlerStatus = DPFP.Gui.EventHandlerStatus.Failure;

            //_data.Update();
        }

       



        private void Intialiazer()
        {
            try
            {


                var beneficiaryBiometrics = new BeneficiaryRepository().GetBeneficiarys();
                if (!beneficiaryBiometrics.Any())
                {

                }


                //var beneficiary = new BeneficiaryRepository().GetBeneficiary(1);
                //if (beneficiary == null || beneficiary.BeneficiaryId < 1)
                //{

                //}


                var pathString = ConfigurationManager.AppSettings["BioResource"];
                var appDir = InternetCon.GetBasePath();
                var root = Path.GetPathRoot(pathString);
                var imagePath = Path.GetFullPath(appDir + pathString);


                #region Verification
                #endregion




                var bioMetricInfo = new BeneficiaryRepository().GetBeneficiaryBiometric(1);

                var companyInfo = new CorporateInfoService().GetCompanyInfos();
                if (companyInfo.Any())
                {
                    MessageBox.Show(@"Database Initializer", @"Configuration");
                }

                var userList = new UserService().GetUsers();
                if (userList.Any())
                {
                    MessageBox.Show(@"Database Initializer", @"Configuration");
                }


                var station = new StationInfoService().GetStationInfos();
                if (station.Any())
                {
                    MessageBox.Show(@"Database Initializer", @"Configuration");
                }

                //var dir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BiometricResource"];
               

                var userProfiles = new UserProfileRepository().GetUserProfiles();
                if (userProfiles.Any())
                {
                    MessageBox.Show(@"Database Initializer", @"Configuration");
                }
                
                var localAreas = new LocalAreaRepository().GetLocalAreas();
                if (localAreas.Any())
                {
                    MessageBox.Show(@"Database Initializer", @"Configuration");
                }




                _apiHelper = new WebAPIHelper(_api);
                List<RegisteredUserReportObj> users = null;
                string reply = null;


                #region POST Request - Add User Details

                var roleList = new List<NameAndValueObject>
                {
                    new NameAndValueObject{ Id = 1, Name = "My Admin" }
                };

                var resp = new UserRegResponse();

                var selRoles = new[] { "1"}.ToList();
                var roleIds = new[] {1};

                var helper = new UserRegistrationObj
                {
                    ConfirmPassword = "January1986",
                    Email = "tacis4real@yahoo.com",
                    Othernames = "Adesoji",
                    Surname = "Ilesanmi",
                    MobileNumber = "08036975694",
                    MyRoleIds = roleIds,
                    MyRoles = selRoles.ToArray(),
                    Username = "tacis4real",
                    Password = "January1986",
                    SelectedRoles = string.Join(";", selRoles),
                    Sex = 1,
                    RegisteredByUserId = 1,
                };

                if (_apiHelper.AddStationUser(helper, ref reply, ref resp))
                {
                    if (resp == null || resp.UserId < 1) return;
                }

                #endregion


                //var userList = Controller.DownLoadStationUsers();

               

                if (_apiHelper.GetStationUsers(ref reply, ref users))
                {
                    if (users == null || !users.Any()) return;
                    var total = users.Count();
                }

                //var context = new BioEnumeratorEntities();
                //InternetCon.ProcessLookUpFromFiles(context);

                string msg;

                var delState = new StateRepository().DeleteState(37);
                if (delState.IsSuccessful)
                {
                    MessageBox.Show(@"State Deleted Successfully", @"Record Deletion");
                }

                //var intit = eDataAdminLite.API.MigrationAssistance.Migrate(out msg);
                //if (intit)
                //{
                //    MessageBox.Show(@"Database Initializer", @"Configuration");
                //}

                //var st = new StateRepository().GetState(37);
                //if (st != null && st.StateId > 0)
                //{
                //    st.Name = "Epay Plus Limited";
                //}

                //var updateState = new StateRepository().UpdateState(st);
                //if (updateState.IsSuccessful)
                //{
                //    MessageBox.Show(@"Database Initializer", @"State Update successfully");
                //}


                //var state = new State
                //{
                //    Name = "Epay Plus",
                //};
                //var retVal = new StateRepository().AddState(state);
                //if (retVal.IsSuccessful)
                //{
                //    MessageBox.Show(@"Database Initializer", @"Configuration");
                //}

                var states = new StateRepository().GetStates();
                if (states.Any())
                {
                    MessageBox.Show(@"Database Initializer", @"Configuration");
                }

                var roles = new RoleRepository().GetRoles();
                if (roles.Any())
                {
                    MessageBox.Show(@"Database Initializer", @"Configuration");
                }

                



                //var retVal = eDataAdminLite.API.UserManager.AddRole(role);
                //if (retVal.IsSuccessful)
                //{
                //    MessageBox.Show(@"Database Initializer", @"Configuration");
                //}

                //var roles = eDataAdminLite.API.UserManager.GetRoles();
                //if (!roles.Any())
                //{
                //    MessageBox.Show(@"Database Initializer", @"Configuration");
                //}

            }
            catch (Exception)
            {
                 throw;
            }

        }


        
    }
}
