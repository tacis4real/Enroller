using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.Infrastructure;
using BioEnumerator.DataAccessManager.Infrastructure.Contract;
using BioEnumerator.DataAccessManager.Repository.Helpers;
using BioEnumerator.RemoteHelper.ExternalTool;
using BioEnumerator.RemoteHelper.MessengerService;
using XPLUG.WINDOWTOOLS;
using XPLUG.WINDOWTOOLS.Date;
using XPLUG.WINDOWTOOLS.Extensions;

namespace BioEnumerator.DataAccessManager.Repository
{
    internal class UserRepository
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly IBioEnumeratorRepository<User> _repository;
        private readonly IBioEnumeratorRepository<CompanyInfo> _companyRepository;
        private readonly IBioEnumeratorRepository<StationInfo> _stationInfoRepository;
        private readonly IBioEnumeratorRepository<UserProfile> _userProfileRepository; 
        private readonly BioEnumeratorUoWork _uoWork;

        public UserRepository()
        {
            _uoWork = new BioEnumeratorUoWork();
            _repository = new BioEnumeratorRepository<User>(_uoWork);
            _companyRepository = new BioEnumeratorRepository<CompanyInfo>(_uoWork);
            _stationInfoRepository = new BioEnumeratorRepository<StationInfo>(_uoWork);
            _userProfileRepository = new BioEnumeratorRepository<UserProfile>(_uoWork);
        }


