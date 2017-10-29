﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class ProductASBViewModel : ProductViewModelBase
    {
        // Property is used by ASB(AutoSuggestBox) for display member path and text member path property
        public string Product_Id_Name { get { return string.Format("{0} ({1})", Code, Name); } }
  

        //Constructor to convert parent obect to child object.
        public ProductASBViewModel(Product parent)
        {
            foreach (PropertyInfo prop in typeof(Product).GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
