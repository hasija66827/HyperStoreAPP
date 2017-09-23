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
                return new ValidationResult("Try value in numeric range.");
            }

            if (compare < 0)
            {
                var errorMessage = String.Format("Value should be less than {0}.", Utility.ConvertToRupee(otherValue));
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
