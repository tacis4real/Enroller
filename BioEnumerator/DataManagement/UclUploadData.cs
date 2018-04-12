using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioEnumerator.APIServer.APIObjs;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;
using BioEnumerator.DataAccessManager.Service.Contract;
using BioEnumerator.GSetting;
using XPLUG.WINDOWTOOLS;
using XPLUG.WINDOWTOOLS.Extensions;

namespace BioEnumerator.DataManagement
{
    public partial class UclUploadData : UserControl
    {

        #region Vars

        private BackgroundWorker _bgWorker;
        private BackgroundWorker _bgWorkerDropDown;


        private string _surname, _mobileNo;
        //private DateTime _startDate, _stopDate;
        private string _startDate, _stopDate, _errorMessage;
        private int _stateId, _localAreaId, _status, _sexId;
        private int _uploadCount, _failedCount;
        private List<State> _states;
        private List<LocalArea> _localAreas;
        private List<NameAndValueObject> _gender;
        private List<MaritalStatus> _maritalStatus;
        private List<Occupation> _occupations;
        private List<BeneficiaryObj> _beneficiaryObjs;
        private List<ErrorMsgObj> _processErrorList;
        private List<BeneficiaryObj> _selectedItems;
        private bool _firstLoad = true;


        #endregion
        
        public UclUploadData()
        {
            InitializeComponent();
            _states = new List<State>();
            _localAreas = new List<LocalArea>();
            _gender = new List<NameAndValueObject>();
            _beneficiaryObjs = new List<BeneficiaryObj>();
            _processErrorList = new List<ErrorMsgObj>();
            EventHooks();
            InitDropDowns();
        }


