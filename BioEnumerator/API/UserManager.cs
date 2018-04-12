using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.Service.Contract;

namespace BioEnumerator.API
{
    public class UserManager
    {


        public static List<UserProfile> GetUserProfiles()
        {
            return ServiceProvider.Instance().GetUserProfileService().GetUserProfiles();
        }

        public static UserInformation LoginUser(string username, string password)
        {
            return ServiceProvider.Instance().GetUserServices().LoginUser(username, password);
        }

        public static UserInformation RemoteLoginUser(string username, string password, string hostServer)
        {
            return ServiceProvider.Instance().GetUserServices().RemoteLoginUser(username, password, hostServer);
        }

        public static User GetUser(string username)
        {
            return ServiceProvider.Instance().GetUserServices().GetUser(username);
        }

        public static List<CompanyInfo> GetCompanyInfos()
        {
            return ServiceProvider.Instance().GetCorporateInfoService().GetCompanyInfos();
        }

        public static Role GetRole(string roleName)
        {
            return ServiceProvider.Instance().GetRoleServices().GetRole(roleName);
        }



    }
}
