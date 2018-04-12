using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows.Forms;
using BioEnumerator.API;
using BioEnumerator.APIServer.APIObjs;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.Infrastructure;
using BioEnumerator.DataAccessManager.Infrastructure.Contract;
using BioEnumerator.DataAccessManager.Repository.Helpers;
using BioEnumerator.RemoteHelper.ExternalTool;
using BioEnumerator.RemoteHelper.MessengerService;
using RestSharp;
using XPLUG.WINDOWTOOLS;
using XPLUG.WINDOWTOOLS.Date;
using XPLUG.WINDOWTOOLS.Extensions;
using Message = BioEnumerator.DataAccessManager.DataContract.Message;
using ResponseStatus = BioEnumerator.APIServer.APIObjs.ResponseStatus;

namespace BioEnumerator.DataAccessManager.Repository
{
    internal class UserProfileRepository
    {

        private Status _status = new Status
        {
            IsSuccessful = false,
            Message = new Message(),
            ReturnedId = 0
        };

        private readonly IBioEnumeratorRepository<UserProfile> _repository;
        private readonly IBioEnumeratorRepository<StationInfo> _stationInfoRepository;
        private readonly IBioEnumeratorRepository<CompanyInfo> _companyRepository;
        private readonly IBioEnumeratorRepository<User> _userRepository; 
        private readonly BioEnumeratorUoWork _uoWork;

        public UserProfileRepository()
        {
            _uoWork = new BioEnumeratorUoWork();
            _repository = new BioEnumeratorRepository<UserProfile>(_uoWork);
            _stationInfoRepository = new BioEnumeratorRepository<StationInfo>(_uoWork);
            _companyRepository = new BioEnumeratorRepository<CompanyInfo>(_uoWork);
            _userRepository = new BioEnumeratorRepository<User>(_uoWork);
        }


        #region CRUD


