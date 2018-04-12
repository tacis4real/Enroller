using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.DataAccessManager.DataContract;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;
using BioEnumerator.RemoteHelper.MessengerService;
using DPFP;

namespace BioEnumerator.APIServer.APIObjs
{

    #region For Testing on ICAS
    public class UserRegResponse
    {
        public long UserId { get; set; }
        public string MobileNumber { get; set; }
        public string Surname { get; set; }
        public string Othernames { get; set; }
        public string Email { get; set; }

        public RespStatus Status;
    }
    
    public class RespStatus
    {
        public bool IsSuccessful;
        public RespMessage Message;
    }


    public class RespMessage
    {
        public string FriendlyMessage;
        public string TechnicalMessage;
        public string ErrorCode;
    }
    
    #endregion


    
    public class RemoteUserInfo
    {
        public StaffUser UserInfo;
        public UserProfile UserProfileInfo;
    }

    public class RemoteUserInformation
    {
        public List<StaffUserInfo> UserInfos;
        public List<UserProfile> UserProfileInfos;
        public List<ClientStation> ClientStationInfos;
    }

    public class StationRespObj
    {
        //public string APIAccessKey;
        //public string DeviceId;
        //public string StationKey;
        //public int ClientStationId;
        //public ResponseStatus ResponseStatus;

        public string APIAccessKey;
        public string DeviceId;
        public string StationId;
        public string StationName;
        //public string StationAddress;
        public string ResidentialAddress;
        public long ClientStationId;
        public long EnrollerId; // OperatorRemoteId
        //public long OrganizationId;
        public string EnrollerRegId; // OperatorRemoteRegistrationId
        public string Surname;
        public string FirstName;
        public string Othernames;
        public int Sex;
        public string UserName;
        public string MobileNumber;
        public string Email;
        public int EnrollerStatus;
        public int StationStatus;

        public ResponseStatus ResponseStatus;


    }




    public class ResponseStatus
    {
        public bool IsSuccessful;
        public ResponseMessage Message;
        public long ReturnedId;
        public string ReturnedValue;
    }

    public class ResponseMessage
    {
        public string FriendlyMessage;
        public string TechnicalMessage;
        public string MessageId;
    }


    public class BulkUserDetailItemObj
    {
        public List<UserDetailObj> UserDetailObjs;
        public ResponseStatus MainStatus;
    }


    public class UserDetailObj
    {
        public User User { get; set; }
        public UserProfile UserProfile { get; set; }
        public CompanyInfo CompanyInfo { get; set; }

    }

