

using System.Collections.Generic;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;

namespace BioEnumerator.DataAccessManager.DataContract
{
    public class UserProfileInformation
    {
        public List<StaffUser> UserInfos;
        public List<UserProfile> UserProfileInfos;
        public List<ClientStation> ClientStationInfos;
    }
}