        private void EventHooks()
        {
            cmbState.SelectedIndexChanged += cmbState_SelectedIndexChanged;
            cmbLocalArea.Enabled = false;
            btnSave.Enabled = false;
            btnSave.Click += btnSave_Click;
            btnReset.Click += btnReset_Click;
            //btnErrorView.Click += btnErrorView_Click;
            chkAll.CheckedChanged += chkAll_CheckedChanged;
            lblProgress.Visible = false;
            btnSearch.Click += btnSearch_Click;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedItems = _beneficiaryObjs.FindAll(m => m.IsSelected);
                if (_selectedItems.Count < 1)
                {
                    MessageBox.Show(@"You must select at least 1 record to upload", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                //_localAreaId = (bdsBusinessLocation.Current as BusinessLocation ?? new BusinessLocation()).LocalAreaId;
                //_stateId = int.Parse(cmbState.SelectedValue.ToString());
                SaveData();

            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                MessageBox.Show(@"Error Occurred! Please try again later", @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void btnReset_Click(object sender, EventArgs e)
        {
            ResetVals();
        }


        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {

            if (!_beneficiaryObjs.Any()) { return; }
            bdsBeneficiary.DataSource = new List<BeneficiaryObj>();
            _beneficiaryObjs.ForEachx(m => { m.IsSelected = chkAll.Checked; });
            bdsBeneficiary.DataSource = _beneficiaryObjs;
            dvgItem.ClearSelection();
        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                
                //if (cmbGender.SelectedValue != null)
                //{
                //    if (int.Parse(cmbGender.SelectedValue.ToString()) > 0)
                //    {
                //        _sexId = int.Parse(cmbGender.SelectedValue.ToString());
                //    }
                //}
                if (cmbState.SelectedValue != null)
                {
                    if (int.Parse(cmbState.SelectedValue.ToString()) > 0)
                    {
                        _stateId = int.Parse(cmbState.SelectedValue.ToString());
                    }
                }
                if (cmbLocalArea.SelectedValue != null)
                {
                    if (int.Parse(cmbLocalArea.SelectedValue.ToString()) > 0)
                    {
                        _localAreaId = int.Parse(cmbLocalArea.SelectedValue.ToString());
                    }
                }

                if (rbbAll.Checked)
                {
                    _status = 0;
                }
                else if (rbbEdited.Checked)
                {
                    _status = 3;
                }
                else if (rbbFailed.Checked)
                {
                    _status = -1;
                }
                else if (rbbFresh.Checked)
                {
                    _status = 1;
                }
                _surname = txtSurname.Text.Trim();
                _mobileNo = txtMobileNo.Text.Trim();

                SearchData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Unable to generate report! Please check parameters and try again", @"Search Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }
        }
        public void SetPanel()
        {

            var mX = (pnlBody.Width - pnlItemBody.Width) / 2;
            pnlItemBody.Location = new Point(mX, pnlItemBody.Location.Y);

            var mX1 = (pnlItemBody.Width - pnlFrame.Width) / 2;
            pnlFrame.Location = new Point(mX1, pnlFrame.Location.Y);

        }
        void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {

            bdsLocalArea.DataSource = new List<LocalArea>();
            cmbLocalArea.Enabled = false;

            if (cmbState.SelectedValue == null) { return; }
            if (int.Parse(cmbState.SelectedValue.ToString()) < 1) return;
            var filteredLocalAreas =
                _localAreas.FindAll(m => m.StateId == int.Parse(cmbState.SelectedValue.ToString()));

            filteredLocalAreas.Insert(0, new LocalArea { LocalAreaId = 0, Name = @"-- Please Select --" });
            bdsLocalArea.DataSource = new List<LocalArea>();
            bdsLocalArea.DataSource = filteredLocalAreas;
            cmbLocalArea.Enabled = true;
        }


        private void ResetVals()
        {
            _surname = "";
            _mobileNo = "";
            _stateId = 0;
            _localAreaId = 0;
            _failedCount = 0;
            _uploadCount = 0;
            _beneficiaryObjs = new List<BeneficiaryObj>();
            bdsBeneficiary.DataSource = new List<BeneficiaryObj>();

            if (cmbLocalArea.Enabled)
            {
                cmbLocalArea.SelectedIndex = 0;
            }
            cmbState.SelectedIndex = 0;
            cmbLocalArea.Enabled = false;
            txtSurname.Text = "";
            txtMobileNo.Text = "";
            btnSearch.Enabled = true;
            btnSave.Enabled = false;
            btnErrorView.Visible = false;
        }


        #region Save Data Stuffs
        private void SaveData()
        {
            lblProgress.Visible = true;
            _bgWorker = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            _bgWorker.DoWork += DataSaveOnDoWork;
            _bgWorker.RunWorkerCompleted += DataSaveOnRunWorkerCompleted;
            _bgWorker.RunWorkerAsync();
        }
        private void DataSaveOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            try
            {
                lblProgress.Visible = false;
                btnErrorView.Visible = _processErrorList.Any();

                if (!string.IsNullOrEmpty(_errorMessage))
                {
                    MessageBox.Show(_errorMessage, @"Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                bdsBeneficiary.DataSource = new List<BeneficiaryObj>();
                bdsBeneficiary.DataSource = _beneficiaryObjs;

                dvgItem.ClearSelection();
                btnSave.Enabled = _beneficiaryObjs.Any();
                btnSearch.Enabled = true;
                cmbState.SelectedIndex = 0;
                cmbLocalArea.Enabled = false;

                if (cmbLocalArea.Enabled)
                {
                    cmbLocalArea.SelectedIndex = 0;
                }

                _beneficiaryObjs = new List<BeneficiaryObj>();
                bdsBeneficiary.DataSource = new List<BeneficiaryObj>();

                if (cmbLocalArea.Enabled)
                {
                    cmbLocalArea.SelectedIndex = 0;
                }
                cmbState.SelectedIndex = 0;
                cmbLocalArea.Enabled = false;
                txtSurname.Text = "";
                txtMobileNo.Text = "";
                btnSearch.Enabled = true;
                btnSave.Enabled = false;
                btnErrorView.Visible = false;

                _surname = "";
                _mobileNo = "";
                _stateId = 0;
                _localAreaId = 0;


                MessageBox.Show(string.Format("Process Completed! {0} record(s) was(were) uploaded {1} record(s) failed", _uploadCount, _failedCount), @"Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                MessageBox.Show(@"Post Process Error Occured!", @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }
        private void DataSaveOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            try
            {
                _failedCount = 0;
                _uploadCount = 0;
                var bulkList = new List<BeneficiaryRegMinObj>();
                _selectedItems.ForEachx(m =>
                {
                    bulkList.Add(new BeneficiaryRegMinObj
                    {
                        BeneficiaryId = m.BeneficiaryId,
                        StateId = m.StateId,
                        LocalAreaId = m.LocalAreaId,
                        Surname = m.Surname,
                        FirstName = m.Firstname,
                        Othernames = m.Othernames,
                        DateOfBirth = m.DateOfBirth,
                        ResidentialAddress = m.ResidentialAddress,
                        MobileNumber = m.MobileNumber,
                        OfficeAddress = m.OfficeAddress,
                        Sex = m.Sex,
                        MaritalStatus = m.MaritalStatus,
                        OccupationId = m.OccupationId,
                        Status = m.Status,
                        Image = m.Image,
                        ImageByteArray = m.ImageByteArray,
                        ImageFileName = m.ImageFileName,
                        ImageByteString = m.ImageByteString,
                        FingerPrintTemplate = m.FingerPrintTemplates
                    });
                    
                });
                
                var myObj = new BulkBeneficiaryRegObj
                {
                    LocalAreaId = _localAreaId,
                    BeneficiaryRegObjs = bulkList
                };

                var retVal = ServiceProvider.Instance().GetBeneficiaryService().UploadBulkData(myObj, Utils.UploadStationInfo);
                if (!retVal.MainStatus.IsSuccessful)
                {
                    _errorMessage = @"Error: " + (string.IsNullOrEmpty(retVal.MainStatus.Message.FriendlyMessage) ? " Process Failed! Please try again later" : retVal.MainStatus.Message.FriendlyMessage);
                    return;
                }

                var savedItems = new List<long>();
                var errorList = new List<ErrorMsgObj>();

                foreach (var item in retVal.BeneficiaryRegResponses)
                {
                    if (item.Status.IsSuccessful)
                    {
                        savedItems.Add(item.BeneficiaryId);
                    }
                    else
                    {
                        errorList.Add(new ErrorMsgObj
                        {
                            Description = "Validation Error",
                            Detail = item.Status.Message.FriendlyMessage,
                            Title = item.MobileNumber
                            //Title = item.BusinessName + " " + item.MobileNumber
                        });
                    }
                }

                _uploadCount = savedItems.Count;
                _failedCount = _selectedItems.Count - savedItems.Count;

                _beneficiaryObjs =
                    _beneficiaryObjs.FindAll(
                        m => !savedItems.Contains(m.BeneficiaryId));

                _processErrorList = errorList;

            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _errorMessage = @"Error: " + ex.Message;
            }
        }

        #endregion

        #region Background Stuffs
        private void SearchData()
        {
            lblProgress.Visible = true;
            _bgWorker = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            _bgWorker.DoWork += SpinnerWatcherOnDoWork;
            _bgWorker.RunWorkerCompleted += SpinnerWatcherOnRunWorkerCompleted;
            _bgWorker.RunWorkerAsync();
        }
        private void SpinnerWatcherOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            try
            {
                lblProgress.Visible = false;
                if (_beneficiaryObjs == null || !_beneficiaryObjs.Any())
                {
                    bdsBeneficiary.DataSource = new List<BeneficiaryObj>();
                    dvgItem.ClearSelection();
                    MessageBox.Show(@"Search Completed! No Record Found", @"Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bdsBeneficiary.DataSource = new List<BeneficiaryObj>();
                bdsBeneficiary.DataSource = _beneficiaryObjs;
                dvgItem.ClearSelection();
                btnSave.Enabled = true;
                MessageBox.Show(_beneficiaryObjs.Count + @" Matching Record(s) Found", @"Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }

        }
        private void SpinnerWatcherOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            _beneficiaryObjs =
                ServiceProvider.Instance()
                    .GetBeneficiaryService()
                    .GetBusinessInfoReportDetail(_surname, _mobileNo, _stateId, _localAreaId, _status);
        }

        #endregion

        #region DropDowns
        #region Background Stuffs
        private void InitDropDowns()
        {
            lblProgress.Visible = true;
            _bgWorkerDropDown = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            _bgWorkerDropDown.DoWork += DropDownOnDoWork;
            _bgWorkerDropDown.RunWorkerCompleted += DropDownOnRunWorkerCompleted;
            _bgWorkerDropDown.RunWorkerAsync();
        }
        private void DropDownOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            try
            {
                lblProgress.Visible = false;

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

                //if (_gender == null || !_gender.Any())
                //{
                //    MessageBox.Show(@"Gender list is empty!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                _states.Insert(0, new State { StateId = 0, Name = @"-- Please Select --" });
                bdsState.DataSource = new List<State>();
                bdsState.DataSource = _states;
                cmbState.SelectedIndex = 0;

                //_gender.Insert(0, new NameAndValueObject { Id = 0, Name = @"-- Please Select --" });
                //bdsGender.DataSource = new List<NameAndValueObject>();
                //bdsGender.DataSource = _gender;
                //cmbGender.SelectedIndex = 0;

                _firstLoad = false;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }

        }
        private void DropDownOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            _states = ServiceProvider.Instance().GetStateService().GetStates().OrderBy(m => m.Name).ToList();
            _localAreas = ServiceProvider.Instance().GetLocalAreaService().GetLocalAreas().ToList();
            //_localAreas = ServiceProvider.Instance().GetLocalAreaService().GetLocalAreas().OrderBy(m => m.Name).ToList();
            //_gender = new List<NameAndValueObject>
            //{
            //    new NameAndValueObject{ Id = 1, Name = "Male"},
            //    new NameAndValueObject{ Id = 2, Name = "Female"}
            //}.ToList();
        }

        #endregion
        #endregion




    }
}