        public List<User> GetUsers()
        {
            try
            {
                var all = _repository.GetAll().ToList();
                return !all.Any() ? new List<User>() : all.ToList();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        } 
        
        public Status AddUser(User user)
        {
            try
            {
                var retVal = _repository.Add(user);
                _uoWork.SaveChanges();
                _status.ReturnedId = retVal.UserId;
                _status.IsSuccessful = retVal.UserId > 0;
                return _status;

            }
            catch (DbEntityValidationException ex)
            {
                ErrorManager.LogApplicationError((ex).StackTrace, (ex).Source, (ex).Message);
                _status.Message.FriendlyMessage = "Unable to add user information";
                _status.Message.TechnicalMessage = "Error: " + (ex).Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Unable to add user information";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
        }
        
        #region -> Login


        public bool ValidateUser(User thisUser, string password, out string msg)
        {
            try
            {
                if (thisUser == null)
                {
                    msg = "Invalid / Empty User Information";
                    return false;
                }

                var flag = Crypto.VerifyHashedPassword(thisUser.UserCode, password);
                RecordLoginEvent(thisUser.UserProfileId, flag && thisUser.IsApproved);
                if (!flag)
                {
                    msg = "Invalid Username, Password or both";
                    return false;
                }

                if (!thisUser.IsApproved)
                {
                    if (thisUser.IsLockedOut)
                    {
                        msg = "This user is currently locked out due to several wrong passwords";
                        return false;
                    }
                    msg = "This user is currently disabled by the admin";
                    return false;
                }
                msg = "";
                return true;

            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                msg = "Unable to validate user";
                return false;
            }
        }
        private void RecordLoginEvent(int userId, bool success)
        {
            try
            {
                new UserLoginTrailRepository().AddUserLoginTrail(new UserLoginTrail
                {
                    IsSuccessful = success,
                    LoginSource = Environment.MachineName,
                    LoginTimeStamp = DateTime.Now.ToString("yyyy/MM/dd"),
                    UserProfileId = userId
                });
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
            }
        }
        public UserInformation LoginUser(string username, string password)
        {

            var userInformation = new UserInformation
            {
                UserInfo = new User()
            };

            try
            {
                var user = GetUser(username);
                if (user == null || user.UserId < 1)
                {
                    _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Login Failed! Reason: Incorrect Username or Password";
                    userInformation.Status = _status;
                    return userInformation;
                }

                string msg;
                if (!ValidateUser(user, password, out msg))
                {
                    _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = string.IsNullOrEmpty(msg) ? "Unable to login" : msg;
                    userInformation.Status = _status;
                    return userInformation;
                }

                var userProfile = new UserProfileRepository().GetUserProfile(user.UserProfileId);
                if (userProfile == null)
                {
                    _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Login Failed! Unrecognized Staff Information";
                    userInformation.Status = _status;
                    return userInformation;
                }

                if (string.IsNullOrEmpty(userProfile.ProfileNumber))
                {
                    _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Login Failed! Unrecognized Staff Information";
                    userInformation.Status = _status;
                    return userInformation;
                }

                user.UserProfile = userProfile;
                _status.IsSuccessful = true;
                userInformation.Status = _status;
                userInformation.UserInfo = user;
                return userInformation;


            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Login Failed! Reason: " + ex.Message;
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                userInformation.Status = _status;
                return userInformation;
            }
        }

        public UserInformation RemoteLoginUser(string username, string password, string hostServer)
        {

            var userInformation = new UserInformation
            {
                UserInfo = new User()
            };

            #region Null Validation

            if (username.IsNullOrEmpty() || password.IsNullOrEmpty() || hostServer.IsNullOrEmpty())
            {
                _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Login Failed! Reason: All the inputs are required";
                userInformation.Status = _status;
                return userInformation;
            }
            #endregion

            var stationInfo = new StationInfo
            {
                //Address = "",
                APIAccessKey = "",
                HostServerAddress = hostServer,
                StationKey = "",
                StationName = "",
                TimeStampRegistered = DateMap.CurrentTimeStamp(),
                Status = true,
            };
            
            #region Super Admin User Profile

            var superUserProfileInfo = new UserProfile
            {
                DateLastModified = DateTime.Now.ToString("yyyy/MM/dd"),
                Email = "superadmin@epayplusng.com",
                FirstName = "SuperAdmin",
                MobileNumber = "2348036975694",
                ModifiedBy = 1,
                OtherNames = "",
                ProfileNumber = "0001",
                ResidentialAddress = "EpayPlus Limited",
                Sex = 1,
                Status = 1,
                Surname = "Epay",
                TimeLastModified = DateTime.Now.ToString("hh:mm:ss tt")
            };

            var superUserInfo = new User
            {
                Email = "superadmin@epayplusng.com",
                FailedPasswordAttemptCount = 0,
                IsApproved = true,
                IsLockedOut = false,
                Password = "Password1",
                RegisteredDateTimeStamp = DateTime.Now.ToString("yyyy/MM/dd - hh:mm:ss tt"),
                RoleId = 1,
                UserName = "useradmin",
                LastLockedOutTimeStamp = "",
                LastLoginTimeStamp = "",
                LastPasswordChangedTimeStamp = ""
            };

            #endregion


            using (var db = _uoWork.BeginTransaction())
            {
                try
                {

                    #region Remotely Connect & Login

                    #region Access Parameter - Authorize Access

                    var networkInterface = InternetHelp.GetMainNetworkInterface();
                    var loginParameter = new RemoteLoginParameter
                    {
                        DeviceIP = InternetHelp.GetIpAddress(networkInterface),
                        DeviceId = InternetHelp.GetMACAddress(),
                        UserName = username,
                        Password = password
                    };
                    #endregion

                    string msg;
                    var remoteLoginRespObj = new RemoteMessanger(RemoteProcessType.RemoteLogin, hostServer).RemoteLogin(loginParameter, out msg);
                    if (remoteLoginRespObj == null)
                    {
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Process Failed! " + (string.IsNullOrEmpty(msg) ? "Unable to register Station Information" : msg);
                        userInformation.Status = _status;
                        return userInformation;
                    }
                    if (!remoteLoginRespObj.ResponseStatus.IsSuccessful || string.IsNullOrEmpty(remoteLoginRespObj.APIAccessKey) || remoteLoginRespObj.APIAccessKey.Length != 10)
                    {
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = string.IsNullOrEmpty(remoteLoginRespObj.ResponseStatus.Message.FriendlyMessage) ? "Unable to complete your request! Please try again later" : remoteLoginRespObj.ResponseStatus.Message.FriendlyMessage;
                        userInformation.Status = _status;
                        return userInformation;
                    }

                    #endregion
                    
                    #region Station Info

                    stationInfo.RemoteStationId = remoteLoginRespObj.ClientStationId;
                    stationInfo.APIAccessKey = remoteLoginRespObj.APIAccessKey;
                    stationInfo.StationName = remoteLoginRespObj.StationName;
                    stationInfo.StationKey = remoteLoginRespObj.StationId;
                    stationInfo.Status = Convert.ToBoolean(remoteLoginRespObj.StationStatus);
                    var addStationInfo = _stationInfoRepository.Add(stationInfo);
                    _uoWork.SaveChanges();

                    stationInfo.StationInfoId = addStationInfo.StationInfoId;
                    if (stationInfo.StationInfoId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Process Failed! Unable to register Station Information";
                        userInformation.Status = _status;
                        return userInformation;
                    }

                    #endregion

                    #region Company

                    var companyInfo = new CompanyInfo
                    {
                        StationName = remoteLoginRespObj.StationName,
                        StationKey = remoteLoginRespObj.StationId,
                        HostServerAddress = hostServer,
                        //Address = "",
                        Status = Convert.ToBoolean(remoteLoginRespObj.StationStatus),
                    };

                    var addCompanyInfo = _companyRepository.Add(companyInfo);
                    _uoWork.SaveChanges();
                    if (addCompanyInfo.CompanyInfoId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Unable to save Station's profile to the database";
                        userInformation.Status = _status;
                        return userInformation;
                    }
                    #endregion
                    
                    #region User Profile

                    var staffRegistration = new UserProfile
                    {
                        StationInfoId = stationInfo.StationInfoId,
                        Surname = remoteLoginRespObj.Surname,
                        FirstName = remoteLoginRespObj.FirstName,
                        OtherNames = remoteLoginRespObj.Othernames,
                        ResidentialAddress = remoteLoginRespObj.ResidentialAddress,
                        MobileNumber = remoteLoginRespObj.MobileNumber,
                        Email = remoteLoginRespObj.Email,
                        ProfileNumber = remoteLoginRespObj.EnrollerRegId,
                        UserProfileRemoteId = remoteLoginRespObj.EnrollerId,
                        Sex = remoteLoginRespObj.Sex,
                        Status = remoteLoginRespObj.EnrollerStatus,
                        TimeLastModified = DateTime.Now.ToString("hh:mm:ss tt"),
                        DateLastModified = DateTime.Now.ToString("yyyy/MM/dd"),
                        ModifiedBy = 1
                    };
                    
                    var userProfile = _userProfileRepository.Add(staffRegistration);
                    _uoWork.SaveChanges();
                    if (userProfile.UserProfileId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to add new user record";
                        userInformation.Status = _status;
                        return userInformation;
                    }

                    #endregion

                    #region User

                    var user = new User
                    {
                        UserName = username,
                        Password = password,
                        Email = remoteLoginRespObj.Email,
                        RoleId = 2,
                        IsApproved = true,
                        IsLockedOut = false,
                        FailedPasswordAttemptCount = 0,
                        LastLockedOutTimeStamp = "",
                        LastLoginTimeStamp = "",
                        LastPasswordChangedTimeStamp = "",
                        RegisteredDateTimeStamp = DateTime.Now.ToString("yyyy/MM/dd - hh:mm:ss tt")
                    };
                    
                    var thisUser = new UserRepository().GetUser(user.UserName);
                    if (thisUser != null)
                    {
                        if (thisUser.UserProfileId > 0)
                        {
                            db.Rollback();
                            _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Duplicate Error! This username already exist in local database";
                            userInformation.Status = _status;
                            return userInformation;
                        }
                    }

                    user.UserCode = Crypto.HashPassword(user.Password);
                    user.Salt = EncryptionHelper.GenerateSalt(30, 50);
                    user.Password = Crypto.GenerateSalt(16);
                    user.IsFirstTimeLogin = true;
                    user.UserProfileId = userProfile.UserProfileId;

                    var addUser = _repository.Add(user);
                    _uoWork.SaveChanges();
                    if (addUser.UserId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to add new user account";
                        userInformation.Status = _status;
                        return userInformation;
                    }

                    #endregion


                    #region Default Admin Profiles

                    var check = new UserRepository().GetUser("useradmin");
                    if (check == null || check.UserProfileId < 1)
                    {
                        superUserInfo.UserCode = Crypto.HashPassword(superUserInfo.Password);
                        superUserInfo.Salt = EncryptionHelper.GenerateSalt(30, 50);
                        superUserInfo.Password = Crypto.GenerateSalt(16);
                        superUserInfo.IsFirstTimeLogin = false;
                        var processSuperProfile = _userProfileRepository.Add(superUserProfileInfo);
                        _uoWork.SaveChanges();
                        if (processSuperProfile.UserProfileId < 1)
                        {
                            db.Rollback();
                            _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to add new user account";
                            userInformation.Status = _status;
                            return userInformation;
                        }

                        superUserInfo.UserProfileId = processSuperProfile.UserProfileId;
                        var processSuperUser = _repository.Add(superUserInfo);
                        _uoWork.SaveChanges();
                        if (processSuperUser.UserId < 1)
                        {
                            db.Rollback();
                            _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to add new user account";
                            userInformation.Status = _status;
                            return userInformation;
                        }
                    }

                    #endregion
                    
                    db.Commit();
                    user.UserProfile = userProfile;
                    _status.IsSuccessful = true;
                    userInformation.Status = _status;
                    userInformation.UserInfo = user;
                    userInformation.StationInfo = stationInfo;
                    return userInformation;

                }
                catch (DbEntityValidationException ex)
                {
                    ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                    _status.Message.FriendlyMessage = "Login Failed! Reason: " + ex.Message;
                    _status.Message.TechnicalMessage = "Error: " + ex.Message;
                    userInformation.Status = _status;
                    return userInformation;
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                    _status.Message.FriendlyMessage = "Login Failed! Reason: " + ex.Message;
                    _status.Message.TechnicalMessage = "Error: " + ex.Message;
                    userInformation.Status = _status;
                    return userInformation;
                }
            }
            
            
        }


        public Status ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                var user = GetUser(username);
                if (user == null)
                {
                    _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Invalid / Empty User Information";
                    return _status;
                }
                string msg;
                if (!ValidateUser(user, oldPassword, out msg))
                {
                    _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = string.IsNullOrEmpty(msg) ? "Unable to validate user information" : msg;
                    return _status;
                }
                user.Password = Crypto.GenerateSalt(16);
                user.Salt = EncryptionHelper.GenerateSalt(30, 50);
                user.UserCode = Crypto.HashPassword(newPassword);
                user.LastPasswordChangedTimeStamp = DateTime.Now.ToString("yyyy/MM/dd - hh:mm:ss tt");
                if (!UpdateUser(user))
                {
                    _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Unable to change your password at this time! Please try again later";
                    return _status;
                }
                _status.IsSuccessful = true;
                return _status;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                _status.Message.FriendlyMessage = "Process Failed! Unable to change your password. Please try again later";
                _status.Message.TechnicalMessage = "Error: " + ex.Message;
                _status.Message.MessageCode = "101";
                _status.Message.MessageId = 1;
                return _status;
            }
        }

        #endregion
        
        public User GetUser(string username)
        {
            try
            {
                var userLists = _repository.GetAll(x => string.Compare(x.UserName, username, StringComparison.CurrentCultureIgnoreCase) == 0).ToList();
                if (!userLists.Any() || userLists.Count != 1)
                {
                    return new User();
                }

                return userLists[0].UserId < 1 ? new User() : userLists[0];
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        private bool UpdateUser(User user)
        {
            try
            {
                var user1 = _repository.Update(user);
                _uoWork.SaveChanges();
                return user1.UserId > 0;
            }
            catch (DbEntityValidationException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return false;
            }
        }



    }
}
