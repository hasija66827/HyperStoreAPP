using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SDKTemplate
{
    class Utility
    {
        public static float RoundInt32(float f)
        {
            return (float)(Math.Round((decimal)f,2));
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
