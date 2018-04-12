using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BioEnumerator.DataAccessManager.DataContract
{
    public class UserLoginTrail
    {
        [ScaffoldColumn(false)]
        public int UserLoginTrailId { get; set; }

        [DisplayName(@"LoginSource")]
        [StringLength(20)]
        [Required(ErrorMessage = @"* Required")]
        public string LoginSource { get; set; }

        public bool IsSuccessful { get; set; }

        [StringLength(25)]
        [DisplayName(@"LoginTimeStamp")]
        public string LoginTimeStamp { get; set; }

        public int UserProfileId { get; set; }

        public virtual User User { get; set; }
    }
}
