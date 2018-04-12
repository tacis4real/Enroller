using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BioEnumerator.DataAccessManager.DataContract.ContractHelper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CheckAttribute : ValidationAttribute
    {
        private object[] ValidValues;

        public CheckAttribute(params object[] validValues)
        {
            ValidValues = validValues;
        }

        public override bool IsValid(object value)
        {
            return (ValidValues).FirstOrDefault((v => v.Equals(value))) != null;
        }
    }
}
