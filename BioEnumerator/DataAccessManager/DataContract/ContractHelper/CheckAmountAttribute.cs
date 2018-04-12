using System;
using System.ComponentModel.DataAnnotations;

namespace BioEnumerator.DataAccessManager.DataContract.ContractHelper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CheckAmountAttribute : ValidationAttribute
    {
        private readonly float _compareValue;

        public CheckAmountAttribute(float compareValue)
        {
            _compareValue = compareValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is float)
            {
            if ((float) value < (double) _compareValue)
                return new ValidationResult(this.ErrorMessageString);
            return ValidationResult.Success;
            }
            if (!(value is double))
            return new ValidationResult(ErrorMessageString);
            if ((double) value < _compareValue)
            return new ValidationResult(ErrorMessageString);
            return ValidationResult.Success;
        }
    }
}
