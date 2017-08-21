using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Certificates;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

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
            return Utility.FloatToInverseRupeeConverter(value);
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
            return Utility.FloatToRupeeConverter(value);
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
            return Utility.FloatToRupeeConverter(value);
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
    // Append the float value with %Off
    public class FloatToPercentageGSTConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            // value is the data from the source object.
            float price = (float)value;
            // Return the value to pass to the target.
            return price.ToString() + "% GST";
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

        public static async Task<HttpResponseMessage> HttpGet(string actionURI)
        {
            string absoluteURI = "https://localhost:44346/api/";
            string uri = string.Concat(absoluteURI, actionURI);

            HttpBaseProtocolFilter httpBaseProtocolFilter = new HttpBaseProtocolFilter();
            httpBaseProtocolFilter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Untrusted);
            HttpClient httpClient = new HttpClient(httpBaseProtocolFilter);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(uri));
            try
            {
                HttpResponseMessage response = await httpClient.SendRequestAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                var x = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                throw new Exception(x);
            }
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

        public static string FloatToRupeeConverter(object value)
        {
            // value is the data from the source object.
            var price = Math.Round(System.Convert.ToDouble(value), 2);
            var absPrice = Math.Abs(price);
            CultureInfo hindi = new CultureInfo("hi-IN");
            string text = string.Format(hindi, "{0:c}", absPrice);
            // Return the value to pass to the target.
            if (price >= 0)
                return text;
            return "-" + text; 
        }

        public static string FloatToInverseRupeeConverter(object value)
        {
            // value is the data from the source object.
            var price = Math.Round(System.Convert.ToDouble(value), 2);
            price = -price;
            CultureInfo hindi = new CultureInfo("hi-IN");
            string text = string.Format(hindi, "{0:c}", price);
            // Return the value to pass to the target.
            return text;

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

        public static bool CheckIfUniqueMobileNumber(string mobileNo, Person person)
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
        public static string GenerateCustomerOrderNo(int? length = null)
        {
            if (length == null)
                length = 7;
            var random = new Random();
            string s = "CORD";
            s = String.Concat(s, random.Next(1, 10).ToString());
            for (int i = 1; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        public static string GenerateWholeSellerTransactionNo(int? length = null)
        {
            if (length == null)
                length = 7;
            var random = new Random();
            string s = "STXN";
            s = String.Concat(s, random.Next(1, 10).ToString());
            for (int i = 1; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        public static string GenerateWholeSellerOrderNo(int? length=null)
        {
            if (length == null)
                length = 7;
            var random = new Random();
            string s = "SORD";
            s = String.Concat(s, random.Next(1,10).ToString());
            for (int i = 1; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns>returns float is object can be converted, o.w. 0</returns>
        public static float TryToConvertToFloat(object value)
        {
            float result = 0;
            try
            {
                result = Convert.ToSingle(value);
            }
            catch
            {
                result = 0;
            }
            return result;
        }
    }
}
