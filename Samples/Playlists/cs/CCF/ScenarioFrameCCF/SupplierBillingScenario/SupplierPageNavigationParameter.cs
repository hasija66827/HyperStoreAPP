﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class SupplierPageNavigationParameter
    {
        public List<SupplierBillingProductViewModelBase> ProductPurchased { get; set; }
        public TSupplier SelectedSupplier { get; set; }
        public SupplierBillingSummaryViewModelBase SupplierBillingSummaryViewModel { get; set; }
        public SupplierCheckoutViewModel SupplierCheckoutViewModel { get; set; }
        public SupplierPageNavigationParameter()
        { }
    }
}
