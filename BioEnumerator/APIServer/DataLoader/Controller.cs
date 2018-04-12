using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.APIServer.APIObjs;
using BioEnumerator.DataAccessManager.Service.Contract;
using BioEnumerator.RemoteHelper.MessengerService;

namespace BioEnumerator.APIServer.DataLoader
{
    public class Controller
    {

        static WebAPIHelper _apiHelper;
        static string _api = ConfigurationManager.AppSettings["localAddr"];



        #region -> Actions

        
        public static ProcessingStatus DownLoadStationUsers()
        {
            try
            {
                _apiHelper = new WebAPIHelper(_api);
                List<UserDetailObj> remoteStationUsers = null;
                string reply = null;

                if (_apiHelper.GetRemoteUsers(ref reply, ref remoteStationUsers))
                {
                    if (remoteStationUsers == null || !remoteStationUsers.Any()) return ProcessingStatus.OperationTerminated;
                    var total = remoteStationUsers.Count();

                    var x = 0;
                    foreach (var stationUser in remoteStationUsers)
                    {
                        var userId = ServiceProvider.Instance().GetUserProfileService().AddRemoteStationUser(stationUser);
                        if (userId.IsSuccessful && userId.UserId > 0)
                        {
                            x++;
                        }
                    }

                    if (total == x)
                    {
                        Console.WriteLine("Our database was updated successfully");
                    }
                    else
                    {
                        Console.WriteLine("Only {0} questions out of {1} questions was updated successfully", x, total);
                    }

                }
                return ProcessingStatus.Completed;

            }
            catch (Exception)
            {
                return ProcessingStatus.OperationTerminated;
            }
        }

        public static ProcessingStatus DownLoadRemoteUserInfos(out string msg)
        {
            try
            {
                _apiHelper = new WebAPIHelper(_api);
                List<RemoteUserInfo> remoteUserInfos = null;
                string reply = null;
                long totalSync = 0;

                if (_apiHelper.GetRemoteUserInfos(ref reply, ref remoteUserInfos))
                {
                    if (remoteUserInfos == null || !remoteUserInfos.Any())
                    {
                        msg = "Empty Remote Users downloaded from the Remote Server.";
                        return ProcessingStatus.OperationTerminated;
                    }
                      

                    var totalRemoteUserDownloaded = remoteUserInfos.Count();
                    var respStatus = ServiceProvider.Instance().GetUserProfileService().SyncRemoteUsers(remoteUserInfos, ref totalSync, out msg);
                    if (!respStatus.IsSuccessful)
                    {
                        return ProcessingStatus.OperationTerminated;
                    }

                    if (!respStatus.UserInfos.Any() || !respStatus.UserProfileInfos.Any())
                    {
                        return ProcessingStatus.OperationTerminated;
                    }
                    
                    if (totalRemoteUserDownloaded != totalSync)
                    {
                        Console.WriteLine(@"Only {0} questions out of {1} questions was updated successfully", totalRemoteUserDownloaded, totalSync);
                        return ProcessingStatus.PartialCompletion;
                    }
                }

                Console.WriteLine(@"Our database was updated successfully");
                msg = "";
                return ProcessingStatus.Completed;

            }
            catch (Exception)
            {
                msg = "";
                return ProcessingStatus.OperationTerminated;
            }
        }

        #endregion


    }
}
