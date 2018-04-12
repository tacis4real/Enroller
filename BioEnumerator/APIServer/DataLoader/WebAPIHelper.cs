using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.APIServer.APIObjs;
using Newtonsoft.Json;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.APIServer.DataLoader
{
    public class WebAPIHelper
    {

        readonly HttpClient _client = new HttpClient();
        public WebAPIHelper(string url, string key = "")
        {
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public bool AddStationUser(UserRegistrationObj userRegistrationObj, ref string reply, ref UserRegResponse userResp)
        {

            if (userRegistrationObj == null)
            {
                reply = "Invalid User Information";
                return false;
            }

            try
            {
                var response = _client.PostAsJsonAsync("api/StationUser/AddUserDetail", userRegistrationObj).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    var questionResponse = JsonConvert.DeserializeObject<UserRegResponse>(data);
                    if (questionResponse.Status.IsSuccessful == false)
                    {
                        reply = questionResponse.Status.Message.FriendlyMessage;
                        return false;
                    }
                    reply = null;
                    userResp = questionResponse;
                    return true;
                }
                return false;
                
            }
            catch (Exception ex)
            {
                reply = ex.Message;
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return false;
            }

        }
        
        public bool GetStationUsers(ref string reply, ref List<RegisteredUserReportObj> userList)
        {

            try
            {

                var response = _client.GetAsync("api/StationUser/UserDetail").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    var stationUserResponse = JsonConvert.DeserializeObject<List<RegisteredUserReportObj>>(data);
                    if (stationUserResponse == null)
                    {
                        reply = "";
                        return false;
                    }
                    reply = null;
                    userList = stationUserResponse.ToList();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                reply = ex.Message;
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return false;
            }
        }

        public bool GetRemoteUsers(ref string reply, ref List<UserDetailObj> userList)
        {
            try
            {

                var response = _client.GetAsync("api/StationUser/Users").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    var stationUserResponse = JsonConvert.DeserializeObject<List<UserDetailObj>>(data);
                    if (stationUserResponse == null)
                    {
                        reply = "";
                        return false;
                    }
                    reply = null;
                    userList = stationUserResponse.ToList();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                reply = ex.Message;
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return false;
            }
        }



        #region Used Ones
        public bool GetRemoteUserInfos(ref string reply, ref List<RemoteUserInfo> userInfos)
        {
            try
            {

                var response = _client.GetAsync("api/StationUser/GetRemoteUserInfos").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    var stationUserResponse = JsonConvert.DeserializeObject<List<RemoteUserInfo>>(data);
                    if (stationUserResponse == null)
                    {
                        reply = "";
                        return false;
                    }
                    reply = null;
                    userInfos = stationUserResponse;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                reply = ex.Message;
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return false;
            }
        }

        #endregion


    }
}
