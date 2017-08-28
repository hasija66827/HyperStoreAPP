﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class SupplierASBViewModel : TSupplier
    {
        public string Supplier_MobileNo_Name
        {
            get { return string.Format("{0}({1})", MobileNo, Name); }
        }
        public SupplierASBViewModel(TSupplier parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }

}
