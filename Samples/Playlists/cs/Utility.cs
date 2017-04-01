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
    public delegate void QuantityPropertyChangedDelegate();
    public class FloatToRupeeConverter : IValueConverter
    {
        // Define the Convert method to convert a float to
        // a month string.
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            // value is the data from the source object.
            float price = (float)value;
            // Return the value to pass to the target.
            return "\u20B9"+price.ToString();
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    public class FloatToPercentageOFFConverter : IValueConverter
    {
        // Define the Convert method to convert a float to
        // a month string.
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            // value is the data from the source object.
            float price = (float)value;
            // Return the value to pass to the target.
            return price.ToString()+"%OFF";
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    public class CheckIfInteger : IValueConverter
    {
        // Define the Convert method to convert a float to
        // a month string.
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            return value.ToString();
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            try
            {
                var v=System.Convert.ToInt32(value);
                if (v < 0)
                    throw new Exception();
            }
            catch (Exception e)
            {
                value = 1;
                //TODO: Notify that quantity has been set to one
            }
            return System.Convert.ToInt32(value);
        }
    }
    class Utility
    {
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
        /// <summary>
        /// Retuns the list of child controls of parent control in visual tree
        /// </summary>
        /// <param name="parent">parent control handler in visual tree</param>
        /// <returns></returns>
        public static List<Control> AllChildren(DependencyObject parent)
        {
            var _List = new List<Control>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var _Child = VisualTreeHelper.GetChild(parent, i);
                if (_Child is Control)
                    _List.Add(_Child as Control);
                _List.AddRange(AllChildren(_Child));
            }
            return _List;
        }
    }
}
