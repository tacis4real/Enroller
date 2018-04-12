using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;
using BioEnumerator.DataAccessManager.Service.Contract;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataManagement
{
    public partial class UclDashboardCounter : UserControl
    {

        #region Vars

        private BackgroundWorker _bgWorker;
        private DashboardDataCount _dashboardDataCount;
        private bool _timerEnabled = false;


        #endregion

        public UclDashboardCounter()
        {
            InitializeComponent();
            _dashboardDataCount = new DashboardDataCount();
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            InitDropDowns();
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
                
                if (_dashboardDataCount == null)
                {
                    return;
                }

                lblErrorData.Text = _dashboardDataCount.Error.ToString();
                lblPendingData.Text = _dashboardDataCount.Pending.ToString();
                lblTotalData.Text = _dashboardDataCount.AllData.ToString();
                lblUploadedData.Text = _dashboardDataCount.Uploaded.ToString();

                if (!_timerEnabled)
                {
                    timer.Enabled = true;
                    _timerEnabled = true;
                }

            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }

        }
        private void SpinnerWatcherOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            _dashboardDataCount = ServiceProvider.Instance().GetBeneficiaryService().GetDashboardDataCount();
        }

        #endregion

    }
}
