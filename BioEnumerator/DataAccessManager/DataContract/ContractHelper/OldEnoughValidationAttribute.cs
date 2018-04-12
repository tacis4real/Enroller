using System;
using System.ComponentModel.DataAnnotations;

namespace BioEnumerator.DataAccessManager.DataContract.ContractHelper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OldEnoughValidationAttribute : ValidationAttribute
    {
        public int LimitAge { get; set; }
        public OldEnoughValidationAttribute(int limitAge)
        {
            LimitAge = limitAge;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int val = (int)value;

            if (val >= LimitAge)
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessageString);
        }
    }
}
