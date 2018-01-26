using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace SDKTemplate
{
    class PersonASBViewModel : PersonViewModelBase
    {
        public string Person_MobileNo_Name
        {
            get { return string.Format("{0} ({1})", MobileNo, Name); }
        }
        public PersonASBViewModel(Person parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
