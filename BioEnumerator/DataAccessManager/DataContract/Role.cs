using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BioEnumerator.DataAccessManager.DataContract
{
    public class Role
    {
        public int RoleId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = @"* Required")]
        public string Name { get; set; }
        public bool Status { get; set; }


        public ICollection<User> Users { get; set; }
    }
}
