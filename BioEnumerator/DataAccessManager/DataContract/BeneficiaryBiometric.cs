using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BioEnumerator.CommonUtils;
using Newtonsoft.Json;

namespace BioEnumerator.DataAccessManager.DataContract
{
    public class BeneficiaryBiometric
    {
        public long BeneficiaryBiometricId { get; set; }
        public long BeneficiaryId { get; set; }
        internal string _Template { get; set; }

        [NotMapped]
        public FingerTemplateData FingerTemplate
        {
            get { return _Template == null ? null : JsonConvert.DeserializeObject<FingerTemplateData>(_Template); }
            set { _Template = JsonConvert.SerializeObject(value); }
        }

        public string ImageFileName { get; set; }
        public string ImagePath { get; set; }


        #region Navigation Properties
        public virtual Beneficiary Beneficiary { get; set; }
        #endregion


    }
}
