using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace SDKTemplate
{
    public class PersonViewModelBase : Person
    {
        public string PersonNameGlyph
        {
            get { return Utility.GetGlyphValue(this.Name); }
        }

        public SolidColorBrush PersonGlyphColor
        {
            get
            {
                return new SolidColorBrush(Utility.GetGlyphColors(this.PersonId.ToString()));
            }
        }
    }

    public class PersonViewModel : PersonViewModelBase
    {
        public PersonViewModel(Person parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
