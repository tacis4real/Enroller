

namespace BioEnumerator.DataAccessManager.DataContract
{

    public class StaffUserInfo
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

}
