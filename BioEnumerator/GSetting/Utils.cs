using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.APIServer.APIObjs;
using BioEnumerator.DataAccessManager.CommonUtils;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.RemoteHelper.ExternalTool;
using DPFP;

namespace BioEnumerator.GSetting
{
    public class Utils
    {

        internal static BeneficiaryRegObj BeneficiaryRegObj { get; set; }
        internal static PreviewBeneficiaryRegObj PreviewBeneficiaryRegObj { get; set; }

        internal static BiometricInfo BiometricInfo { get; set; }
        internal static Template[] VerifyTemplates = new Template[0];
        internal static Bitmap CaptureImage;
        internal static Template[] CapturedTemplates = new Template[10];
        internal static Template RightThumbPrintTemplateFile;
        internal static Template RightIndexPrintTemplateFile;
        internal static User CurrentUser { get; set; }
        internal static string CurrentUserFullName
        {
            get
            {
                var staffInfo = CurrentUser.UserProfile;
                if (staffInfo == null)
                {
                    return "";
                }
                if (staffInfo.UserProfileId < 1)
                {
                    return "";
                }
                return staffInfo.Surname + " " + staffInfo.FirstName + " " + staffInfo.OtherNames;
            }
        }
        internal static StationInfo StationInfo { get; set; }
        internal static UploadStationInfo UploadStationInfo { get; set; }
        internal static CompanyInfo CorporateInfo { get; set; }
        internal static AppConfigSetting AppConfigSettingInfo { get; set; }

        internal static string BeneficiaryStore
        {
            get
            {
                return "Store";
            }
        }
        internal static bool IsConnectedToInternet { get { return InternetHelp.Check(); } }

    }
}
