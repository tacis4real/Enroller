using System;
using System.ComponentModel.DataAnnotations;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataAccessManager.DataContract.ContractHelper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CheckNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value as string;
            if (string.IsNullOrEmpty(str))
            return new ValidationResult(ErrorMessageString);
            if (!RegExValidation.IsNameValid(str))
            return new ValidationResult(ErrorMessageString);
            return ValidationResult.Success;
        }
    }
}
