using System.ComponentModel.DataAnnotations;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;

namespace BioEnumerator.DataAccessManager.DataContract
{
    public class StationInfo
    {
        public int StationInfoId { get; set; }
        public long RemoteStationId { get; set; }

        [StringLength(100, ErrorMessage = @"Invalid Station Name", MinimumLength = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"Station Name is required")]
        public string StationName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"Station Key is required")]
        //[StringLength(15, ErrorMessage = @"Invalid Station Key", MinimumLength = 15)]
        public string StationKey { get; set; }

        [StringLength(500, ErrorMessage = @"Invalid Host Server Address", MinimumLength = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"Host Server Address is required")]
        public string HostServerAddress { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = @"Station Address is required")]
        //[StringLength(200, ErrorMessage = @"Station Address is too short or too long", MinimumLength = 5)]
        //public string Address { get; set; }

        //[StringLength(15, ErrorMessage = @"Station Mobile number must be between 7 and 15 digits", MinimumLength = 7)]
        ////[CheckMobileNumber(ErrorMessage = @"Invalid Mobile Number")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = @"Station Mobile Number is required")]
        //public string MobileNumber { get; set; }

        [StringLength(10)]
        public string APIAccessKey { get; set; }

        //[StringLength(50)]
        //public string PhoneNumbers { get; set; }

        [Required(ErrorMessage = @"Invalid System Date")]
        [StringLength(35, ErrorMessage = @" Invalid System Date", MinimumLength = 10)]
        public string TimeStampRegistered { get; set; }

        public bool Status { get; set; }

    }
}
