using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Certificates;
using Windows.UI.Notifications;
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
            if (value == null)
                return null;
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

    public class FloatToInverseRupeeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return Utility.ConvertToRupeeWithInverseSign(value);
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class FloatToNegativeRupeeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return Utility.FloatToNegativeRupeeConverter(value);
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    // Append the decimal value with Rupee symbol.
    public class FloatToPositiveRupeeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return Utility.FloatToPositiveRupeeConverter(value);
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    // Append the decimal value with Rupee symbol.
    public class FloatToRupeeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return Utility.ConvertToRupee(value);
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    // Append the decimal value with Rupee symbol and (+ve or -ve sign).
    public class FloatToRupeeConverterWithSign : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return Utility.ConvertToRupeeWithSign(value);
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    // Append the decimal value with %Off
    public class FloatToPercentageOFFConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            if (value == null)
                return "x% Off";

            // Return the value to pass to the target.
            return value.ToString() + "% Off";
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    // Append the decimal value with %GST
    public class FloatToPercentageGSTConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            //TODO: Remove
            if (value == null)
                return "x% GST";

            // Return the value to pass to the target.
            return value.ToString() + "% GST";
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    // Append the decimal value with %GST
    public class FloatToPercentageProfitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            //TODO: Remove
            if (value == null)
                return "x% Pft";
            // Return the value to pass to the target.
            return Math.Round((System.Convert.ToDecimal(value)), 2) + "% Pft";
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
                var v = System.Convert.ToDecimal(value);
                if (v < 0)
                    throw new Exception();
            }
            catch (Exception e)
            {
                value = 1;
                MainPage.Current.NotifyUser("Invalid value entered, resetting it to one", NotifyType.ErrorMessage);
            }
            return System.Convert.ToDecimal(value);
        }
    }

    // Tries to convert value into positive decimal, if it fails then reset the value to one.
    public class CheckIfPositiveFloat : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            var formattedvalue = Math.Round((System.Convert.ToDecimal(value)), 2);
            return formattedvalue.ToString();
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            try
            {
                value = (System.Convert.ToDecimal(value));

            }
            catch (Exception e)
            {
                value = null;
            }
            return (value);
        }
    }



    // Tries to convert value into positive decimal, if it fails then reset the value to one.
    public class ConverToInt32 : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            try
            {
                value = (System.Convert.ToInt32(value));
            }
            catch (Exception e)
            {
                value = null;
            }
            return (value);
        }
    }

    // Tries to convert value into positive integer, if it fails then reset the value to one.
    public class CheckIfValidPercentage : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return value?.ToString();
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

            return System.Convert.ToDecimal(value);
        }
    }



    partial class Utility
    {
        public static string GetHardwareId()
        {
            var token = Windows.System.Profile.HardwareIdentification.GetPackageSpecificToken(null);
            var hardwareId = token.Id;
            var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

            byte[] bytes = new byte[hardwareId.Length];
            dataReader.ReadBytes(bytes);

            return BitConverter.ToString(bytes);
        }

        public static void CopyPropertiesTo<TS, TD>(TS source, TD dest)
        {
            var sourceProps = typeof(TS).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TD).GetProperties().Where(x => x.CanWrite).ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    p.SetValue(dest, sourceProp.GetValue(source, null), null);
                }
            }
        }
        public static decimal Rounddecimal(decimal f)
        {
            return (decimal)(Math.Round((decimal)f, 2));
        }

        public static string ConvertToRupee(object value)
        {
            try
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
            catch (Exception e)
            {
                return "";
            }
        }

        public static string FloatToNegativeRupeeConverter(object value)
        {
            // value is the data from the source object.
            var price = Math.Abs(Math.Round(System.Convert.ToDouble(value), 2));
            CultureInfo hindi = new CultureInfo("hi-IN");
            string text = string.Format(hindi, "{0:c}", price);
            // Return the value to pass to the target.
            return "-" + text;

        }
        public static string FloatToPositiveRupeeConverter(object value)
        {
            // value is the data from the source object.
            var price = Math.Abs(Math.Round(System.Convert.ToDouble(value), 2));
            CultureInfo hindi = new CultureInfo("hi-IN");
            string text = string.Format(hindi, "{0:c}", price);
            // Return the value to pass to the target.
            return "+" + text;


        }

        public static string ConvertToRupeeWithInverseSign(object value)
        {
            var v = System.Convert.ToDouble(value);
            if (v > 0)
                return Utility.FloatToNegativeRupeeConverter(value);
            else
                return Utility.FloatToPositiveRupeeConverter(value);
        }

        public static string ConvertToRupeeWithSign(object value)
        {
            var v = System.Convert.ToDouble(value);
            if (v < 0)
                return Utility.FloatToNegativeRupeeConverter(value);
            else
                return Utility.FloatToPositiveRupeeConverter(value);
        }


        public static string GetGlyphValue(String productName)
        {
            var productGlyph = "";
            var tokenizeProductName = productName.Split(' ');
            foreach (var a in tokenizeProductName)
            {
                productGlyph += a.ElementAtOrDefault(0).ToString();
            }
            string glyph = productGlyph.ToUpper();
            if (glyph.Length > 2)
                glyph = glyph.Substring(0, 2);
            return glyph;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns>returns decimal is object can be converted, o.w. 0</returns>
        public static decimal? TryToConvertToDecimal(object value)
        {
            if (value == null)
                return null;
            decimal? result = null;
            try
            {

                result = Convert.ToDecimal(value);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

    }
}
