using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.GSetting;
using Camera_NET;
using Transitions;

namespace BioEnumerator.DataManagement
{
    public partial class UclCaptureImage : UserControl
    {

        #region Vars

        private CameraChoice _cameraChoice = new CameraChoice();
        //public Action<CameraControl> CloseCamera;
        #endregion

        public UclCaptureImage()
        {
            InitializeComponent();
            EventHooks();
        }


        private void EventHooks()
        {

            Load += UclCaptureImage_Load;
            btnCaptureImage.Click += btnCaptureImage_Click;
            cmbCameraList.SelectedIndexChanged += cmbCameraList_SelectedIndexChanged;
            cmbResolutionList.SelectedIndexChanged += cmbResolutionList_SelectedIndexChanged;
            btnStartCamera.Click += btnStartCamera_Click;
        }

        public void btnStartCamera_Click(object sender, EventArgs e)
        {
            cameraControl.CloseCamera();
        }

        

        

       


        #region Form Event Handlers:

        void UclCaptureImage_Load(object sender, EventArgs e)
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

        #endregion



        public void SetPanel()
        {

            var mX = (pnlBody.Width - pnlItemBody.Width) / 2;
            pnlItemBody.Location = new Point(mX, pnlItemBody.Location.Y);

            var mX1 = (pnlItemBody.Width - pnlFrame.Width) / 2;
            pnlFrame.Location = new Point(mX1, pnlFrame.Location.Y);

        }



        #region Sub Modules


        void btnCaptureImage_Click(object sender, EventArgs e)
        {

            var bioInfo = Utils.BeneficiaryRegObj;
            if (bioInfo.FirstName.Length > 0)
            {
                
            }
            //bool flag;
            //var frm = new frmModuleDisplay(3, out flag);
            //if (!flag) { return; }
            //frm.Show();
            //frm.Location = new Point(Width, 0);
            //var t = new Transition(new TransitionType_EaseInEaseOut(150));
            //t.add(frm, "Left", 0);
            //t.run();
        }


        #endregion


        #region Camera and resolution selection

        public void CloseCamera()
        {
            cameraControl.CloseCamera();
        }

       
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
