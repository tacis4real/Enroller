﻿using System;
using System.ComponentModel.DataAnnotations;
using XPLUG.WINDOWTOOLS;

namespace BioEnumerator.DataAccessManager.DataContract.ContractHelper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CheckMobileNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value as string;
            if (string.IsNullOrEmpty(str) || GSMHelper.ValidateMobileNumber(str))
            return ValidationResult.Success;
            return new ValidationResult(ErrorMessageString);
        }
    }
}
