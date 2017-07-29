using System.ComponentModel.DataAnnotations;

namespace Mvvm
{
    /// <summary>
    /// Sample custom validation for float values.
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

            float result;
            return float.TryParse(value.ToString(), out result);
        }
    }
}
