using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class ProductASBViewModel : TProduct
    {
        // Property is used by ASB(AutoSuggestBox) for display member path and text member path property
        public string Product_Id_Name { get { return string.Format("{0} ({1})", Code, Name); } }
        public string FormattedNameQuantity
        {
            get { return this.Name + " (" + this.TotalQuantity + ")"; }
        }

        //Constructor to convert parent obect to child object.
        public ProductASBViewModel(TProduct parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
