using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace SDKTemplate
{
    public class ConvertDateTimeOffsetToDateTime : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return new DateTimeOffset((DateTime)value);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            return ((DateTimeOffset)value).Date;
        }
    }

    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Should never be reached.
            throw new Exception("Binding of the payltrRadBtn.ischecked is done one way to the enable property of the use walllet.");
        }
    }

    // Append the float value with Rupee symbol.
    public class FloatToInverseRupeeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            // value is the data from the source object.
            float price =(float)value;
            if (price < 0)
                return "+" + Math.Abs(price).ToString() + "\u20B9";
            else
                return "-" + Math.Abs(price).ToString() + "\u20B9";
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    // Append the float value with Rupee symbol.
    public class FloatToRupeeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            // value is the data from the source object.
            float price = System.Convert.ToSingle(value);
            // Return the value to pass to the target.
            return price.ToString() + "\u20B9";
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    // Append the float value with Rupee symbol and (+ve or -ve sign).
    public class FloatToRupeeConverterWithSign : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            // value is the data from the source object.
            float price = System.Convert.ToSingle(value);
            // Return the value to pass to the target.
            string sign="";
            
           if (price > 0)
                sign = "+";
                
            return sign+price.ToString() + "\u20B9";
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    // Append the float value with %Off
    public class FloatToPercentageOFFConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            // value is the data from the source object.
            float price = (float)value;
            // Return the value to pass to the target.
            return price.ToString() + "% Off";
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    // Tries to convert value into positive integer, if it fails then reset the value to one.
    public class CheckIfPositiveInteger : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            try
            {
                var v = System.Convert.ToInt32(value);
                if (v < 0)
                    throw new Exception();
            }
            catch (Exception e)
            {
                value = 1;
                MainPage.Current.NotifyUser("Invalid value entered, resetting it to one", NotifyType.ErrorMessage);
            }
            return System.Convert.ToInt32(value);
        }
    }

    // Tries to convert value into positive float, if it fails then reset the value to one.
    public class CheckIfPositiveFloat : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            try
            {
                var v = (float)(System.Convert.ToDouble(value));
                if (v < 0)
                    throw new Exception();
            }
            catch (Exception e)
            {

                value = 0;
                MainPage.Current.NotifyUser("Invalid value entered, resetting it to zero", NotifyType.ErrorMessage);
            }
            return (float)System.Convert.ToDouble(value);
        }
    }
    
    // Tries to convert value into positive integer, if it fails then reset the value to one.
    public class CheckIfValidPercentage : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            try
            {
                double v = System.Convert.ToDouble(value);
                if (v < 0 || v > 100)
                    throw new Exception();
            }
            catch (Exception e)
            {
                value = 0;
                MainPage.Current.NotifyUser("Percentage should be between 0 to 100, resetting it to zero", NotifyType.ErrorMessage);
            }

            return System.Convert.ToSingle(value);
        }
    }

    class Utility
    {
        public static string DEFAULT_CUSTOMER_GUID { get { return "cccccccc-cccc-cccc-cccc-cccccccccccc"; } }

        public static float RoundInt32(float f)
        {
            return (float)(Math.Round((decimal)f, 2));
        }

        public static bool CheckIfStringIsNumber(string str)
        {
            // TODO: Apply more checks
            if (str == null || str == "")
                return false;
            return true;
        }

        public static bool IsMobileNumber(string text)
        {
            if (text.Length == 10)
            {
                try
                {
                    Convert.ToInt64(text);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;
        }

        public static string GetGlyphValue(String productName)
        {
            var productGlyph = "";
            var tokenizeProductName = productName.Split(' ');
            foreach (var a in tokenizeProductName)
            {
                productGlyph += a.ElementAtOrDefault(0).ToString();
            }
            return productGlyph.ToUpper();
        }

        public static bool CheckIfUniqueProductName(string productName)
        {
            if (ProductDataSource.IsProductNameExist(productName))
            {
                MainPage.Current.NotifyUser("The product name already exist", NotifyType.ErrorMessage);
                return false;
            }
            return true;
        }

        public static bool CheckIfValidMobileNumber(string mobileNo, Person person)
        {
            if (IsMobileNumber(mobileNo) == false)
            {
                MainPage.Current.NotifyUser("Mobile number is not valid", NotifyType.ErrorMessage);
                return false;
            }
            if (person == Person.Customer)
            {
                if (CustomerDataSource.IsMobileNumberExist(mobileNo))
                {
                    MainPage.Current.NotifyUser("Mobile Number already exist", NotifyType.ErrorMessage);
                    return false;
                }
            }
            else if (person == Person.WholeSeller)
            {
                if (WholeSellerDataSource.IsMobileNumberExist(mobileNo))
                {
                    MainPage.Current.NotifyUser("Mobile Number already exist", NotifyType.ErrorMessage);
                    return false;
                }
            }
            return true;
        }

        public static bool CheckIfValidName(string name, Person person)
        {
            if (name == "")
            {
                MainPage.Current.NotifyUser("Name cannot be empty", NotifyType.ErrorMessage);
                return false;
            }
            if (person == Person.Customer)
            {
                if (CustomerDataSource.IsNameExist(name))
                {
                    MainPage.Current.NotifyUser("Name already exist", NotifyType.ErrorMessage);
                    return false;
                }
            }
            else if (person == Person.WholeSeller)
            {
                if (WholeSellerDataSource.IsNameExist(name))
                {
                    MainPage.Current.NotifyUser("Name already exist", NotifyType.ErrorMessage);
                    return false;
                }
            }
            return true;
        }

        public static bool CheckIfUniqueProductCode(string productCode)
        {
            if (ProductDataSource.IsProductCodeExist(productCode))
            {
                MainPage.Current.NotifyUser("The product code already exist", NotifyType.ErrorMessage);
                return false;
            }
            return true;
        }

        public static bool CheckIfValidProductCode(string productCode)
        {
            if (productCode == "")
            {
                MainPage.Current.NotifyUser("The Product code cannot be empty", NotifyType.ErrorMessage);
                return false;
            }
            try
            {
                //TODO: negative number should also be not allowed.
                System.Convert.ToInt16(productCode);
            }
            catch
            {
                MainPage.Current.NotifyUser("The Product should contains digits only", NotifyType.ErrorMessage);
                return false;
            }
            if (productCode.Length != 4)
            {
                MainPage.Current.NotifyUser("The Product code should be of 4 digits", NotifyType.ErrorMessage);
                return false;
            }

     
            return true;
        }

        public static string GenerateRandom(int? length=null)
        {
            if (length == null)
                length = 7;
            var random = new Random();
            string s = string.Empty;
            s = String.Concat(s, random.Next(1,10).ToString());
            for (int i = 1; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}
