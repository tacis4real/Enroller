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
    public partial class frmChangePassword : Form
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
        private readonly string _username;

        #endregion


        public frmChangePassword(string username)
        {

            //if (string.IsNullOrEmpty(username) || username.Length < 8)
            //{
            //    _username = "";
            //}
            //else
            //{
            //    InitializeComponent();
            //    _username = username;
            //}
            InitializeComponent();
            _username = username;
            EventHooks();
        }
        
        private void EventHooks()
        {
            btnHeader.MouseMove += btnHeader_MouseMove;
            btnCloseWind.Click += btnCloseWind_Click;
            btnUpdate.Click += btnUpdate_Click;
        }

        
        
        
        void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateSubmission())
                return;
            try
            {
                var status = ServiceProvider.Instance().GetUserServices().ChangePassword(_username, txtPassword.Text.Trim(), txtNewPassword.Text.Trim());
                if (!status.IsSuccessful)
                {
                    MessageBox.Show(
                        string.IsNullOrEmpty(status.Message.FriendlyMessage)
                            ? "Unable to complete your request! Please try later"
                            : status.Message.FriendlyMessage, @"Process Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Hand);
                }

                MessageBox.Show(@"Process Completed! Your password was changed successfully", @"Process Completed", MessageBoxButtons.OK,
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


        #region Validation

        private bool ValidateSubmission()
        {
            try
            {
                if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    MessageBox.Show(@"Please supply your current password", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPassword.Focus();
                    return false;
                }
                if (txtPassword.Text.Trim().Length < 8)
                {
                    MessageBox.Show(@"Invalid current password", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPassword.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
                {
                    MessageBox.Show(@"Please supply your new password", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNewPassword.Focus();
                    return false;
                }
                if (txtNewPassword.Text.Trim().Length < 8)
                {
                    MessageBox.Show(@"Invalid new password", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNewPassword.Focus();
                    return false;
                }
                if (string.Equals(txtNewPassword.Text.Trim(), txtPassword.Text.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    MessageBox.Show(@"Your new password must be different from the current password",
                        @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNewPassword.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
                {
                    MessageBox.Show(@"Please confirm your new password", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtConfirmPassword.Focus();
                    return false;
                }
                if (txtConfirmPassword.Text.Trim().Length < 8)
                {
                    MessageBox.Show(@"Invalid confirm password", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtConfirmPassword.Focus();
                    return false;
                }
                if (!string.Equals(txtNewPassword.Text.Trim(), txtConfirmPassword.Text.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    MessageBox.Show(@"New password and confirm password must match", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtConfirmPassword.Focus();
                    return false;
                }
                
                return true;
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
