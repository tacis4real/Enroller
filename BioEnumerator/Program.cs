using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioEnumerator.DataManagement.PopUps;
using BioEnumerator.GSetting;

namespace BioEnumerator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Application.Run(new frmCaptureImage());
            new frmSplash().ShowDialog();

            try
            {
                if (Utils.CurrentUser.UserProfileId < 1)
                {
                    Application.Exit();
                    return;
                }

                Application.Run(new frmHome());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

        }
    }
}
