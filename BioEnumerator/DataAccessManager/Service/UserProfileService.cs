using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.APIServer.APIObjs;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.Repository;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataAccessManager.Service
{
    internal class UserProfileService
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private SyncUserStatus _respStatus = new SyncUserStatus
        {
            IsSuccessful = false,
            Message = new Message(),
            UserInfos = new List<User>(),
            UserProfileInfos = new List<UserProfile>()
        };



        private readonly UserProfileRepository _userProfileRepository;
        public UserProfileService()
        {
            _userProfileRepository = new UserProfileRepository();
        }

        

        #region CRUD

        public Status CompanyAndUserProfile(CompanyInfo companyInfo, UserProfile staffRegistration, User user)
        {
            try
            {
                return _userProfileRepository.AddCompanyAndUserProfile(companyInfo, staffRegistration, user);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Process Failed! Unable to add new user record";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
        }
        
        public List<UserProfile> GetUserProfiles()
        {
            try
            {
                var objList = _userProfileRepository.GetUserProfiles();
                if (objList == null) { return new List<UserProfile>(); }
                return objList;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<UserProfile>();
            }
        }



        public Status AddRemoteStationUser(UserDetailObj userDetailObj)
        {
            try
            {
                return _userProfileRepository.AddRemoteStationUser(userDetailObj);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Process Failed! Unable to add new role record";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
        }

        public SyncUserStatus SyncRemoteUsers(List<RemoteUserInfo> remoteUserInfos, ref long totalSync, out string msg)
        {
            try
            {
                return _userProfileRepository.SyncRemoteUsers(remoteUserInfos, ref totalSync, out msg);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _respStatus.Message.FriendlyMessage = "Process Failed! Unable to add new role record";
                _respStatus.Message.TechnicalMessage = "Error: " + ex.Message;
                _respStatus.Message.MessageCode = "101";
                _respStatus.Message.MessageId = 1;
                msg = _respStatus.Message.FriendlyMessage;
                return _respStatus;
            }
        }
        
        #endregion

    }
}
