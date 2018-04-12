using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioEnumerator.CommonUtils;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;
using BioEnumerator.Properties;
using DPFP;

namespace BioEnumerator.APIServer.APIObjs
{


    public class BiometricInfo
    {
        public string RightThumbPrintImage { get; set; }
        public string RightIndexPrintImage { get; set; }
        public byte[] RightThumbPrintTemplate { get; set; }
        public byte[] RightIndexPrintTemplate { get; set; }
        public Template RightThumbPrintTemplateFile { get; set; }
        public Template RightIndexPrintTemplateFile { get; set; }
    }
    public class BeneficiaryRegObj
    {
        public long BeneficiaryId { get; set; }
        public int RecordId { get; set; }
        public int ClientStationId { get; set; }

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

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resources), ErrorMessage = @"Mobile_Number_is_required")]
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
        

        #region Biometrics

        public FingerTemplateData FinFingerTemplateData { get; set; }
        public List<byte[]> FingerPrintTemplate { get; set; }
        public Template[] CapturedFingerPrintTemplate { get; set; }

        

        public byte[] FingerTemplate { get; set; }
        public byte[] RightThumbPrintTemplate { get; set; }
        public byte[] RightIndexPrintTemplate { get; set; }
        //public string ThumbFileName { get; set; }
        //public string IndexFileName { get; set; }

        public Image Image { get; set; }
        public string ImagePath { get; set; }
        public string ImageFileName { get; set; }

        #endregion

    }

    public class BeneficiaryRegMinObj
    {
        public long BeneficiaryId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Surname_is_required", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Surname_is_too_short_or_too_long")]
        public string Surname { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Firstname_is_required", AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"First_name_is_too_short_or_too_long")]
        public string FirstName { get; set; }
        public string Othernames { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Date_of_Birth_is_required", AllowEmptyStrings = false)]
        [StringLength(10, MinimumLength = 10, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Invalid_Date_of_Birth")]
        public string DateOfBirth { get; set; }
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

        //[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Occupation_is_required")]
        //[CheckNumber(0, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Invalid_Occupation_Information")]
        public int OccupationId { get; set; }
        public int Status { get; set; }

        #region Biometrics

        public List<byte[]> FingerPrintTemplate { get; set; }
        public Image Image { get; set; }
        public byte[] ImageByteArray { get; set; }
        public string ImageByteString { get; set; }
        public string ImagePath { get; set; }
        public string ImageFileName { get; set; }

        #endregion

    }

    public class BulkBeneficiaryRegObj
    {
        public int LocalAreaId { get; set; }
        public List<BeneficiaryRegMinObj> BeneficiaryRegObjs { get; set; }
    }



    public class PreviewBeneficiaryRegObj
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Othernames { get; set; }
        public string DateOfBirth { get; set; }
        public string MobileNumber { get; set; }
        public string ResidentialAddress { get; set; }
        public string OfficeAddress { get; set; }
        public string State { get; set; }
        public string LocalArea { get; set; }
        public string Sex { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }

    }


}
