using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Models;

namespace SDKTemplate
{
    public class PageNavigationParameter
    {
        public List<CustomerOrderProductViewModelBase> ProductsConsumed { get; set; }
        public TCustomer SelectedCustomer { get; set; }
        public BillingSummaryViewModel BillingSummaryViewModel { get; set; }
        public SelectPaymentModeViewModelBase SelectPaymentModeViewModelBase { get; set; }
    }
}
