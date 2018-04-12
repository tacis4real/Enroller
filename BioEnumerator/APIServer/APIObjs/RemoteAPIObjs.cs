using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;

namespace BioEnumerator.APIServer.APIObjs
{
    public class AuthAdminUser
    {
        public string Username;
        public string Password;
    }

    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "Password and Confirm Password must match.")]
    public class UserRegistrationObj
    {
        public long UserId { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Surname is required", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Surname must be between 2 and 100 characters")]
        public string Surname { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = "Othernames are required", AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Othernames must be between 2 and 100 characters")]
        public string Othernames { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "Mobile Number must be between 7 and 15 digits")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile Number is required")]
        //[CheckMobileNumber(ErrorMessage = "Invalid Mobile Number")]
        public string MobileNumber { get; set; }

        [Range(1, 2)]
        public int Sex { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(20, ErrorMessage = "Username cannot be longer than 20 character length")]
        public string Username { get; set; }

        [Required]
        [StringLength(150)]
        [DataType(DataType.Password)]
        [ValidatePasswordLength(ErrorMessage = "Password length must be at least 8 character long")]
        public string Password { get; set; }

        [Required]
        [StringLength(150)]
        [ValidatePasswordLength(ErrorMessage = "Password length must be at least 8 character long")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public int RegisteredByUserId { get; set; }

        [StringLength(50)]
        public string DeviceSerialNumber { get; set; }

        [StringLength(50)]
        public string DeviceName { get; set; }

        public string SelectedRoles { get; set; }
        public int[] MyRoleIds { get; set; }

        public string[] MyRoles { get; set; }
        public bool IsActive { get; set; }
    }


    public class UploadStationInfo
    {

        public int StationInfoId;
        public string EnrollerRegId;
        public long RemoteStationId;
        public string StationName;
        public string StationKey;
        public string HostServerAddress;
        public string APIAccessKey;
        public string TimeStampRegistered;
        public bool Status;

    }


}
