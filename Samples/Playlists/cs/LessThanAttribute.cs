using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class LessThanPropertyAttribute : ValidationAttribute
    {
        string propertyName;

        public LessThanPropertyAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext obj)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var propertyInfo = obj.ObjectInstance.GetType().GetProperty(this.propertyName);

            if (propertyInfo == null)
            {
                // Should actually throw an exception.
                return ValidationResult.Success;
            }

            dynamic otherValue = propertyInfo.GetValue(obj.ObjectInstance);

            if (otherValue == null)
            {
                return ValidationResult.Success;
            }

            dynamic compare = 0;
            try
            {
                compare = otherValue.CompareTo(System.Convert.ToDecimal(value));
            }
            catch (FormatException)
            {
                return new ValidationResult(String.Format("{0} is not numeric", this.propertyName));
            }

            if (compare < 0)
            {
                return new ValidationResult(String.Format("Value should be less than {0}({1})", this.propertyName, otherValue));
            }

            return ValidationResult.Success;
        }
    }
}