    public class RegisteredUserReportObj
    {
        public long UserId { get; set; }
        public string Othernames { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Sex { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsApproved { get; set; }
        public int SexId { get; set; }
        public string SelectedRoles { get; set; }
        public string[] MyRoles { get; set; }
        public int[] MyRoleIds { get; set; }
        public string PasswordChangeTimeStamp { get; set; }
        public string LastLoginTimeStamp { get; set; }
        public string LastLockedOutTimeStamp { get; set; }
        public int FailedPasswordCount { get; set; }
        public string TimeStampRegistered { get; set; }
        public bool IsPasswordChangeRequired { get; set; }

    }

   

    public class StationUserObj
    {

        public int UserProfileId { get; set; }
        public string ProfileNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int Sex { get; set; }
        public string SexName
        {
            get
            {
                if (Sex < 1)
                    return "";
                return Sex != 1 ? "Female" : "Male";
            }
        }

        public string DateOfBirth { get; set; }
        public string ResidentialAddress { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string DateLastModified { get; set; }
        public string TimeLastModified { get; set; }
        public int ModifiedBy { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsFirstTimeLogin { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public string LastLockedOutTimeStamp { get; set; }
        public string LastLoginTimeStamp { get; set; }
        public string LastPasswordChangedTimeStamp { get; set; }
        public string RegisteredDateTimeStamp { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }


    public class BeneficiaryObj
    {
        public bool IsSelected { get; set; }
        public long BeneficiaryId { get; set; }
        public long BeneficiaryBiometricId { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othernames { get; set; }
        public string DateOfBirth { get; set; }
        public string MobileNumber { get; set; }
        public string ResidentialAddress { get; set; }
        public string OfficeAddress { get; set; }
        public int OccupationId { get; set; }
        public string OccupationLabel { get; set; }
        public int MaritalStatus { get; set; }
        public int StateId { get; set; }
        public int LocalAreaId { get; set; }
        public string StateLabel { get; set; }
        public string LocalAreaLabel { get; set; }
        public string MaritalStatusLabel
        {
            get
            {
                if (MaritalStatus < 1)
                {
                    return "";
                }
                var name = Enum.GetName(typeof(MaritalStatus), MaritalStatus);
                if (name != null)
                {
                    return name.Replace("_", " ");
                }
                return "";
            }
        }
        public int Status { get; set; }
        public string StatusLabel
        {
            get
            {
                if (Status < 1)
                {
                    return "";
                }
                var name = Enum.GetName(typeof(RegStatus), Status);
                if (name != null)
                {
                    return name.Replace("_", " ");
                }
                return "";
            }
        }
        public string TimeStampUploaded { get; set; }
        public int UploadStatus { get; set; }
        public string UploadStatusLabel
        {
            get
            {
                //if (UploadStatus < 1)
                //{
                //    return "";
                //}
                var name = Enum.GetName(typeof(UploadStatus), UploadStatus);
                if (name != null)
                {
                    return name.Replace("_", " ");
                }
                return "";
            }
        }
        public string UploadMessage { get; set; }

        public int Sex { get; set; }
        public string SexLabel
        {
            get
            {
                if (Sex < 1)
                {
                    return "";
                }
                return Sex == 1 ? "Male" : Sex == 2 ? "Female" : "Unknown";
            }
        }
        public string TimestampRegistered { get; set; }
        public string ImageFileName { get; set; }
        public string ImagePath { get; set; }
        public byte[] ImageByteArray { get; set; }
        public string ImageByteString { get; set; }
        public Image Image { get; set; }
        public Bitmap BitmapImage { get; set; }
        public List<byte[]> FingerPrintTemplates { get; set; }

        public Template[] FingerTemplates = new Template[10];


    }

    public class BeneficiaryRegResponseObj
    {
        public long BeneficiaryRemoteId;
        public long BeneficiaryId;
        public string MobileNumber;
        public int RecordId;
        public ResponseStatus Status;
    }

    public class BulkBeneficiaryRegResponseObj
    {
        public List<BeneficiaryRegResponseObj> BeneficiaryRegResponses;
        public ResponseStatus MainStatus;
    }


    public class BeneficiaryInfoObj
    {
        public bool IsSelected { get; set; }
        public long BeneficiaryId { get; set; }
        public long BeneficiaryBiometricId { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othernames { get; set; }
        public string DateOfBirth { get; set; }
        public string MobileNumber { get; set; }
        public string ResidentialAddress { get; set; }
        public string OfficeAddress { get; set; }
        public int OccupationId { get; set; }
        public string OccupationLabel { get; set; }
        public int MaritalStatus { get; set; }
        public string MaritalStatusLabel
        {
            get
            {
                if (MaritalStatus < 1)
                {
                    return "";
                }
                var name = Enum.GetName(typeof(MaritalStatus), MaritalStatus);
                if (name != null)
                {
                    return name.Replace("_", " ");
                }
                return "";
            }
        }
        public int Status { get; set; }
        public string StatusLabel
        {
            get
            {
                if (Status < 1)
                {
                    return "";
                }
                var name = Enum.GetName(typeof(RegStatus), Status);
                if (name != null)
                {
                    return name.Replace("_", " ");
                }
                return "";
            }
        }
        public string TimeStampUploaded { get; set; }
        public int UploadStatus { get; set; }
        public string UploadStatusLabel
        {
            get
            {
                if (UploadStatus < 1)
                {
                    return "";
                }
                var name = Enum.GetName(typeof(UploadStatus), UploadStatus);
                if (name != null)
                {
                    return name.Replace("_", " ");
                }
                return "";
            }
        }
        public string UploadMessage { get; set; }

        public int Sex { get; set; }
        public string SexLabel
        {
            get
            {
                if (Sex < 1)
                {
                    return "";
                }
                return Sex == 1 ? "Male" : Sex == 2 ? "Female" : "Unknown";
            }
        }
        public string TimestampRegistered { get; set; }

        public string ImageFileName { get; set; }
        public string ImagePath { get; set; }
        public List<byte[]> FingerPrintTemplates { get; set; }

        public Template[] FingerTemplates = new Template[10];


    }


}
