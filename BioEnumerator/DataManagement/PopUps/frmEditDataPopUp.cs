using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioEnumerator.APIServer.APIObjs;
using BioEnumerator.DataAccessManager.CommonUtils;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;
using BioEnumerator.DataAccessManager.Service.Contract;
using BioEnumerator.GSetting;
using Camera_NET;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataManagement.PopUps
{
    public partial class frmEditDataPopUp : Form
    {

        #region Header
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        void lblHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        void btnHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        #region Vars

        private BeneficiaryObj _beneficiaryObj;

        private BackgroundWorker _bgWorker;
        private List<State> _states;
        private List<LocalArea> _localAreas;
        private List<MaritalStatus> _maritalStatus;
        private List<NameAndValueObject> _marital;
        private List<Occupation> _occupations;

        private bool _firstLoad = true;

        #endregion


        public frmEditDataPopUp(BeneficiaryObj item)
        {
            _beneficiaryObj = item;
            InitializeComponent();
            EventHooks();
            InitDropDowns();
        }
        
        private void EventHooks()
        {
            //Load += frmCaptureImage_Load;
            //Closed += frmCaptureImage_Closed;
            btnHeader.MouseMove += btnHeader_MouseMove;
            cmbState.SelectedIndexChanged += cmbState_SelectedIndexChanged;
            btnCloseWind.Click += btnCloseWind_Click;
            btnUpdate.Click += btnUpdate_Click;
        }

        
        
        
        void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateSubmission())
                return;
            try
            {

                var updateBeneficiary =
                    ServiceProvider.Instance()
                        .GetBeneficiaryService()
                        .GetBeneficiary(_beneficiaryObj.BeneficiaryId);

                if (updateBeneficiary == null || updateBeneficiary.BeneficiaryId < 1)
                {
                    MessageBox.Show(@"Process Error! Unable to retrieve the corresponding record to update", @"Process Error", MessageBoxButtons.OK,
                      MessageBoxIcon.Exclamation);
                    return;
                }

                updateBeneficiary.Surname = txtSurname.Text.Trim();
                updateBeneficiary.FirstName = txtFirstName.Text.Trim();
                updateBeneficiary.Othernames = txtOtherName.Text.Trim();
                updateBeneficiary.DateOfBirth = dtpDateOfBirth.Value.ToString("yyyy/MM/dd");
                updateBeneficiary.MobileNumber = txtMobileNo.Text.Trim();
                updateBeneficiary.Sex = rbbMale.Checked ? 1 : 2;
                updateBeneficiary.StateId = int.Parse(cmbState.SelectedValue.ToString());
                updateBeneficiary.LocalAreaId = int.Parse(cmbLocalArea.SelectedValue.ToString());
                updateBeneficiary.MaritalStatus = int.Parse(cmbMaritalStatus.SelectedValue.ToString());
                updateBeneficiary.OccupationId = int.Parse(cmbOccupation.SelectedValue.ToString());
                updateBeneficiary.ResidentialAddress = txtHomeAddress.Text.Trim();
                updateBeneficiary.OfficeAddress = txtOfficeAddress.Text.Trim();
                updateBeneficiary.Status = RegStatus.Edited;

                string msg;
                var retVal = ServiceProvider.Instance().GetBeneficiaryService().UpdateBeneficiary(updateBeneficiary, out msg);
                if (!retVal)
                {
                    MessageBox.Show(@"Error: " + (string.IsNullOrEmpty(msg) ? " Update Failed! Please try again later" : msg), @"Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation);
                    return;
                }

                MessageBox.Show(@"Process Completed! Data was updated successfully", @"Process Completed", MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;

                Close();

            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                MessageBox.Show(@"Error Occurred! Please try again later", @"Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation);
            }
        }
        

        void btnCloseWind_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void bindItems()
        {
            try
            {
                txtMobileNo.Text = _beneficiaryObj.MobileNumber;
                txtSurname.Text = _beneficiaryObj.Surname;
                txtFirstName.Text = _beneficiaryObj.Firstname;
                txtOtherName.Text = _beneficiaryObj.Othernames;
                txtHomeAddress.Text = _beneficiaryObj.ResidentialAddress;
                txtOfficeAddress.Text = _beneficiaryObj.OfficeAddress;
                dtpDateOfBirth.Value = DateTime.Parse(_beneficiaryObj.DateOfBirth);

                if (_beneficiaryObj.Sex == 1)
                {
                    rbbMale.Checked = true;
                }
                else if (_beneficiaryObj.Sex == 2)
                {
                    rbbFemale.Checked = true;
                }

                if (_beneficiaryObj.OccupationId > 0)
                {
                    cmbOccupation.SelectedValue = _beneficiaryObj.OccupationId;
                }
                if (_beneficiaryObj.MaritalStatus > 0)
                {
                    cmbMaritalStatus.SelectedValue = _beneficiaryObj.MaritalStatus;
                }
                _firstLoad = false;
                //_firstTime = true;
                if (_beneficiaryObj.StateId > 0)
                {
                    cmbState.SelectedValue = _beneficiaryObj.StateId;
                }

                if (_beneficiaryObj.LocalAreaId > 0)
                {
                    cmbLocalArea.SelectedValue = _beneficiaryObj.LocalAreaId;
                }

                //enableDisableControls();

            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                MessageBox.Show(@"Error Occurred! Please try again later", @"Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation);
            }
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

                bindItems();
                _firstLoad = false;
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

        #region Validation

        private bool ValidateSubmission()
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
                //if (picImage.Image != null)
                    return true;
                //MessageBox.Show(@"Please take the image of the person", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //return false;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                MessageBox.Show(@"Unable to validate your inputs. Please check all inputs and try again later", @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        #endregion
        
    }
}
