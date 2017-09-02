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
    public class CustomerPageNavigationParameter
    {
        public List<CustomerBillingProductViewModelBase> ProductsConsumed { get; set; }
        public TCustomer SelectedCustomer { get; set; }
        public CustomerBillingSummaryViewModelBase BillingSummaryViewModel { get; set; }
        public SelectPaymentModeViewModelBase SelectPaymentModeViewModelBase { get; set; }
    }
}
