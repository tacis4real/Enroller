using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioEnumerator.APIServer.APIObjs;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;
using BioEnumerator.DataAccessManager.Service.Contract;
using BioEnumerator.DataManagement.PopUps;
using XPLUG.WINDOWTOOLS;
using XPLUG.WINDOWTOOLS.Controls.DefaultEnums;

namespace BioEnumerator.DataManagement
{
    public partial class UclLocalDataReport : UserControl
    {

        #region Vars

        private BackgroundWorker _bgWorker;
        private BackgroundWorker _bgWorkerDropDown;


        private string _surname, _mobileNo;
        //private DateTime _startDate, _stopDate;
        private string _startDate, _stopDate;
        private int _stateId, _localAreaId, _sexId; 
        private List<State> _states;
        private List<LocalArea> _localAreas;
        private List<NameAndValueObject> _gender;
        private List<MaritalStatus> _maritalStatus;
        private List<Occupation> _occupations;
        private List<BeneficiaryObj> _beneficiaryObjs;
        private bool _firstLoad = true;


        #endregion


        public UclLocalDataReport()
        {
            InitializeComponent();
            _states = new List<State>();
            _localAreas = new List<LocalArea>();
            _gender = new List<NameAndValueObject>();
            _beneficiaryObjs = new List<BeneficiaryObj>();
            lblProgress.Visible = false;

            EventHooks();
            InitDropDowns();
        }

        private void EventHooks()
        {
            cmbState.SelectedIndexChanged += cmbState_SelectedIndexChanged;
            cmbLGA.Enabled = false;
            btnSearch.Click += btnSearch_Click;
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            
            try
            {
                _surname = txtSurname.Text.Trim();
                _mobileNo = txtMobileNo.Text.Trim();
                //_startDate = dtpStartDate.Value.Date;
                //_stopDate = dtpStopDate.Value.Date;

                //DateTime.ParseExact(dateString, "ddMMyyyy", CultureInfo.InvariantCulture);

                //DateTime.ParseExact(dateString, "yyyy/MM/dd");
                _startDate = dtpStartDate.Value.ToString("yyyy/MM/dd");
                _stopDate = dtpStopDate.Value.ToString("yyyy/MM/dd");

                if (cmbGender.SelectedValue != null)
                {
                    if (int.Parse(cmbGender.SelectedValue.ToString()) > 0)
                    {
                        _sexId = int.Parse(cmbGender.SelectedValue.ToString());
                    }
                }
                if (cmbState.SelectedValue != null)
                {
                    if (int.Parse(cmbState.SelectedValue.ToString()) > 0)
                    {
                        _stateId = int.Parse(cmbState.SelectedValue.ToString());
                    }
                }
                if (cmbLGA.SelectedValue != null)
                {
                    if (int.Parse(cmbLGA.SelectedValue.ToString()) > 0)
                    {
                        _localAreaId = int.Parse(cmbLGA.SelectedValue.ToString());
                    }
                }

                SearchData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Unable to generate report! Please check parameters and try again", @"Search Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }


        }


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
                _stateId = 0;
                _localAreaId = 0;
                _surname = "";
                _mobileNo = "";
                _startDate = "";
                _stopDate = "";
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
                    .GetBusinessInfoReportDetail(_startDate, _stopDate, _surname, _mobileNo, _sexId, _stateId, _localAreaId);
            //tartDate, stopDate, surname, mobileNo, sexId, stateId, localAreaId
        }

        #endregion


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
            cmbLGA.Enabled = false;

            if (cmbState.SelectedValue == null) { return; }
            if (int.Parse(cmbState.SelectedValue.ToString()) < 1) return;
            var filteredLocalAreas =
                _localAreas.FindAll(m => m.StateId == int.Parse(cmbState.SelectedValue.ToString()));

            filteredLocalAreas.Insert(0, new LocalArea { LocalAreaId = 0, Name = @"-- Please Select --" });
            bdsLocalArea.DataSource = new List<LocalArea>();
            bdsLocalArea.DataSource = filteredLocalAreas;
            cmbLGA.Enabled = true;
        }




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

                if (_gender == null || !_gender.Any())
                {
                    MessageBox.Show(@"Gender list is empty!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _states.Insert(0, new State { StateId = 0, Name = @"-- Please Select --" });
                bdsState.DataSource = new List<State>();
                bdsState.DataSource = _states;
                cmbState.SelectedIndex = 0;
                
                _gender.Insert(0, new NameAndValueObject { Id = 0, Name = @"-- Please Select --" });
                bdsGender.DataSource = new List<NameAndValueObject>();
                bdsGender.DataSource = _gender;
                cmbGender.SelectedIndex = 0;

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
            _gender = new List<NameAndValueObject>
            {
                new NameAndValueObject{ Id = 1, Name = "Male"},
                new NameAndValueObject{ Id = 2, Name = "Female"}
            }.ToList();
        }

        #endregion
        #endregion

    }
}
