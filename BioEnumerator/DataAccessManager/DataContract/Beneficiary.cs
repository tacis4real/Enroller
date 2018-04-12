using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;
using BioEnumerator.Properties;

namespace BioEnumerator.DataAccessManager.DataContract
{
    public class Beneficiary
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long BeneficiaryId { get; set; }
        public long BeneficiaryRemoteId { get; set; }
        public int RecordId { get; set; }

        //public int ClientStationId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Surname_is_required", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Surname_is_too_short_or_too_long")]
        public string Surname { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Firstname_is_required", AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"First_name_is_too_short_or_too_long")]
        public string FirstName { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessage = @"Othername_is_required", AllowEmptyStrings = false)]
        //[StringLength(200, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources), ErrorMessage = @"Other_name_is_too_short_or_too_long")]
        public string Othernames { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Date_of_Birth_is_required", AllowEmptyStrings = false)]
        [StringLength(10, MinimumLength = 10, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Invalid_Date_of_Birth")]
        public string DateOfBirth { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Mobile_Number_is_required")]
        //[StringLength(15, MinimumLength = 7, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Mobile_Number_must_be_between_7_and_15_characters")]
        //[CheckMobileNumber(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Invalid_Mobile_Number")]
        public string MobileNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Residential_Address_is_required")]
        [StringLength(200)]
        public string ResidentialAddress { get; set; }

        [StringLength(200)]
        public string OfficeAddress { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"State_is_required")]
        [CheckNumber(0, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Invalid_State_Information")]
        public int StateId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Local_Government_Area_is_required")]
        [CheckNumber(0, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Invalid_Local_Government_Area_Information")]
        public int LocalAreaId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Sex_Info_is_required", AllowEmptyStrings = false)]
        [CheckNumber(0, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Invalid_Sex")]
        public int Sex { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Marital_Status_is_required")]
        [CheckNumber(0, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Invalid_Marital_Status_Information")]
        public int MaritalStatus { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Occupation_is_required")]
        [CheckNumber(0, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Invalid_Occupation_Information")]
        public int OccupationId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Time_registered_is_required", AllowEmptyStrings = false)]
        [StringLength(35)]
        public string TimeStampRegistered { get; set; }
        public RegStatus Status { get; set; }
        public string TimeStampUploaded { get; set; }
        public UploadStatus UploadStatus { get; set; }
        public string UploadMessage { get; set; }



        #region Navigation Properties
        //public virtual ClientStation ClientStation { get; set; }
        //public virtual BeneficiaryBiometric BeneficiaryBiometric { get; set; }
        #endregion


    }
}