        public Status AddCompanyAndUserProfile(CompanyInfo companyInfo, UserProfile staffRegistration, User user)
        {

            #region Null Validation

            if (companyInfo.Equals(null))
            {
                _status.Message.FriendlyMessage = "Station Information is empty / invalid";
                _status.Message.TechnicalMessage = "Station Information is empty / invalid";
                return _status;
            }

            if (user.Equals(null))
            {
                _status.Message.FriendlyMessage = "Admin User Information is empty / invalid";
                _status.Message.TechnicalMessage = "Admin User Information is empty / invalid";
                return _status;
            }

            if (staffRegistration.Equals(null))
            {
                _status.Message.FriendlyMessage = "Admin Profile Information is empty / invalid";
                _status.Message.TechnicalMessage = "Admin Profile Information is empty / invalid";
                return _status;
            }

            //var companyInfo = userDetail.CompanyInfo;
            //var user = userDetail.User;
            //var userProfileDetail = userDetail.UserProfile;

            #endregion

            var stationInfo = new StationInfo
            {
                //Address = "",
                APIAccessKey = "",
                HostServerAddress = companyInfo.HostServerAddress,
                StationKey = companyInfo.StationKey,
                StationName = companyInfo.StationName,
                TimeStampRegistered = DateMap.CurrentTimeStamp(),
                Status = companyInfo.Status,
            };

            #region Model Validation

            // Validation

            List<ValidationResult> valResults;
            if (!EntityValidatorHelper.Validate(companyInfo, out valResults))
            {
                var errorDetail = new StringBuilder();
                if (!valResults.IsNullOrEmpty())
                {
                    errorDetail.AppendLine("Following error occurred:");
                    valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                }
                else
                {
                    errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                }
                _status.Message.FriendlyMessage = errorDetail.ToString();
                _status.Message.TechnicalMessage = errorDetail.ToString();
                _status.IsSuccessful = false;
                return _status;
            }

            if (!EntityValidatorHelper.Validate(staffRegistration, out valResults))
            {
                var errorDetail = new StringBuilder();
                if (!valResults.IsNullOrEmpty())
                {
                    errorDetail.AppendLine("Following error occurred:");
                    valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                }
                else
                {
                    errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                }
                _status.Message.FriendlyMessage = errorDetail.ToString();
                _status.Message.TechnicalMessage = errorDetail.ToString();
                _status.IsSuccessful = false;
                return _status;
            }

            if (!EntityValidatorHelper.Validate(user, out valResults))
            {
                var errorDetail = new StringBuilder();
                if (!valResults.IsNullOrEmpty())
                {
                    errorDetail.AppendLine("Following error occurred:");
                    valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                }
                else
                {
                    errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                }
                _status.Message.FriendlyMessage = errorDetail.ToString();
                _status.Message.TechnicalMessage = errorDetail.ToString();
                _status.IsSuccessful = false;
                return _status;
            }

            if (!EntityValidatorHelper.Validate(stationInfo, out valResults))
            {
                var errorDetail = new StringBuilder();
                if (!valResults.IsNullOrEmpty())
                {
                    errorDetail.AppendLine("Following error occurred:");
                    valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                }
                else
                {
                    errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                }
                _status.Message.FriendlyMessage = errorDetail.ToString();
                _status.Message.TechnicalMessage = errorDetail.ToString();
                _status.IsSuccessful = false;
                return _status;
            }

            #endregion

            #region Super Admin User Profile

            var superUserProfileInfo = new UserProfile
            {
                DateLastModified = DateTime.Now.ToString("yyyy/MM/dd"),
                Email = "admin@bioenroll.com.ng",
                FirstName = "Useradmin",
                MobileNumber = "2348036975694",
                ModifiedBy = 1,
                OtherNames = "",
                ProfileNumber = "0001",
                ResidentialAddress = "EpayPlus Limited",
                Sex = 1,
                Status = 1,
                Surname = "Bio Enroll Admin",
                TimeLastModified = DateTime.Now.ToString("hh:mm:ss tt")
            };

            var superUserInfo = new User
            {
                Email = "admin@bioenroll.com.ng",
                FailedPasswordAttemptCount = 0,
                IsApproved = true,
                IsLockedOut = false,
                Password = "Password",
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

                    #region Access Parameter - Authorize Access

                    var networkInterface = InternetHelp.GetMainNetworkInterface();
                    var authParameter = new AccessParameter
                    {
                        DeviceIP = InternetHelp.GetIpAddress(networkInterface),
                        DeviceId = InternetHelp.GetMACAddress(),
                        StationName = stationInfo.StationName,
                        StationId = stationInfo.StationKey,
                        Surname = staffRegistration.Surname,
                        FirstName = staffRegistration.FirstName,
                        UserName = user.UserName,
                        MobileNumber = staffRegistration.MobileNumber,
                        Email = staffRegistration.Email
                    };

                    string msg;
                    //var stationRespObj = new RemoteMessanger(stationInfo.HostServerAddress).AuthourizeAccess(authParameter, out msg);
                    //MessageBox.Show(msg, @"Process Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                    var stationRespObj = new RemoteMessanger(RemoteProcessType.AuthorizeStationAccess, stationInfo.HostServerAddress).AuthourizeAccess(authParameter, out msg);
                    if (stationRespObj == null)
                    {
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Process Failed! Unable to register Station Information";
                        return _status;
                    }
                    if (!stationRespObj.ResponseStatus.IsSuccessful || string.IsNullOrEmpty(stationRespObj.APIAccessKey) || stationRespObj.APIAccessKey.Length != 10)
                    {
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = string.IsNullOrEmpty(stationRespObj.ResponseStatus.Message.FriendlyMessage) ? "Unable to complete your request! Please try again later" : stationRespObj.ResponseStatus.Message.FriendlyMessage;
                        return _status;
                    }


                    stationInfo.APIAccessKey = stationRespObj.APIAccessKey;
                    stationInfo.StationName = stationRespObj.StationName;
                    var addStationInfo = _stationInfoRepository.Add(stationInfo);
                    _uoWork.SaveChanges();

                    stationInfo.StationInfoId = addStationInfo.StationInfoId;
                    if (stationInfo.StationInfoId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Process Failed! Unable to register Station Information";
                        return _status;
                    }


                    #endregion

                    #region Company
                    var addCompanyInfo = _companyRepository.Add(companyInfo);
                    _uoWork.SaveChanges();
                    if (addCompanyInfo.CompanyInfoId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Unable to save Station's profile to the database";
                        return _status;
                    }

                    #endregion

                    #region User Profile

                    staffRegistration.ProfileNumber = stationRespObj.EnrollerRegId;
                    staffRegistration.UserProfileRemoteId = stationRespObj.EnrollerId;

                    var userProfile = _repository.Add(staffRegistration);
                    _uoWork.SaveChanges();
                    if (userProfile.UserProfileId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to add new user record";
                        return _status;
                    }

                    #endregion

                    #region User

                    if (string.IsNullOrEmpty(user.UserName))
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Invalid / Empty User Information";
                        return _status;
                    }

                    var thisUser = new UserRepository().GetUser(user.UserName);
                    if (thisUser != null)
                    {
                        if (thisUser.UserProfileId > 0)
                        {
                            db.Rollback();
                            _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Duplicate Error! This username already exist";
                            return _status;
                        }
                    }

                    user.UserCode = Crypto.HashPassword(user.Password);
                    user.Salt = EncryptionHelper.GenerateSalt(30, 50);
                    user.Password = Crypto.GenerateSalt(16);
                    user.IsFirstTimeLogin = true;
                    user.UserProfileId = userProfile.UserProfileId;

                    var addUser = _userRepository.Add(user);
                    _uoWork.SaveChanges();
                    if (addUser.UserId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to add new user account";
                        return _status;
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
                        var processSuperProfile = _repository.Add(superUserProfileInfo);
                        _uoWork.SaveChanges();
                        if (processSuperProfile.UserProfileId < 1)
                        {
                            db.Rollback();
                            _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to add new user account";
                            return _status;
                        }
                        
                        superUserInfo.UserProfileId = processSuperProfile.UserProfileId;
                        var processSuperUser = _userRepository.Add(superUserInfo);
                        _uoWork.SaveChanges();
                        if (processSuperUser.UserId < 1)
                        {
                            db.Rollback();
                            _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to add new user account";
                            return _status;
                        }
                    }

                    #endregion
                    


                    db.Commit();
                    _status.ReturnedId = companyInfo.CompanyInfoId;
                    _status.UserId = addUser.UserId;
                    _status.IsSuccessful = true;
                    _status.StationInfo = stationInfo;
                    return _status;

                }
                catch (DbEntityValidationException ex)
                {
                    ErrorManager.LogApplicationError((ex).StackTrace, (ex).Source, (ex).Message);
                    _status.Message.FriendlyMessage = "Process Failed! Unable complete registration ";
                    _status.Message.TechnicalMessage = "Error: " + (ex).Message;
                    _status.Message.MessageCode = "101";
                    _status.Message.MessageId = 1;
                    return _status;
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                    _status.Message.FriendlyMessage = "Process Failed! Unable complete registration ";
                    _status.Message.TechnicalMessage = "Error: " + ex.Message;
                    _status.Message.MessageCode = "101";
                    _status.Message.MessageId = 1;
                    return _status;
                }
            }

        }


        public List<UserProfile> GetUserProfiles()
        {
            try
            {
                var all = _repository.GetAll().ToList();
                return !all.Any() ? new List<UserProfile>() : all.ToList();
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        } 

        public Status CompanyAndUserProfile(UserDetailObj userDetail)
        {

            #region Null Validation

            if (userDetail.CompanyInfo.Equals(null))
            {
                _status.Message.FriendlyMessage = "Station Information is empty / invalid";
                _status.Message.TechnicalMessage = "Station Information is empty / invalid";
                return _status;
            }

            if (userDetail.User.Equals(null))
            {
                _status.Message.FriendlyMessage = "Admin User Information is empty / invalid";
                _status.Message.TechnicalMessage = "Admin User Information is empty / invalid";
                return _status;
            }

            if (userDetail.UserProfile.Equals(null))
            {
                _status.Message.FriendlyMessage = "Admin Profile Information is empty / invalid";
                _status.Message.TechnicalMessage = "Admin Profile Information is empty / invalid";
                return _status;
            }

            var companyInfo = userDetail.CompanyInfo;
            var user = userDetail.User;
            var userProfileDetail = userDetail.UserProfile;

            #endregion

            var stationInfo = new StationInfo
            {
                //Address = "",
                APIAccessKey = "",
                HostServerAddress = companyInfo.HostServerAddress,
                StationKey = companyInfo.StationKey,
                StationName = companyInfo.StationName,
                TimeStampRegistered = DateMap.CurrentTimeStamp(),
                Status = companyInfo.Status,
            };
            
            #region Model Validation
            
            // Validation

            List<ValidationResult> valResults;
            if (!EntityValidatorHelper.Validate(companyInfo, out valResults))
            {
                var errorDetail = new StringBuilder();
                if (!valResults.IsNullOrEmpty())
                {
                    errorDetail.AppendLine("Following error occurred:");
                    valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                }
                else
                {
                    errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                }
                _status.Message.FriendlyMessage = errorDetail.ToString();
                _status.Message.TechnicalMessage = errorDetail.ToString();
                _status.IsSuccessful = false;
                return _status;
            }

            if (!EntityValidatorHelper.Validate(userProfileDetail, out valResults))
            {
                var errorDetail = new StringBuilder();
                if (!valResults.IsNullOrEmpty())
                {
                    errorDetail.AppendLine("Following error occurred:");
                    valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                }
                else
                {
                    errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                }
                _status.Message.FriendlyMessage = errorDetail.ToString();
                _status.Message.TechnicalMessage = errorDetail.ToString();
                _status.IsSuccessful = false;
                return _status;
            }

            if (!EntityValidatorHelper.Validate(user, out valResults))
            {
                var errorDetail = new StringBuilder();
                if (!valResults.IsNullOrEmpty())
                {
                    errorDetail.AppendLine("Following error occurred:");
                    valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                }
                else
                {
                    errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                }
                _status.Message.FriendlyMessage = errorDetail.ToString();
                _status.Message.TechnicalMessage = errorDetail.ToString();
                _status.IsSuccessful = false;
                return _status;
            }

            if (!EntityValidatorHelper.Validate(stationInfo, out valResults))
            {
                var errorDetail = new StringBuilder();
                if (!valResults.IsNullOrEmpty())
                {
                    errorDetail.AppendLine("Following error occurred:");
                    valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                }
                else
                {
                    errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                }
                _status.Message.FriendlyMessage = errorDetail.ToString();
                _status.Message.TechnicalMessage = errorDetail.ToString();
                _status.IsSuccessful = false;
                return _status;
            }

            #endregion

            #region Model Values Mapping

            var userProfileInfo = new UserProfile
            {
                DateLastModified = DateTime.Now.ToString("yyyy/MM/dd"),
                Email = "admin@taxserve.com.ng",
                FirstName = "Useradmin",
                MobileNumber = "2348039087453",
                ModifiedBy = 1,
                OtherNames = "",
                ProfileNumber = "0001",
                ResidentialAddress = "Tax Serve Limited",
                Sex = 1,
                Status = 1,
                Surname = "Tax Serve Admin",
                TimeLastModified = DateTime.Now.ToString("hh:mm:ss tt")
            };

            var userInfo = new User
            {
                Email = "admin@taxserve.com.ng",
                FailedPasswordAttemptCount = 0,
                IsApproved = true,
                IsLockedOut = false,
                Password = "555555%#",
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

                    #region Access Parameter - Authorize Access

                    var networkInterface = InternetHelp.GetMainNetworkInterface();
                    var authParameter = new AccessParameter
                    {
                        DeviceIP = InternetHelp.GetIpAddress(networkInterface),
                        DeviceId = InternetHelp.GetMACAddress(),
                        StationName = stationInfo.StationName,
                        StationId = stationInfo.StationKey
                    };

                    string msg;
                    var stationRespObj = new RemoteMessanger(stationInfo.HostServerAddress).AuthourizeAccess(authParameter, out msg);
                    if (stationRespObj == null)
                    {
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Process Failed! Unable to register Station Information";
                        return _status;
                    }
                    if (!stationRespObj.ResponseStatus.IsSuccessful || string.IsNullOrEmpty(stationRespObj.APIAccessKey) || stationRespObj.APIAccessKey.Length != 10)
                    {
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = string.IsNullOrEmpty(stationRespObj.ResponseStatus.Message.FriendlyMessage) ? "Unable to complete your request! Please try again later" : stationRespObj.ResponseStatus.Message.FriendlyMessage;
                        return _status;
                    }

                    stationInfo.APIAccessKey = stationRespObj.APIAccessKey;
                    var addStationInfo = _stationInfoRepository.Add(stationInfo);
                    _uoWork.SaveChanges();

                    stationInfo.StationInfoId = addStationInfo.StationInfoId;
                    if (stationInfo.StationInfoId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Process Failed! Unable to register Station Information";
                        return _status;
                    }


                    #endregion

                    #region Company

                    var addCompanyInfo = _companyRepository.Add(companyInfo);
                    _uoWork.SaveChanges();
                    if (addCompanyInfo.CompanyInfoId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Unable to save Station's profile to the database";
                        return _status;
                    }

                    #endregion

                    #region User Profile

                    var userProfile = _repository.Add(userProfileDetail);
                    _uoWork.SaveChanges();
                    if (userProfile.UserProfileId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to add new user record";
                        return _status;
                    }

                    #endregion
                    
                    #region User
                    
                    if (string.IsNullOrEmpty(user.UserName))
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Invalid / Empty User Information";
                        return _status;
                    }

                    var thisUser = new UserRepository().GetUser(user.UserName);
                    if (thisUser != null)
                    {
                        if (thisUser.UserProfileId > 0)
                        {
                            db.Rollback();
                            _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Duplicate Error! This username already exist";
                            return _status;
                        }
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "This username already exist";
                        return _status;
                    }

                    user.UserCode = Crypto.HashPassword(user.Password);
                    user.Salt = EncryptionHelper.GenerateSalt(30, 50);
                    user.Password = Crypto.GenerateSalt(16);
                    user.IsFirstTimeLogin = true;
                    user.UserProfileId = userProfile.UserProfileId;

                    var addUser = _userRepository.Add(user);
                    _uoWork.SaveChanges();
                    if (addUser.UserId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to add new user account";
                        return _status;
                    }


                    #endregion


                    db.Commit();
                    _status.ReturnedId = companyInfo.CompanyInfoId;
                    _status.UserId = addUser.UserId;
                    _status.IsSuccessful = true;
                    _status.StationInfo = stationInfo;
                    return _status;

                }
                catch (DbEntityValidationException ex)
                {
                    ErrorManager.LogApplicationError((ex).StackTrace, (ex).Source, (ex).Message);
                    _status.Message.FriendlyMessage = "Process Failed! Unable complete registration ";
                    _status.Message.TechnicalMessage = "Error: " + (ex).Message;
                    _status.Message.MessageCode = "101";
                    _status.Message.MessageId = 1;
                    return _status;
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                    _status.Message.FriendlyMessage = "Process Failed! Unable complete registration ";
                    _status.Message.TechnicalMessage = "Error: " + ex.Message;
                    _status.Message.MessageCode = "101";
                    _status.Message.MessageId = 1;
                    return _status;
                }
            }

        }

        public Status AddRemoteStationUser(UserDetailObj userDetail)
        {

            #region Null Validation

            if (userDetail.CompanyInfo.Equals(null))
            {
                _status.Message.FriendlyMessage = "Station Information is empty / invalid";
                _status.Message.TechnicalMessage = "Station Information is empty / invalid";
                return _status;
            }

            if (userDetail.User.Equals(null))
            {
                _status.Message.FriendlyMessage = "Admin User Information is empty / invalid";
                _status.Message.TechnicalMessage = "Admin User Information is empty / invalid";
                return _status;
            }

            if (userDetail.UserProfile.Equals(null))
            {
                _status.Message.FriendlyMessage = "Admin Profile Information is empty / invalid";
                _status.Message.TechnicalMessage = "Admin Profile Information is empty / invalid";
                return _status;
            }

            var companyInfo = userDetail.CompanyInfo;
            var user = userDetail.User;
            var userProfileDetail = userDetail.UserProfile;

            #endregion

            var stationInfo = new StationInfo
            {
                //Address = "",
                APIAccessKey = "",
                HostServerAddress = companyInfo.HostServerAddress,
                StationKey = companyInfo.StationKey,
                StationName = companyInfo.StationName,
                TimeStampRegistered = DateMap.CurrentTimeStamp(),
                Status = companyInfo.Status,
            };

            #region Model Validation

            // Validation

            List<ValidationResult> valResults;
            if (!EntityValidatorHelper.Validate(companyInfo, out valResults))
            {
                var errorDetail = new StringBuilder();
                if (!valResults.IsNullOrEmpty())
                {
                    errorDetail.AppendLine("Following error occurred:");
                    valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                }
                else
                {
                    errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                }
                _status.Message.FriendlyMessage = errorDetail.ToString();
                _status.Message.TechnicalMessage = errorDetail.ToString();
                _status.IsSuccessful = false;
                return _status;
            }

            if (!EntityValidatorHelper.Validate(userProfileDetail, out valResults))
            {
                var errorDetail = new StringBuilder();
                if (!valResults.IsNullOrEmpty())
                {
                    errorDetail.AppendLine("Following error occurred:");
                    valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                }
                else
                {
                    errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                }
                _status.Message.FriendlyMessage = errorDetail.ToString();
                _status.Message.TechnicalMessage = errorDetail.ToString();
                _status.IsSuccessful = false;
                return _status;
            }

            if (!EntityValidatorHelper.Validate(user, out valResults))
            {
                var errorDetail = new StringBuilder();
                if (!valResults.IsNullOrEmpty())
                {
                    errorDetail.AppendLine("Following error occurred:");
                    valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                }
                else
                {
                    errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                }
                _status.Message.FriendlyMessage = errorDetail.ToString();
                _status.Message.TechnicalMessage = errorDetail.ToString();
                _status.IsSuccessful = false;
                return _status;
            }

            if (!EntityValidatorHelper.Validate(stationInfo, out valResults))
            {
                var errorDetail = new StringBuilder();
                if (!valResults.IsNullOrEmpty())
                {
                    errorDetail.AppendLine("Following error occurred:");
                    valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                }
                else
                {
                    errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                }
                _status.Message.FriendlyMessage = errorDetail.ToString();
                _status.Message.TechnicalMessage = errorDetail.ToString();
                _status.IsSuccessful = false;
                return _status;
            }

            #endregion

            #region Model Values Mapping

            var userProfileInfo = new UserProfile
            {
                DateLastModified = DateTime.Now.ToString("yyyy/MM/dd"),
                Email = "admin@taxserve.com.ng",
                FirstName = "Useradmin",
                MobileNumber = "2348039087453",
                ModifiedBy = 1,
                OtherNames = "",
                ProfileNumber = "0001",
                ResidentialAddress = "Tax Serve Limited",
                Sex = 1,
                Status = 1,
                Surname = "Tax Serve Admin",
                TimeLastModified = DateTime.Now.ToString("hh:mm:ss tt")
            };

            var userInfo = new User
            {
                Email = "admin@taxserve.com.ng",
                FailedPasswordAttemptCount = 0,
                IsApproved = true,
                IsLockedOut = false,
                Password = "555555%#",
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

                    #region Access Parameter - Authorize Access

                    var networkInterface = InternetHelp.GetMainNetworkInterface();
                    var authParameter = new AccessParameter
                    {
                        DeviceIP = InternetHelp.GetIpAddress(networkInterface),
                        DeviceId = InternetHelp.GetMACAddress(),
                        StationName = stationInfo.StationName,
                        StationId = stationInfo.StationKey
                    };

                    string msg;
                    var stationRespObj = new RemoteMessanger(stationInfo.HostServerAddress).AuthourizeAccess(authParameter, out msg);
                    if (stationRespObj == null)
                    {
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Process Failed! Unable to register Station Information";
                        return _status;
                    }
                    if (!stationRespObj.ResponseStatus.IsSuccessful || string.IsNullOrEmpty(stationRespObj.APIAccessKey) || stationRespObj.APIAccessKey.Length != 10)
                    {
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = string.IsNullOrEmpty(stationRespObj.ResponseStatus.Message.FriendlyMessage) ? "Unable to complete your request! Please try again later" : stationRespObj.ResponseStatus.Message.FriendlyMessage;
                        return _status;
                    }

                    stationInfo.APIAccessKey = stationRespObj.APIAccessKey;
                    var addStationInfo = _stationInfoRepository.Add(stationInfo);
                    _uoWork.SaveChanges();

                    stationInfo.StationInfoId = addStationInfo.StationInfoId;
                    if (stationInfo.StationInfoId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Process Failed! Unable to register Station Information";
                        return _status;
                    }


                    #endregion

                    #region Company

                    var addCompanyInfo = _companyRepository.Add(companyInfo);
                    _uoWork.SaveChanges();
                    if (addCompanyInfo.CompanyInfoId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Unable to save Station's profile to the database";
                        return _status;
                    }

                    #endregion

                    #region User Profile

                    var userProfile = _repository.Add(userProfileDetail);
                    _uoWork.SaveChanges();
                    if (userProfile.UserProfileId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to add new user record";
                        return _status;
                    }

                    #endregion

                    #region User

                    if (string.IsNullOrEmpty(user.UserName))
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Invalid / Empty User Information";
                        return _status;
                    }

                    var thisUser = new UserRepository().GetUser(user.UserName);
                    if (thisUser != null)
                    {
                        if (thisUser.UserProfileId > 0)
                        {
                            db.Rollback();
                            _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Duplicate Error! This username already exist";
                            return _status;
                        }
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "This username already exist";
                        return _status;
                    }

                    user.UserCode = Crypto.HashPassword(user.Password);
                    user.Salt = EncryptionHelper.GenerateSalt(30, 50);
                    user.Password = Crypto.GenerateSalt(16);
                    user.IsFirstTimeLogin = true;
                    user.UserProfileId = userProfile.UserProfileId;

                    var addUser = _userRepository.Add(user);
                    _uoWork.SaveChanges();
                    if (addUser.UserId < 1)
                    {
                        db.Rollback();
                        _status.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to add new user account";
                        return _status;
                    }


                    #endregion


                    db.Commit();
                    _status.ReturnedId = companyInfo.CompanyInfoId;
                    _status.UserId = addUser.UserId;
                    _status.IsSuccessful = true;
                    _status.StationInfo = stationInfo;
                    return _status;

                }
                catch (DbEntityValidationException ex)
                {
                    ErrorManager.LogApplicationError((ex).StackTrace, (ex).Source, (ex).Message);
                    _status.Message.FriendlyMessage = "Process Failed! Unable complete registration ";
                    _status.Message.TechnicalMessage = "Error: " + (ex).Message;
                    _status.Message.MessageCode = "101";
                    _status.Message.MessageId = 1;
                    return _status;
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                    _status.Message.FriendlyMessage = "Process Failed! Unable complete registration ";
                    _status.Message.TechnicalMessage = "Error: " + ex.Message;
                    _status.Message.MessageCode = "101";
                    _status.Message.MessageId = 1;
                    return _status;
                }
            }

        }



        #region Download Bulk User


        public SyncUserStatus SyncRemoteUsers(List<RemoteUserInfo> remoteUserInfos, ref long totalSync, out string msg)
        {


            var respStatus = new SyncUserStatus
            {
                IsSuccessful = false,
                Message = new Message(),
                UserInfos = new List<User>(),
                UserProfileInfos = new List<UserProfile>()
            };

            #region Null Validation

            if (remoteUserInfos.Equals(null))
            {
                respStatus.Message.FriendlyMessage = "Empty Remote Users downloaded from the Remote Server.";
                respStatus.Message.TechnicalMessage = "Empty Remote Users downloaded from the Remote Server.";
                msg = respStatus.Message.FriendlyMessage;
                return respStatus;
            }
            
            #endregion

            try
            {
                #region Accessing Looping Through

                #region Sample

                //foreach (var item in remoteResponse.TaxCategories)
                //{
                //    var check = GetRawItem(item.Name.Trim());
                //    if (check != null && check.TaxCategoryId > 0)
                //    {
                //        myUpdateList.Add(new TaxCategory
                //        {
                //            Name = item.Name,
                //            Status = item.Status,
                //            TaxCategoryId = check.TaxCategoryId,
                //        });
                //        continue;
                //    }

                //    myAddList.Add(new TaxCategory
                //    {
                //        Name = item.Name,
                //        Status = item.Status,
                //        TaxCategoryId = item.TaxCategoryId
                //    });

                //}

                #endregion

                var myAddUserList = new List<User>();
                var myUpdateUserList = new List<User>();
                var myAddUserProfileList = new List<UserProfile>();
                var myUpdateUserProfileList = new List<UserProfile>();
                var retUserProfileList = new List<UserProfile>();
                var retUserList = new List<User>();
                var c = 0;

                foreach (var remoteUserInfo in remoteUserInfos)
                {

                    var userProfile = remoteUserInfo.UserProfileInfo;
                    var user = remoteUserInfo.UserInfo;

                    #region UserProfile

                    if (userProfile == null || userProfile.FirstName.Count() < 3)
                    {
                        continue;
                    }

                    var check =
                        _repository.GetAll(x => x.Email == userProfile.Email && x.MobileNumber == userProfile.MobileNumber).ToList();
                    if (check.Any() && check.Count() == 1 && check[0].UserProfileId > 0)
                    {
                        myUpdateUserProfileList.Add(new UserProfile
                        {
                            UserProfileId = check[0].UserProfileId,
                            //ClientStationId = userProfile.ClientStationId,
                            ProfileNumber = userProfile.ProfileNumber,
                            Surname = userProfile.Surname,
                            FirstName = userProfile.FirstName,
                            OtherNames = userProfile.OtherNames,
                            //Sex = userProfile.Sex,
                            ResidentialAddress = userProfile.ResidentialAddress,
                            //Email = userProfile.Email,
                            MobileNumber = userProfile.MobileNumber,
                            Status = userProfile.Status
                        });
                    }

                    myAddUserProfileList.Add(new UserProfile
                    {
                        UserProfileId = userProfile.UserProfileId,
                        //ClientStationId = userProfile.ClientStationId,
                        ProfileNumber = userProfile.ProfileNumber,
                        Surname = userProfile.Surname,
                        FirstName = userProfile.FirstName,
                        OtherNames = userProfile.OtherNames,
                        Sex = userProfile.Sex,
                        ResidentialAddress = userProfile.ResidentialAddress,
                        Email = userProfile.Email,
                        MobileNumber = userProfile.MobileNumber,
                        DateLastModified = userProfile.DateLastModified,
                        TimeLastModified = userProfile.TimeLastModified,
                        ModifiedBy = userProfile.ModifiedBy,
                        Status = userProfile.Status
                    });

                    #endregion

                    #region User

                    var check2 =
                        _userRepository.GetAll(x => x.Email == user.Email && x.UserName == user.UserName).ToList();
                    if (check2.Any() && check2.Count() == 1 && check2[0].UserId > 0)
                    {
                        myUpdateUserList.Add(new User
                        {
                            UserId = check2[0].UserId,
                            //UserProfileId = user.UserProfileId,
                            //UserName = user.UserName,
                            //Email = user.Email,
                            //IsFirstTimeLogin = user.IsFirstTimeLogin,
                            //FailedPasswordAttemptCount = user.FailedPasswordAttemptCount,
                            //IsApproved = user.IsApproved,
                            //IsLockedOut = user.IsLockedOut,
                            //LastLockedOutTimeStamp = user.LastLockedOutTimeStamp,
                            //LastLoginTimeStamp = user.LastLoginTimeStamp,
                            LastPasswordChangedTimeStamp = user.LastPasswordChangedTimeStamp,
                            Password = user.Password,
                            //RegisteredDateTimeStamp = user.RegisteredDateTimeStamp,
                            RoleId = user.RoleId,
                            Salt = user.Salt,
                            UserCode = user.UserCode
                        });
                    }

                    myAddUserList.Add(new User
                    {
                        UserId = user.StaffUserId,
                        UserProfileId = user.UserProfileId,
                        UserName = user.UserName,
                        Email = user.Email,
                        IsFirstTimeLogin = user.IsFirstTimeLogin,
                        FailedPasswordAttemptCount = user.FailedPasswordAttemptCount,
                        IsApproved = user.IsApproved,
                        IsLockedOut = user.IsLockedOut,
                        LastLockedOutTimeStamp = user.LastLockedOutTimeStamp,
                        LastLoginTimeStamp = user.LastLoginTimeStamp,
                        LastPasswordChangedTimeStamp = user.LastPasswordChangedTimeStamp,
                        Password = user.Password,
                        RegisteredDateTimeStamp = user.RegisteredDateTimeStamp,
                        RoleId = user.RoleId,
                        Salt = user.Salt,
                        UserCode = user.UserCode
                    });

                    #endregion

                }

                #endregion


                #region Submission

                #region UserProfile

                if (myAddUserProfileList.Any())
                {
                    var processUserProfiles = _repository.AddRange(myAddUserProfileList).ToList();
                    _uoWork.SaveChanges();
                    if (!processUserProfiles.Any())
                    {
                        respStatus.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to update local database";
                        msg = respStatus.Message.FriendlyMessage;
                        return respStatus;
                    }

                    retUserProfileList.AddRange(processUserProfiles);
                    c += processUserProfiles.Count;
                }

                if (myUpdateUserProfileList.Any())
                {
                    var processUserProfiles = _repository.UpdateRange(myAddUserProfileList).ToList();
                    _uoWork.SaveChanges();
                    if (!processUserProfiles.Any())
                    {
                        respStatus.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to update local database";
                        msg = respStatus.Message.FriendlyMessage;
                        return respStatus;
                    }

                    retUserProfileList.AddRange(processUserProfiles);
                    c += processUserProfiles.Count;
                }

                #endregion

                #region User

                if (myAddUserList.Any())
                {
                    var processUsers = _userRepository.AddRange(myAddUserList).ToList();
                    _uoWork.SaveChanges();
                    if (!processUsers.Any())
                    {
                        respStatus.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to update local database";
                        msg = respStatus.Message.FriendlyMessage;
                        return respStatus;
                    }
                    
                    retUserList.AddRange(processUsers);
                    c += processUsers.Count;
                }

                if (myUpdateUserList.Any())
                {
                    var processUsers = _userRepository.UpdateRange(myUpdateUserList).ToList();
                    _uoWork.SaveChanges();
                    if (!processUsers.Any())
                    {
                        respStatus.Message.FriendlyMessage = _status.Message.TechnicalMessage = "Error Occurred! Unable to update local database";
                        msg = respStatus.Message.FriendlyMessage;
                        return respStatus;
                    }

                    retUserList.AddRange(processUsers);
                    c += processUsers.Count;
                }

                #endregion
                
                #endregion

                totalSync = c;
                respStatus.IsSuccessful = true;
                respStatus.UserInfos = retUserList;
                respStatus.UserProfileInfos = retUserProfileList;
                msg = "";
                return respStatus;

            }
            catch (DbEntityValidationException ex)
            {
                ErrorManager.LogApplicationError((ex).StackTrace, (ex).Source, (ex).Message);
                respStatus.Message.FriendlyMessage = "Process Failed! Unable complete registration ";
                respStatus.Message.TechnicalMessage = "Error: " + (ex).Message;
                respStatus.Message.MessageCode = "101";
                respStatus.Message.MessageId = 1;
                msg = respStatus.Message.FriendlyMessage;
                return respStatus;
            }
            
        }

        public BulkUserDetailItemObj DownloadBulkStationUser(AuthAdminUser authUser)
        {

            var response = new BulkUserDetailItemObj
            {
                MainStatus = new ResponseStatus
                {
                    IsSuccessful = false,
                    Message = new ResponseMessage()
                },
                UserDetailObjs = new List<UserDetailObj>()
            };


            try
            {
                string msg;

                return response;
            }
            catch (Exception ex)
            {
                response.MainStatus.Message.FriendlyMessage = "Processing Error Occurred! Please try again later";
                response.MainStatus.Message.TechnicalMessage = "Error: " + ex.Message;
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return response;
            }


        }

        #endregion


        public UserProfile GetUserProfile(int userProfileId)
        {
            try
            {
                var myItem = _repository.GetById(userProfileId);
                if (myItem == null || myItem.UserProfileId < 1)
                {
                    return new UserProfile();
                }

                return myItem;
            }
            catch (Exception ex)
            {
                ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        #endregion


        #region Usefuls Hint

        //new UserProfile
        //{
        //    ClientStationId = 5,
        //    ProfileNumber = "",
        //    Surname = "",
        //    FirstName = "",
        //    OtherNames = "",
        //    Sex = 1,
        //    ResidentialAddress = "",
        //    Email = "",
        //    MobileNumber = "",
        //    DateLastModified = "",
        //    TimeLastModified = "",
        //    ModifiedBy = 1,
        //    Status = 1
        //};


        
        //new User
        //{
        //    UserProfileId = 1,
        //    UserName = "",
        //    Email = "",
        //    IsFirstTimeLogin = true,
        //    FailedPasswordAttemptCount = 0,
        //    IsApproved = true,
        //    IsLockedOut = true,
        //    LastLockedOutTimeStamp = "",
        //    LastLoginTimeStamp = "",
        //    LastPasswordChangedTimeStamp = "",
        //    Password = "",
        //    RegisteredDateTimeStamp = "",
        //    RoleId = 1,
        //    Salt = "",
        //    UserCode = ""
        //};

        #endregion


    }
}
