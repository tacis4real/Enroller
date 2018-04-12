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
    public partial class frmUpdateHostSetting : Form
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

        private StationInfo _corporateInfo;
        private List<StationInfo> _corporateInfos;

        #endregion


        public frmUpdateHostSetting()
        {
            InitializeComponent();
            EventHooks();
            _corporateInfo = new StationInfo();
            _corporateInfos = new List<StationInfo>();
            LoadData();
        }
        
        private void EventHooks()
        {
            btnHeader.MouseMove += btnHeader_MouseMove;
            btnCloseWind.Click += btnCloseWind_Click;
            btnSave.Click += btnSave_Click;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateSubmission())
                return;
            try
            {

                var stationInfo =
                    ServiceProvider.Instance()
                        .GetStationInfoService()
                        .GetStationInfo(_corporateInfo.StationInfoId);

                if (stationInfo == null || stationInfo.StationInfoId < 1)
                {
                    MessageBox.Show(@"Process Error! Unable to retrieve the corresponding record to update", @"Process Error", MessageBoxButtons.OK,
                      MessageBoxIcon.Exclamation);
                    return;
                }

                stationInfo.APIAccessKey = (txtAccessKey.Text.Trim());
                stationInfo.HostServerAddress = (txtHostServer.Text.Trim());
                stationInfo.StationKey = (txtStationKey.Text.Trim());
                stationInfo.StationName = (txtStationName.Text.Trim());
                //stationInfo.MobileNumber = (txtMobileNumber.Text.Trim());
                //stationInfo.Address = (txtAddress.Text.Trim());

                string msg;
                var retVal = ServiceProvider.Instance().GetStationInfoService().UpdateStationInfo(stationInfo, out msg);
                if (!retVal)
                {
                    MessageBox.Show(@"Error: " + (string.IsNullOrEmpty(msg) ? " Update Failed! Please try again later" : msg), @"Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation);
                    return;
                }

                MessageBox.Show(@"Process Completed! Host Server Information was updated successfully", @"Process Completed", MessageBoxButtons.OK,
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
        
        void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateSubmission())
                return;
            try
            {
                //string msg;
                //var retVal = ServiceProvider.Instance().GetBeneficiaryService().UpdateBeneficiary(updateBeneficiary, out msg);
                //if (!retVal)
                //{
                //    MessageBox.Show(@"Error: " + (string.IsNullOrEmpty(msg) ? " Update Failed! Please try again later" : msg), @"Error", MessageBoxButtons.OK,
                //       MessageBoxIcon.Exclamation);
                //    return;
                //}

                //MessageBox.Show(@"Process Completed! Data was updated successfully", @"Process Completed", MessageBoxButtons.OK,
                //     MessageBoxIcon.Information);
                //DialogResult = DialogResult.OK;

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


        private void LoadData()
        {

            var stationInfos = ServiceProvider.Instance().GetStationInfoService().GetStationInfos();
            if (stationInfos == null || !stationInfos.Any())
            {
                MessageBox.Show(@"No Host Server Setting Found! Please add new setting", @"Process Completed",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            _corporateInfos = stationInfos;
            _corporateInfo = _corporateInfos[0];

            if (_corporateInfo == null || _corporateInfo.StationInfoId < 1)
            {
                MessageBox.Show(@"No Host Server Setting Found! Please add new setting", @"Process Completed",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            txtAccessKey.Text = _corporateInfo.APIAccessKey;
            txtHostServer.Text = _corporateInfo.HostServerAddress;
            txtStationKey.Text = _corporateInfo.StationKey;
            txtStationName.Text = _corporateInfo.StationName;
            //txtMobileNumber.Text = _corporateInfo.MobileNumber;
            //txtAddress.Text = _corporateInfo.Address;
            chkStatus.Checked = _corporateInfo.Status;

            chkStatus.Enabled = false;
            txtStationName.Enabled = false;
            txtStationKey.Enabled = false;
        }


        #region Validation

        private bool ValidateSubmission()
        {
            try
            {
                if (txtStationName.Text.Length < 5)
                {
                    MessageBox.Show(@"Station Name is required and must be more than 5 character long", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                if (txtStationKey.Text == "")
                {
                    MessageBox.Show(@"Station is required", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                if (txtStationKey.Text.Length != 15)
                {
                    MessageBox.Show(@"Invalid Station Key", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                if (txtHostServer.Text == "" || txtHostServer.Text.Trim().Length < 5)
                {
                    MessageBox.Show(@"Invalid Host Server Address", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                //if (txtMobileNumber.Text.Length < 7)
                //{
                //    MessageBox.Show(@"Kindly provide your company's Mobile Number", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //    return false;
                //}
                if (string.IsNullOrEmpty(txtAccessKey.Text) || txtAccessKey.Text.Length != 10)
                {
                    MessageBox.Show(@"This Station cannot be updated because it contains invalid / empty Access Key", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                //if (txtAddress.Text.Length >= 5)
                //    return true;
                //MessageBox.Show(@"Invalid / Empty Station Address", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return true;
                
                //return true;
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
