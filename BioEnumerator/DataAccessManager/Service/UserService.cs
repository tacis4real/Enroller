using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.Repository;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataAccessManager.Service
{
    internal class UserService
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly UserRepository _userRepository;
        public UserService()
        {
            _userRepository = new UserRepository();
        }


        #region CRUD

        public List<User> GetUsers()
        {
            try
            {
                var objList = _userRepository.GetUsers();
                if (objList == null) { return new List<User>(); }
                return objList;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new List<User>();
            }
        }

        public User GetUser(string username)
        {
            try
            {
                var myItem = _userRepository.GetUser(username);
                if (myItem == null || myItem.UserId < 1) { return new User(); }
                return myItem;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return new User();
            }
        }


        #region -> Logins

        public UserInformation LoginUser(string username, string password)
        {
            var userInformation = new UserInformation()
            {
                UserInfo = new User()
            };
            try
            {
                return _userRepository.LoginUser(username, password);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Login Failed! Reason: Invalid Parameters";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                userInformation.Status = _status;
                return userInformation;
            }
        }

        public UserInformation RemoteLoginUser(string username, string password, string hostServer)
        {
            var userInformation = new UserInformation()
            {
                UserInfo = new User()
            };
            try
            {
                return _userRepository.RemoteLoginUser(username, password, hostServer);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Login Failed! Reason: Invalid Parameters";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                userInformation.Status = _status;
                return userInformation;
            }
        }


        public Status ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                return _userRepository.ChangePassword(username, oldPassword, newPassword);
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Unable to change your password at this time. Please try again later";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
        }


        #endregion

        #endregion

    }
}
