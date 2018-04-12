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
using BioEnumerator.DataManagement.PopUps;
using DPFP.Verification;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataManagement
{
    public partial class UclEditData : UserControl
    {

        #region Vars

        private BackgroundWorker _bgWorker;
        private string _surname, _othernames, _mobileNo;
        private List<State> _states;
        private List<LocalArea> _localAreas;
        private List<MaritalStatus> _maritalStatus;
        private List<Occupation> _occupations;
        private List<BeneficiaryObj> _beneficiaryObjs;
        private List<BeneficiaryObj> _verifiedBeneficiaryObjs;
        private DPFP.FeatureSet _featureSet;
        private frmBusy _frmBusy;


        #endregion


        public UclEditData()
        {
            InitializeComponent();
            dvgItem.CellContentClick += DvgItemOnCellContentClick;
            dvgItem.RowHeaderMouseClick += dvgItem_RowHeaderMouseClick;
            btnSearch.Click += btnSearch_Click;
            progressBar.Style = ProgressBarStyle.Marquee;
            pnlProgress.Visible = false;
            Load += UclEditData_Load;
            verificationControl.OnComplete += OnComplete;
            chkVerify.Click += chkVerify_Click;
            //verificationControl.Visible = false;
            //rdbVerify.Click += rdbVerify_Click;
            _beneficiaryObjs = new List<BeneficiaryObj>();
            
        }

        void chkVerify_Click(object sender, EventArgs e)
        {
            if (!chkVerify.Checked)
            {
                pnlProgress.Visible = false;
            }
        }

        void UclEditData_Load(object sender, EventArgs e)
        {
            InitDropDowns();
        }

        

        void OnComplete(object control, DPFP.FeatureSet featureSet, ref DPFP.Gui.EventHandlerStatus eventHandlerStatus)
        {
            _featureSet = featureSet;
            Verification();
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            
            //if (cmbState.SelectedValue != null)
            //{
            //    if (int.Parse(cmbState.SelectedValue.ToString()) > 0)
            //    {
            //        _stateId = int.Parse(cmbState.SelectedValue.ToString());
            //    }
            //}
            //if (cmbLocalArea.SelectedValue != null)
            //{
            //    if (int.Parse(cmbLocalArea.SelectedValue.ToString()) > 0)
            //    {
            //        _localAreaId = int.Parse(cmbLocalArea.SelectedValue.ToString());
            //    }
            //}
            bdsBeneficiary.DataSource = new List<BeneficiaryObj>();
            
            //_verifiedBeneficiaryObjs = new List<BeneficiaryObj>();
            if (chkVerify.Checked)
            {
                //OnComplete();
                //_frmBusy = new frmBusy("Waiting for fingerprint ...");
                //_frmBusy.ShowDialog();


                //RunWorkerCompletedEventArgs taskResult = frmBusy.RunLongTask("Waiting for fingerprint ...", new DoWorkEventHandler(Verification), false, ((CData)lbDatabase.SelectedItem).EngineUser, new EventHandler(CancelScanningHandler));
                //verificationControl.OnComplete += OnComplete;

                //var ft = _featureSet;
                //if (ft == null)
                //{
                //    return;
                //}
                pnlProgress.Visible = true;
                //RunWorkerCompletedEventArgs taskResult = frmBusy.RunLongTask("Waiting for fingerprint ...", new DoWorkEventHandler(Verification), false);

            }
            else
            {
                _surname = txtSurname.Text.Trim();
                _mobileNo = txtMobileNo.Text.Trim();
                SearchData();
            }


            



        }



        public void SetPanel()
        {

            var mX = (pnlBody.Width - pnlItemBody.Width) / 2;
            pnlItemBody.Location = new Point(mX, pnlItemBody.Location.Y);

            var mX1 = (pnlItemBody.Width - pnlFrame.Width) / 2;
            pnlFrame.Location = new Point(mX1, pnlFrame.Location.Y);

        }


        #region Background Stuffs
        private void SearchData()
        {
            lblProgress.Visible = true;
            _bgWorker = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            _bgWorker.DoWork += SpinnerWatcherOnSearchDoWork;
            _bgWorker.RunWorkerCompleted += SpinnerWatcherOnRunWorkerSearchCompleted;
            _bgWorker.RunWorkerAsync();
        }
        private void SpinnerWatcherOnRunWorkerSearchCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
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
                MessageBox.Show(_beneficiaryObjs.Count + @" Matching Record(s) Found", @"Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }

        }
        private void SpinnerWatcherOnSearchDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            _beneficiaryObjs =
                ServiceProvider.Instance()
                    .GetBeneficiaryService()
                    .GetBusinessInfoReportDetail(_surname, _mobileNo);
        }

        #endregion

        #region Background Stuffs
        private void InitDropDowns()
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
                //lblProgress.Visible = false;
                if (_beneficiaryObjs == null || !_beneficiaryObjs.Any())
                {
                    MessageBox.Show(@"Search Completed! No Record Found", @"Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //bdsBeneficiary.DataSource = new List<BeneficiaryObj>();
                //bdsBeneficiary.DataSource = _beneficiaryObjs;
                //dvgItem.ClearSelection();
                //MessageBox.Show(_beneficiaryObjs.Count + @" Matching Record(s) Found", @"Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    .GetBeneficiarys();
        }
        #endregion
        
        #region Background Stuffs
        //private void Verification(object sender, DoWorkEventArgs args)
        //{
        //    lblProgress.Visible = true;
        //    _bgWorker = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
        //    _bgWorker.DoWork += VerifyOnDoWork;
        //    _bgWorker.RunWorkerCompleted += VerifyOnRunWorkerCompleted;
        //    _bgWorker.RunWorkerAsync();
        //}

        private void Verification()
        {
            lblProgress.Visible = true;
            _bgWorker = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            _bgWorker.DoWork += VerifyOnDoWork;
            _bgWorker.RunWorkerCompleted += VerifyOnRunWorkerCompleted;
            _bgWorker.RunWorkerAsync();
        }

        private void VerifyOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            try
            {
                lblProgress.Visible = false;
                pnlProgress.Visible = false;
                if (_verifiedBeneficiaryObjs == null || !_verifiedBeneficiaryObjs.Any())
                {
                    MessageBox.Show(@"Search Completed! No Record Found", @"Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bdsBeneficiary.DataSource = new List<BeneficiaryObj>();
                bdsBeneficiary.DataSource = _verifiedBeneficiaryObjs;
                dvgItem.ClearSelection();
                MessageBox.Show(_verifiedBeneficiaryObjs.Count + @" Matching Record(s) Found", @"Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }

        }
        private void VerifyOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {

            if (!_beneficiaryObjs.Any() || _beneficiaryObjs == null)
            {
                _verifiedBeneficiaryObjs = new List<BeneficiaryObj>();
                return;
            }

            _verifiedBeneficiaryObjs = new List<BeneficiaryObj>();
            var ver = new Verification();
            var res = new Verification.Result();

            foreach (var thisBeneficiary in _beneficiaryObjs)
            {
                if(thisBeneficiary == null || thisBeneficiary.BeneficiaryId < 1) continue;
                var fingerTemplates = thisBeneficiary.FingerTemplates;
                if(Array.TrueForAll(fingerTemplates, x => x.Equals(null))) continue;

                foreach (var fingerTemplate in fingerTemplates)
                {
                    if (fingerTemplate != null)
                    {
                        ver.Verify(_featureSet, fingerTemplate, ref res);
                        if (res.Verified)
                        {
                            _verifiedBeneficiaryObjs.Add(thisBeneficiary);
                            break; 
                        }
                    }
                }

                if (_verifiedBeneficiaryObjs.Any() && _verifiedBeneficiaryObjs.Count == 1)
                {
                    break;
                }
            }

        }

        #endregion



        #region MyRegion

        private void DvgItemOnCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                var myItem = dvgItem.Rows[e.RowIndex].DataBoundItem as BeneficiaryObj;
                LoadData(myItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Process failed! Please try again later", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }
        }
        
        void dvgItem_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void LoadData(BeneficiaryObj item)
        {
            var frm = new frmEditDataPopUp(item);
            if (frm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            //InitDropDowns();
        }
        #endregion


    }
}
