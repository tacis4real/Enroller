using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BioEnumerator.DataAccessManager.DataContract.ContractHelper;
using BioEnumerator.Properties;

namespace BioEnumerator.DataAccessManager.DataContract
{
    public class LocalArea
    {

        public LocalArea()
        {
            Beneficiaries = new HashSet<Beneficiary>();
        }


        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LocalAreaId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "State_Information_is_required")]
        [CheckNumber(0, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Invalid_State_Information")]
        public int StateId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }


        public ICollection<Beneficiary> Beneficiaries { get; set; }
        public virtual State State { get; set; }
    }
}
