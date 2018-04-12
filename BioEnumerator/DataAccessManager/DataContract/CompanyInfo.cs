using System.ComponentModel.DataAnnotations;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;

namespace BioEnumerator.DataAccessManager.DataContract
{
    public class CompanyInfo
    {
        public int CompanyInfoId { get; set; }

        [StringLength(100, ErrorMessage = @"Station name is too short or too long", MinimumLength = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"Station name is required")]
        public string StationName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"Station key is required")]
        [StringLength(100, ErrorMessage = @"Station key is too short or too long", MinimumLength = 3)]
        public string StationKey { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"Host Server Address is required")]
        [StringLength(500, ErrorMessage = @"Host Server Address  is too short or too long", MinimumLength = 3)]
        public string HostServerAddress { get; set; }

        //[StringLength(200, ErrorMessage = @"Organization address must be between 10 and 200 characters", MinimumLength = 10)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = @"Organization address is required")]
        public string Address { get; set; }

        //[CheckMobileNumber(ErrorMessage = @"Invalid Mobile Number")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = @"Mobile number is required")]
        //[StringLength(15, ErrorMessage = @"Mobile number is too short or too long", MinimumLength = 7)]
        //public string MobileNumber { get; set; }

        //[StringLength(50)]
        //public string PhoneNumbers { get; set; }

        public bool Status { get; set; }
    }
}
