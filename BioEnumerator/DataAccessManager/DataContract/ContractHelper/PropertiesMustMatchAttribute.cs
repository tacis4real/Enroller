using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BioEnumerator.DataAccessManager.DataContract.ContractHelper
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class PropertiesMustMatchAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "'{0}' and '{1}' do not match.";

        private readonly object _typeId = new object();

        public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
            : base(DefaultErrorMessage)
        {
            OriginalProperty = originalProperty;
            ConfirmProperty = confirmProperty;
        }

        public string ConfirmProperty
        {
            get;
            private set;
        }

        public string OriginalProperty
        {
            get;
            private set;
        }

        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                OriginalProperty, ConfirmProperty);
        }

        public override bool IsValid(object value)
        {
            if (value == null) { return false; }
            var properties = TypeDescriptor.GetProperties(value);
            var originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
            var confirmValue = properties.Find(ConfirmProperty, true /* ignoreCase */).GetValue(value);
            return Equals(originalValue, confirmValue);
        }
    }

}
