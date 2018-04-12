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
using BioEnumerator.DataAccessManager.CommonUtils;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.GSetting;
using Camera_NET;

namespace BioEnumerator.DataManagement.PopUps
{
    public partial class frmCaptureImage : Form
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
        #endregion

        #region Vars

        private CameraChoice _cameraChoice = new CameraChoice();
        public Action<Bitmap> RenderImageAction;

        #endregion


        public frmCaptureImage()
        {
            InitializeComponent();
            EventHooks();
            //btnHeader.MouseDown += btnHeader_MouseDown;

        }



        private void EventHooks()
        {
            Load += frmCaptureImage_Load;
            Closed += frmCaptureImage_Closed;
            btnSnapShot.Click += btnSnapShot_Click;
            btnOk.Click += btnOk_Click;
            btnOk.Enabled = false;
            //btnCaptureImage.Click += btnCaptureImage_Click;
            lblHeader.MouseMove += lblHeader_MouseMove;
            btnCloseWind.Click += btnCloseWind_Click;
            cmbCameraList.SelectedIndexChanged += cmbCameraList_SelectedIndexChanged;
            cmbResolutionList.SelectedIndexChanged += cmbResolutionList_SelectedIndexChanged;
        }

        

        void btnSnapShot_Click(object sender, EventArgs e)
        {
            if (!cameraControl.CameraCreated)
                return;

            Bitmap bitmap = cameraControl.SnapshotOutputImage();

            if (bitmap == null)
                return;

            picSnapShot.Image = bitmap;
            //picSnapShot.Update();
            picSnapShot.SizeMode = PictureBoxSizeMode.StretchImage;
            btnOk.Enabled = true;
            //cameraControl.CloseCamera();
            //RenderImageAction(bitmap);
            //cameraControl.CloseCamera();
            //frmCaptureImage_Closed(null, null);
            //Close();

        }

        void btnOk_Click(object sender, EventArgs e)
        {

            if(picSnapShot.Image == null) return;
            cameraControl.CloseCamera();
            var imageBitmap = (Bitmap)picSnapShot.Image;
            Utils.CaptureImage = imageBitmap;

            //RenderImageAction(imageBitmap);
            Close();
        }


        void frmCaptureImage_Closed(object sender, EventArgs e)
        {
            // Close camera
            cameraControl.CloseCamera();
        }

        void frmCaptureImage_Load(object sender, EventArgs e)
        {
            // Fill camera list combobox with available cameras
            FillCameraList();

            // Select the first one
            if (cmbCameraList.Items.Count > 0)
            {
                cmbCameraList.SelectedIndex = 0;
            }

            // Fill camera list combobox with available resolutions
            FillResolutionList();
        }




        void btnCloseWind_Click(object sender, EventArgs e)
        {
            //DialogResult = DialogResult.Cancel;
            //Utils.CaptureImage = null;
            cameraControl.CloseCamera();
            //cameraControl.Dispose();
            Close();
        }



        #region Camera and resolution selection

       
        private void FillCameraList()
        {
            cmbCameraList.Items.Clear();

            _cameraChoice.UpdateDeviceList();

            foreach (var cameraDevice in _cameraChoice.Devices)
            {
                cmbCameraList.Items.Add(cameraDevice.Name);
            }
        }
        private void FillResolutionList()
        {
            cmbResolutionList.Items.Clear();

            if (!cameraControl.CameraCreated)
                return;

            ResolutionList resolutions = Camera.GetResolutionList(cameraControl.Moniker);

            if (resolutions == null)
                return;

            int indexToSelect = -1;

            for (int index = 0; index < resolutions.Count; index++)
            {
                cmbResolutionList.Items.Add(resolutions[index].ToString());

                if (resolutions[index].CompareTo(cameraControl.Resolution) == 0)
                {
                    indexToSelect = index;
                }
            }

            // select current resolution
            if (indexToSelect >= 0)
            {
                cmbResolutionList.SelectedIndex = indexToSelect;
            }

            cmbResolutionList.Enabled = false;
        }
        void cmbResolutionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cameraControl.CameraCreated)
                return;

            int comboBoxResolutionIndex = cmbResolutionList.SelectedIndex;
            if (comboBoxResolutionIndex < 0)
            {
                return;
            }
            var resolutions = Camera.GetResolutionList(cameraControl.Moniker);

            if (resolutions == null)
                return;

            if (comboBoxResolutionIndex >= resolutions.Count)
                return; // throw

            if (0 == resolutions[comboBoxResolutionIndex].CompareTo(cameraControl.Resolution))
            {
                // this resolution is already selected
                return;
            }

            // Recreate camera
            //SetCamera(_Camera.Moniker, resolutions[comboBoxResolutionIndex]);
            cameraControl.SetCamera(cameraControl.Moniker, resolutions[comboBoxResolutionIndex]);
        }
        void cmbCameraList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCameraList.SelectedIndex < 0)
            {
                cameraControl.CloseCamera();
            }
            else
            {
                // Set camera
                cameraControl.SetCamera(_cameraChoice.Devices[cmbCameraList.SelectedIndex].Mon, null);
                //SetCamera(_CameraChoice.Devices[ comboBoxCameraList.SelectedIndex ].Mon, null);
            }

            FillResolutionList();
        }

        #endregion


        
    }
}
