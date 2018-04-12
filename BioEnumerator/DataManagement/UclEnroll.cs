using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Data;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using BioEnumerator.CommonUtils;
using BioEnumerator.DataAccessManager.CommonUtils;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;
using BioEnumerator.DataAccessManager.Repository.Helpers;
using BioEnumerator.DataAccessManager.Service.Contract;
using BioEnumerator.DataManagement.PopUps;
using BioEnumerator.GSetting;
using DPFP;
using Transitions;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataManagement
{

    delegate void Function();	// a simple delegate for marshalling calls from event handlers to the GUI thread


    public partial class UclEnroll : UserControl
    {


        #region Vars

        //private DPFP.Template _template;

        // Rect Selection with mouse
        //private NormalizedRect _mouseSelectionRect = new NormalizedRect(0, 0, 0, 0);
        private bool _bDrawMouseSelection = false;

        private BackgroundWorker _bgWorker;
        private List<State> _states;
        private List<LocalArea> _localAreas;
        private List<MaritalStatus> _maritalStatus;
        private List<NameAndValueObject> _marital;
        private List<Occupation> _occupations;

        // Zooming
        private bool _bZoomed = false;
        private double _fZoomValue = 1.0;

        // Camera choice
        //private CameraChoice _cameraChoice = new CameraChoice();

        #endregion


        public UclEnroll()
        {
            InitializeComponent();
            EventHooks();
            InitDropDowns();
        }


        private void EventHooks()
        {
            btnCaptureInfo.Click += btnCaptureInfo_Click;
            cmbState.SelectedIndexChanged += cmbState_SelectedIndexChanged;
            cmbLocalArea.Enabled = false;
            //btnCaptureImage.Click += btnCaptureImage_Click;
            picCapture.Click += picCapture_Click;
            btnSaveImage.Click += btnSaveImage_Click;
            btnSaveImage.Visible = false;
            dtpDateOfBirth.Format = DateTimePickerFormat.Custom;
            dtpDateOfBirth.CustomFormat = @"yyyy/MM/dd";
            //cmbLocationType.SelectedIndexChanged += cmbLocationType_SelectedIndexChanged;
            //btnSave.Click += btnSave_Click;
            //btnReset.Click += btnReset_Click;
        }

        
        void btnSaveImage_Click(object sender, EventArgs e)
        {
            #region Save Image

            //var bioStoreDir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BiometricResource"];
            var bioStoreDir = InternetCon.GetBasePath();
            var mainFolder = ConfigurationManager.AppSettings["BiometricResource"];
            bioStoreDir = bioStoreDir + mainFolder;
            //var imgImage = new Bitmap(picImage.Image);    //Create an object of Bitmap class/
            var imgName = DateTime.Now.Ticks;
            //picImage.Image.Save(bioStoreDir + "\\" + imgName + "\\" + 1 + ".jpg");

           
            //using (var fs = picImage.Image)
            //{
            //    byte[] bytes = Convert.FromBase64String(fs);
            //    File.WriteAllBytes(@imagePath, bytes);
            //}

            if (picImage.Image != null)
            {
                SaveImage(picImage.Image);
            }
            
            
            //using (var mStream = new MemoryStream())
            //{
            //    if (picImage.Image != null)
            //    { 
            //        //using MemoryStream:
            //        picImage.Image.Save(mStream, ImageFormat.Jpeg);
            //        //var photoAray = new byte[mStream.Length];
            //        var photoAray = mStream.ToArray();
            //        SaveImage(photoAray);
            //        //File.WriteAllBytes(bioStoreDir + "\\" + imgName, photoAray);
            //    }
            //}

            #endregion
        }


        private void SaveImage(byte[] imageByteArray)
        {

            var imgName = DateTime.Now.Ticks;
            var fileName = imgName + ".jpeg";
            //TODO:Resourse path
            const string folderPath = "/Image/";
            var imageResPath = folderPath + fileName;
            var dir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BiometricResource"];
            var imagePath = Path.GetFullPath(dir + imageResPath);
            if (!Directory.Exists(@dir + @folderPath))
            {
                try
                {
                    Directory.CreateDirectory(@dir + @folderPath);
                }
                catch (Exception ex)
                {
                    
                }
            }
            while (File.Exists(imagePath))
            {
                fileName = DateTime.Now.Ticks + ".jpeg";
                imageResPath = folderPath + fileName;
                imagePath = Path.GetFullPath(dir + imageResPath);
            }

            File.WriteAllBytes(@imagePath, imageByteArray);

        }

        private void SaveImage(Image image)
        {


            #region Folder Structure For Storage
            
            var stationKey = (ServiceProvider.Instance().GetStationInfoService().GetStationInfos() ?? new List<StationInfo>())[0].StationKey;
            var username = Utils.CurrentUser.UserName;
            var year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);

            #endregion

            var imgName = DateTime.Now.Ticks;
            var fileName = imgName + ".jpeg";
            //var fileName = imgName;
            //TODO:Resourse path
            string folderPath = "/Station-"+ stationKey +"/"+ username +"/"+ year +"/"+ "/Image/";
            var imageResPath = folderPath + fileName;
            var dir = InternetCon.GetBasePath() + ConfigurationManager.AppSettings["BioResource"];
            var imagePath = Path.GetFullPath(dir + imageResPath);
            if (!Directory.Exists(@dir + @folderPath))
            {
                try
                {
                    Directory.CreateDirectory(@dir + @folderPath);
                }
                catch (Exception ex)
                {

                }
            }
            while (File.Exists(imagePath))
            {
                fileName = DateTime.Now.Ticks + ".jpeg";
                imageResPath = folderPath + fileName;
                imagePath = Path.GetFullPath(dir + imageResPath);
            }

            image.Save(imagePath, ImageFormat.Jpeg);
            //File.WriteAllBytes(@imagePath, imageByteArray);

        }
        void picCapture_Click(object sender, EventArgs e)
        {
            var frmCap = new frmCaptureImage();
            frmCap.ShowDialog();
            //if (frmCap.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}




            // Render the Image on Picture Box
            //var bmp = new Bitmap(pictureBox1.Image);
            //Graphics gr = Graphics.FromImage(bmp);
            //Pen p = new Pen(Color.Red);
            //p.Width = 5.0f;
            //gr.DrawRectangle(p, 1, 2, 30, 40);
            //pictureBox1.Image = bmp;

            picImage.Image = new Bitmap(Utils.CaptureImage);
            picImage.SizeMode = PictureBoxSizeMode.StretchImage;
            //picImage.Update();
        }

        void btnCaptureImage_Click(object sender, EventArgs e)
        {
            var frmCap = new frmCaptureImage();
            frmCap.ShowDialog();
            //if (frmCap.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}

            


            // Render the Image on Picture Box
            //var bmp = new Bitmap(pictureBox1.Image);
            //Graphics gr = Graphics.FromImage(bmp);
            //Pen p = new Pen(Color.Red);
            //p.Width = 5.0f;
            //gr.DrawRectangle(p, 1, 2, 30, 40);
            //pictureBox1.Image = bmp;

            picImage.Image = new Bitmap(Utils.CaptureImage);
            picImage.Update();

            //frmCap.RenderImageAction += RenderImageAction;
            //frmCap.Close();

        }

        private void RenderImageAction(Bitmap capturedImage)
        {
            if (capturedImage == null)
                return;
            picImage.Image = capturedImage;
            picImage.Update();
            //VisualStyleElement.ToolTip.Close
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


        
        //void cmbLocationType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (_firstLoad) { return; }
        //    if (cmbLocationType.SelectedValue == null) { return; }
        //    if (int.Parse(cmbLocationType.SelectedValue.ToString()) < 1) ;
        //    var filteredLocation =
        //        _locations.FindAll(m => m.BusinessLocationTypeId == int.Parse(cmbLocationType.SelectedValue.ToString()));

        //    // filteredLocation.Insert(0, new BusinessLocation { BusinessLocationId = 0, Name = @"-- Please Select --" });
        //    bdsBusinessLocation.DataSource = new List<BusinessLocation>();
        //    bdsBusinessLocation.DataSource = filteredLocation;
        //    cmbBusinessLocation.SelectedValue = Utils.StationInfo.BusinessLocationId;
        //}



        #region Sub Modules

        void btnCaptureInfo_Click(object sender, EventArgs e)
        {
            var fingers = Utils.CapturedTemplates;

            if (!ValidateSubmission())
                return;

            CacheInfo();
            //var frmCap = new frmCaptureImage();
            //if (frmCap.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}


            bool flag;
            var frm = new frmModuleDisplay(2, out flag);
            if (!flag) { return; }

           
            //var ck = Utils.BeneficiaryRegObj;

            frm.Show();
            frm.Location = new Point(Width, 0);
            var t = new Transition(new TransitionType_EaseInEaseOut(150));
            t.add(frm, "Left", 0);
            t.run();
        }


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
                if(picImage.Image != null)
                    return true;
                    MessageBox.Show(@"Please take the image of the person", @"Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                MessageBox.Show(@"Unable to validate your inputs. Please check all inputs and try again later", @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        private void CacheInfo()
        {

            Utils.BeneficiaryRegObj.Surname = txtSurname.Text.Trim();
            Utils.BeneficiaryRegObj.FirstName = txtFirstName.Text.Trim();
            Utils.BeneficiaryRegObj.Othernames = txtOtherName.Text.Trim();
            Utils.BeneficiaryRegObj.DateOfBirth = dtpDateOfBirth.Value.ToString("yyyy/MM/dd");
            Utils.BeneficiaryRegObj.Sex = rbbMale.Checked ? 1 : 2;
            Utils.BeneficiaryRegObj.OccupationId = int.Parse(cmbOccupation.SelectedValue.ToString());
            Utils.BeneficiaryRegObj.MaritalStatus = int.Parse(cmbOccupation.SelectedValue.ToString());
            Utils.BeneficiaryRegObj.ResidentialAddress = txtHomeAddress.Text.Trim();
            Utils.BeneficiaryRegObj.OfficeAddress = txtOfficeAddress.Text.Trim();
            Utils.BeneficiaryRegObj.StateId = int.Parse(cmbState.SelectedValue.ToString());
            Utils.BeneficiaryRegObj.LocalAreaId = int.Parse(cmbLocalArea.SelectedValue.ToString());
            Utils.BeneficiaryRegObj.MobileNumber = txtMobileNo.Text.Trim();
            Utils.BeneficiaryRegObj.Image = new Bitmap(Utils.CaptureImage);

            #region Preview

            Utils.PreviewBeneficiaryRegObj.Surname = txtSurname.Text.Trim();
            Utils.PreviewBeneficiaryRegObj.FirstName = txtFirstName.Text.Trim();
            Utils.PreviewBeneficiaryRegObj.Othernames = txtOtherName.Text.Trim();
            Utils.PreviewBeneficiaryRegObj.DateOfBirth = dtpDateOfBirth.Value.ToString("yyyy/MM/dd");
            Utils.PreviewBeneficiaryRegObj.Sex = rbbMale.Checked ? rbbMale.Text : rbbFemale.Text;
            Utils.PreviewBeneficiaryRegObj.Occupation = cmbOccupation.Text;
            Utils.PreviewBeneficiaryRegObj.MaritalStatus = cmbMaritalStatus.Text;
            Utils.PreviewBeneficiaryRegObj.ResidentialAddress = txtHomeAddress.Text.Trim();
            Utils.PreviewBeneficiaryRegObj.OfficeAddress = txtOfficeAddress.Text.Trim();
            Utils.PreviewBeneficiaryRegObj.State = cmbState.Text;
            Utils.PreviewBeneficiaryRegObj.LocalArea = cmbLocalArea.Text;
            Utils.PreviewBeneficiaryRegObj.MobileNumber = txtMobileNo.Text.Trim();

            #endregion


        }

        private void RetrieveCacheInfo()
        {
            txtSurname.Text = Utils.BeneficiaryRegObj.Surname;
            txtFirstName.Text = Utils.BeneficiaryRegObj.FirstName;
            txtOtherName.Text = Utils.BeneficiaryRegObj.Othernames;
            //if (Utils.BeneficiaryRegObj.DateOfBirth >= dtpDateOfBirth.MinDate &&
            //    Utils.BeneficiaryRegObj.DateOfBirth <= dtpDateOfBirth.MaxDate)
            //{
            //    dtpDateOfBirth.Value = Utils.BeneficiaryRegObj.DateOfBirth;
            //}

            if (!string.IsNullOrEmpty(Utils.BeneficiaryRegObj.DateOfBirth))
            {
                dtpDateOfBirth.Value = DateTime.Parse(Utils.BeneficiaryRegObj.DateOfBirth);
            }
            
            rbbMale.Checked = (Utils.BeneficiaryRegObj.Sex == 1);
            rbbFemale.Checked = (Utils.BeneficiaryRegObj.Sex == 2);

            if (Utils.BeneficiaryRegObj.StateId > 0)
            {
                cmbState.SelectedValue = Utils.BeneficiaryRegObj.StateId;
            }
            if (Utils.BeneficiaryRegObj.LocalAreaId > 0)
            {
                cmbLocalArea.SelectedValue = Utils.BeneficiaryRegObj.LocalAreaId;
            }
            if (Utils.BeneficiaryRegObj.OccupationId > 0)
            {
                cmbOccupation.SelectedValue = (Utils.BeneficiaryRegObj.OccupationId);
            }
            if (Utils.BeneficiaryRegObj.MaritalStatus > 0)
            {
                cmbMaritalStatus.SelectedValue = Utils.BeneficiaryRegObj.MaritalStatus;
            }
            
            txtHomeAddress.Text = Utils.BeneficiaryRegObj.ResidentialAddress;
            txtOfficeAddress.Text = Utils.BeneficiaryRegObj.OfficeAddress;
            txtMobileNo.Text = Utils.BeneficiaryRegObj.MobileNumber;
        }


        private void BindSelectInfo()
        {
            cmbState.SelectedIndex = Utils.BeneficiaryRegObj.StateId;
            cmbLocalArea.SelectedIndex = Utils.BeneficiaryRegObj.LocalAreaId;
            cmbOccupation.SelectedIndex = (Utils.BeneficiaryRegObj.OccupationId);
            cmbMaritalStatus.SelectedValue = Utils.BeneficiaryRegObj.MaritalStatus;
        }

        #endregion


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

                //_maritalStatus.Insert(0, new MaritalStatus { MaritalStatusId = 0, Name = @"-- Please Select --" });
                //bdsMaritalStatus.DataSource = new List<MaritalStatus>();
                //bdsMaritalStatus.DataSource = _maritalStatus;
                //cmbMaritalStatus.SelectedIndex = 0;

                RetrieveCacheInfo();

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



    }
}
