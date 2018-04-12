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
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.Service.Contract;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator
{
    public partial class frmCorporateRegistration : Form
    {

        public Action<CompanyInfo, User, StationInfo, bool> RegistrationStatusAction;

        public frmCorporateRegistration()
        {
            InitializeComponent();
            eventHandler();
        }

        private void eventHandler()
        {
            btnContinue.Click += btnContinue_Click;
            btnPrevious.Click += btnPrevious_Click;
            btnCancel.Click += btnCancel_Click;
            btnSave.Click += btnSave_Click;
        }
        

        #region Events

        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateSubmission())
                    return;
                Save();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                MessageBox.Show(@"Unable to save ", @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"This will close the application! Are you sure?", @"Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            if (RegistrationStatusAction != null)
                RegistrationStatusAction(null, null, null, false);
            Close();
        }

        void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                pnlCorporate.Visible = true;
                pnlAdmin.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                MessageBox.Show(@"Error Occurred! Please try again later", @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidatePageOne())
                    return;
                pnlAdmin.Visible = true;
                pnlCorporate.Visible = false;
                
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                MessageBox.Show(@"Error Occurred! Please try again later", @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        
        #endregion

        #region Validations

        private bool ValidatePageOne()
        {
            try
            {
                //if (txtStationName.Text.Length < 5)
                //{
                //    MessageBox.Show(@"Station Name is required and must be more than 5 character long", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //    return false;
                //}
                if (txtStationKey.Text == "")
                {
                    MessageBox.Show(@"Station ID is required", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                //if (txtStationKey.Text.Length != 15)
                //{
                //    MessageBox.Show(@"Invalid Station Key", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //    return false;
                //}
                if (txtHostServer.Text == "")
                {
                    MessageBox.Show(@"Host Server is required", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                //if (txtMobileNumber.Text.Length < 7)
                //{
                //    MessageBox.Show(@"Kindly provide your company's Mobile Number", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //    return false;
                //}
                //if (!GSMHelper.ValidateMobileNumber(txtMobileNumber.Text))
                //{
                //    MessageBox.Show(@"Invalid Station Mobile Number", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //    return false;
                //}
                //if (txtAddress.Text.Length >= 5)
                //    return true;
                //MessageBox.Show(@"Invalid / Empty Station Address", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return true;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                MessageBox.Show(@"Unable to validate your inputs. Please check all inputs and try again later", @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        private bool ValidateSubmission()
        {
            try
            {
                //if (txtStationName.Text.Length < 5)
                //{
                //    MessageBox.Show(@"Station Name is required and must be more than 5 character long", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //    return false;
                //}
                if (txtStationKey.Text == "")
                {
                    MessageBox.Show(@"Station ID is required", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                if (txtSurname.Text.Length < 3)
                {
                    MessageBox.Show(@"Kindly provide the Administrator's Surname", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                if (txtFirstName.Text.Length < 3)
                {
                    MessageBox.Show(@"Kindly provide the Administrator's First Name", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                if (txtUsername.Text.Length < 8)
                {
                    MessageBox.Show(@"Kindly provide the Administrator's Username", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                if (txtPassword.Text.Length != 8)
                {
                    MessageBox.Show(@"Invalid Administrator's login Password. Password must be exactly 8 character long: 6 number digits and 2 special characters", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                {
                    MessageBox.Show(@"Password & Confirm Password must match", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                if (txtAdminAddress.Text.Length < 10)
                {
                    MessageBox.Show(@"Invalid / Admin Residential Address", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                if (txtAdminMobileNo.Text.Length < 7)
                {
                    MessageBox.Show(@"Kindly provide Admin's Mobile Number", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                //if (!GSMHelper.ValidateMobileNumber(txtAdminMobileNo.Text))
                //{
                //    MessageBox.Show(@"Invalid Admin Mobile Number", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //    return false;
                //}
                //if (txtAdminMobileNo.Text.Length >= 7 && txtAdminMobileNo.Text.Length <= 15)
                //{
                //    MessageBox.Show(@"Invalid Administrator's Mobile Number", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //    return false;
                //}
                if (txtEmail.Text == "")
                {
                    MessageBox.Show(@"Administrator's Email is required", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                if (RegExValidation.IsEmailValid(txtEmail.Text.Trim()))
                    return true;
                MessageBox.Show(@"Invalid Administrator's Email", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
                    
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                MessageBox.Show(@"Unable to validate your inputs. Please check all inputs and try again later", @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        #endregion


        #region Event Methods

        private bool Save()
        {
            string text = "";
            //if (!MigrationAssistance.Migrate(out text))
            //{
            //    int num = (int)MessageBox.Show(text);
            //    return false;
            //}
            try
            {
                var companyInfo = new CompanyInfo
                {
                    StationName = (txtStationName.Text.Trim() == "" ? "Station Name" : txtStationName.Text.Trim()),
                    StationKey = txtStationKey.Text.Trim(),
                    HostServerAddress = txtHostServer.Text.Trim(),
                    //MobileNumber = txtMobileNumber.Text.Trim(),
                    //Address = txtAddress.Text.Trim(),
                    Status = chkStatus.Checked,
                    //PhoneNumbers = txtMobileNumber.Text.Trim()
                };

                var userProfile = new UserProfile
                {
                    Surname = txtSurname.Text.Trim(),
                    FirstName = txtFirstName.Text.Trim(),
                    OtherNames = txtFirstName.Text.Trim(),
                    ResidentialAddress = txtAdminAddress.Text.Trim(),
                    MobileNumber = txtAdminMobileNo.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    ProfileNumber = "EPP-002",
                    Sex = 1,
                    Status = 1,
                    TimeLastModified = DateTime.Now.ToString("hh:mm:ss tt"),
                    DateLastModified = DateTime.Now.ToString("yyyy/MM/dd"),
                    ModifiedBy = 1
                };
                
                var user = new User
                {
                    UserName = txtUsername.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    RoleId = 2,
                    IsApproved = true,
                    IsLockedOut = false,
                    FailedPasswordAttemptCount = 0,
                    LastLockedOutTimeStamp = "",
                    LastLoginTimeStamp = "",
                    LastPasswordChangedTimeStamp = "",
                    RegisteredDateTimeStamp = DateTime.Now.ToString("yyyy/MM/dd - hh:mm:ss tt")
                };

                var status = ServiceProvider.Instance()
                    .GetUserProfileService()
                    .CompanyAndUserProfile(companyInfo, userProfile, user);

                if (!status.IsSuccessful)
                {
                    MessageBox.Show(@"Process Failed!" + string.Format("\r\nInner Exception: " + @status.Message.FriendlyMessage), @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                StationInfo stationInfo;
                if (status.StationInfo == null)
                {
                    stationInfo = (ServiceProvider.Instance().GetStationInfoService().GetStationInfos() ?? new List<StationInfo>())[0];
                    if (stationInfo == null || stationInfo.StationInfoId < 1)
                    {
                        MessageBox.Show(@"Process Failed! Unable to load station information", @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return false;
                    }
                }
                else
                    stationInfo = status.StationInfo;
                companyInfo.CompanyInfoId = Convert.ToInt32(status.ReturnedId);
                user.UserId = status.UserId;
                if (RegistrationStatusAction != null)
                {
                    RegistrationStatusAction(companyInfo, user, stationInfo, true);
                }
                    
                MessageBox.Show(@"Process Completed! Station information was registered successfully", @"Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return true;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                MessageBox.Show(@"Error occurred while processing your request. Please try again later " + ex.GetBaseException().Message, @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        #endregion


        public void SetPanels()
        {
            var x = (Width - pnlCorporate.Width) / 2;
            var y = (Height - pnlCorporate.Height) / 2;
            
            pnlCorporate.Location = new Point(x, y);
            pnlAdmin.Location = new Point(x, y);
            //pnlAdmin.Location = new Point(-1 * pnlAdmin.Width, y);

            //pnlBody.Location = new Point((Width - pnlBody.Width) / 2, (Height - pnlBody.Height) / 2);
        }

        

    }
}
