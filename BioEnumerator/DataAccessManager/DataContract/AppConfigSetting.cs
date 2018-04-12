using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioEnumerator.DataAccessManager.DataContract
{
    public class AppConfigSetting
    {

        public int AppConfigSettingId { get; set; }
        public bool FirstTimeLaunch { get; set; }
        public DateTime TimeStampConfigured { get; set; }

    }
}
