using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using BioEnumerator.CommonUtils;
using BioEnumerator.DataAccessManager.Service.Contract;
using BioEnumerator.GSetting;

namespace BioEnumerator.DataManagement
{
    public partial class UclPreview : UserControl
    {
        public UclPreview()
        {
            InitializeComponent();
            EventHooks();
        }



        private void EventHooks()
        {
            btnSubmit.Click += btnSubmit_Click;
            Load += UclPreview_Load;
        }

        void UclPreview_Load(object sender, EventArgs e)
        {
            picImage.Image = new Bitmap(Utils.CaptureImage);
            picImage.SizeMode = PictureBoxSizeMode.StretchImage;

            lblSurname.Text = Utils.PreviewBeneficiaryRegObj.Surname;
            lblFirstName.Text = Utils.PreviewBeneficiaryRegObj.FirstName;
            lblOtherName.Text = Utils.PreviewBeneficiaryRegObj.Othernames;
            lblGender.Text = Utils.PreviewBeneficiaryRegObj.Sex;
            lblDateOfBirth.Text = Utils.PreviewBeneficiaryRegObj.DateOfBirth;
            lblMaritalStatus.Text = Utils.PreviewBeneficiaryRegObj.MaritalStatus;
            lblHomeAddress.Text = Utils.PreviewBeneficiaryRegObj.ResidentialAddress;
            lblOfficeAddress.Text = Utils.PreviewBeneficiaryRegObj.OfficeAddress;
            lblState.Text = Utils.PreviewBeneficiaryRegObj.State;
            lblLga.Text = Utils.PreviewBeneficiaryRegObj.LocalArea;
            lblMobileNo.Text = Utils.PreviewBeneficiaryRegObj.MobileNumber;
            lblOccupation.Text = Utils.PreviewBeneficiaryRegObj.Occupation;
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateSubmission()) { return; }


            var retVal = ServiceProvider.Instance().GetBeneficiaryService().AddBeneciary(Utils.BeneficiaryRegObj);
            if (!retVal.IsSuccessful)
            {
                MessageBox.Show(@"Error: " + (string.IsNullOrEmpty(retVal.Message.FriendlyMessage) ? " Process Failed! Please try again later" : retVal.Message.FriendlyMessage), @"Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation);
                return;
            }

            MessageBox.Show(@"Process Completed! Data was saved successfully", @"Process Completed", MessageBoxButtons.OK,
                  MessageBoxIcon.Information);

            new frmHome().ShowDialog();

        }



        public void SetPanel()
        {

            var mX = (pnlBody.Width - pnlItemBody.Width) / 2;
            pnlItemBody.Location = new Point(mX, pnlItemBody.Location.Y);

            var mX1 = (pnlItemBody.Width - pnlFrame.Width) / 2;
            pnlFrame.Location = new Point(mX1, pnlFrame.Location.Y);

        }


        private bool ValidateSubmission()
        {
            try
            {
                if (Utils.BeneficiaryRegObj.Image == null)
                {
                    MessageBox.Show(@"No picture capture for the registrant!",
                            @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false; ;
                }

                if (CustomHelper.ValidateArray(Utils.BeneficiaryRegObj.CapturedFingerPrintTemplate))
                {
                    MessageBox.Show(@"No fingerprint capture for the registrant!",
                            @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false; ;
                }

                return true;

                // Utils.BeneficiaryRegObj.CapturedFingerPrintTemplate
            }
            catch (Exception)
            {
                MessageBox.Show(@"Error Occurred! Please try again later", @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                return false;
            }
        }

       
        

    }
}
