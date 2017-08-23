using System.ComponentModel.DataAnnotations;

namespace Mvvm
{
    /// <summary>
    /// Sample custom validation for decimal values.
    /// </summary>
    public class NumericAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // The [Required] attribute should test this.
            if (value == null)
            {
                return true;
            }

            decimal result;
            return decimal.TryParse(value.ToString(), out result);
        }
    }
}
