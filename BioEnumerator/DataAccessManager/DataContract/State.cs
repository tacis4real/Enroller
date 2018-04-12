using System.ComponentModel.DataAnnotations.Schema;

namespace BioEnumerator.DataAccessManager.DataContract
{

    public class State
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateId { get; set; }
        public string Name { get; set; }

    }
}
