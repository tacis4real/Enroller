using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioEnumerator.DataManagement
{
    public partial class frmChooseScanner : Form
    {

        #region Vars

        private string _allModuleString = string.Empty;
        private string _scannerString = string.Empty;

        #endregion


        public frmChooseScanner()
        {
            InitializeComponent();


            //_allModuleString = Nffv.GetAvailableScannerModules();
            //if (_allModuleString.Length > 0)
            //{
            //    string[] allModuleList = _allModuleString.Split(new char[] { ';' });
            //    clbScannerList.Items.Clear();
            //    clbScannerList.Items.AddRange(allModuleList);
                
            //}
            //else
            //{
            //    clbScannerList.Items.Add("No scanner modules found.");
            //    clbScannerList.Enabled = false;
            //}
        }
    }
}
