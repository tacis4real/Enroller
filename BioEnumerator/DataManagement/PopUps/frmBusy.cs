using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioEnumerator.DataManagement.PopUps
{
    public partial class frmBusy : Form
    {

        #region Vars

        protected BackgroundWorker _worker = new BackgroundWorker();
        private object argument;
        private DoWorkEventHandler callback;
        private Exception error;
        private RunWorkerCompletedEventArgs results;
        #endregion


        public frmBusy(string title)
        {
            InitializeComponent();
            progressBar.Style = ProgressBarStyle.Marquee;
            lblOperation.Text = title;
            btnCancel.Click += btnCancel_Click;
        }


        private frmBusy(string title, DoWorkEventHandler callback, bool reportsProgress)
        {
            InitializeComponent();

            if (!reportsProgress)
                progressBar.Style = ProgressBarStyle.Marquee;

            SetExecutionText(title);
            this.callback = callback;
            //argument = args;
            _worker.WorkerReportsProgress = reportsProgress;
            _worker.DoWork += BusyForm_DoWork;
            _worker.RunWorkerCompleted += BusyForm_RunWorkerCompleted;
            _worker.ProgressChanged += BusyForm_ProgressChanged;

            //if (cancelHandler != null)
            //{
            //    _worker.WorkerSupportsCancellation = true;
            //    btnCancel.Click += cancelHandler;
            //    btnCancel.Click += PostOnCancelClick;
            //    btnCancel.Enabled = true;
            //    btnCancel.Visible = true;
            //}
        }



        private frmBusy(string title, DoWorkEventHandler callback, bool reportsProgress, object args, EventHandler cancelHandler)
		{
			InitializeComponent();

			if (!reportsProgress)
				progressBar.Style = ProgressBarStyle.Marquee;

			SetExecutionText(title);
			this.callback = callback;
			argument = args;
			_worker.WorkerReportsProgress = reportsProgress;
			_worker.DoWork += BusyForm_DoWork;
			_worker.RunWorkerCompleted += BusyForm_RunWorkerCompleted;
			_worker.ProgressChanged += BusyForm_ProgressChanged;

			if (cancelHandler != null)
			{
				_worker.WorkerSupportsCancellation = true;
				btnCancel.Click += cancelHandler;
				btnCancel.Click += PostOnCancelClick;
				btnCancel.Enabled = true;
				btnCancel.Visible = true;
			}
		}





        void PostOnCancelClick(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;
            btnCancel.Refresh();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


        public static RunWorkerCompletedEventArgs RunLongTask(string title, DoWorkEventHandler callback, bool reportsProgress)
        {
            using (var frmLongTask = new frmBusy(title, callback, reportsProgress))
            {
                frmLongTask.ShowDialog();
                if (frmLongTask.error != null)
                {
                    throw frmLongTask.error;
                }
                return frmLongTask.results;
            }
        }


        public static RunWorkerCompletedEventArgs RunLongTask(string title, DoWorkEventHandler callback, bool reportsProgress, object args, EventHandler cancelHandler)
        {
            using (var frmLongTask = new frmBusy(title, callback, reportsProgress, args, cancelHandler))
            {
                frmLongTask.ShowDialog();
                if (frmLongTask.error != null)
                {
                    throw frmLongTask.error;
                }
                return frmLongTask.results;
            }
        }



        #region Background worker
        private void BusyForm_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (callback != null)
                {
                    callback(sender, e);
                }
            }
            catch (Exception ex)
            {
                error = ex;
            }
        }

        private void BusyForm_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var text = e.UserState as string;
            if (text != null)
            {
                SetExecutionText(text);
            }

            SetExecutionValue(e.ProgressPercentage);
        }

        private void BusyForm_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            results = e;
            Close();
        }


        #endregion


        #region Setters/Getters
        public void SetExecutionText(string text)
        {
            try
            {
                if (lblOperation.InvokeRequired)
                {
                    lblOperation.Invoke((MethodInvoker)delegate()
                    {
                        lblOperation.Text = text;
                    });
                }
                else
                {
                    lblOperation.Text = text;
                }
            }
            finally
            {
            }
        }

        public void SetExecutionValue(int value)
        {
            try
            {
                if (progressBar.InvokeRequired)
                {
                    progressBar.Invoke((MethodInvoker)delegate()
                    {
                        progressBar.Value = value;
                    });
                }
                else
                {
                    progressBar.Value = value;
                }
            }
            finally
            {
            }
        }
        #endregion



    }
}
