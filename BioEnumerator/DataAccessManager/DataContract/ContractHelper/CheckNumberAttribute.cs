using System;
using System.ComponentModel.DataAnnotations;

namespace BioEnumerator.DataAccessManager.DataContract.ContractHelper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CheckNumberAttribute : ValidationAttribute
    {
        private readonly int _compareValue;

        public CheckNumberAttribute(int compareValue)
        {
            _compareValue = compareValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int)
            {
                if ((int) value <= _compareValue)
                return new ValidationResult(ErrorMessageString);
                return ValidationResult.Success;
            }
            if (!(value is long))
            return new ValidationResult(ErrorMessageString);
            if ((long) value <= _compareValue)
            return new ValidationResult(ErrorMessageString);
            return ValidationResult.Success;
        }
    }
}
