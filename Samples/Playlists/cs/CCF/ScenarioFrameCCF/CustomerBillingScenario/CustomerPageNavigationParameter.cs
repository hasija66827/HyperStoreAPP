using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Models;
using SDKTemplate.Models;

namespace SDKTemplate
{
    public class CustomerPageNavigationParameter
    {
        public List<CustomerBillingProductViewModelBase> ProductsPurchased { get; set; }
        public Person SelectedCustomer { get; set; }
        public CustomerBillingSummaryViewModelBase BillingSummaryViewModel { get; set; }
        public CheckoutViewModel CheckoutViewModel { get; set; }
    }
    public enum OrderType
    {
        CustomerOrder,
        SupplierOrder
    }

    public class PageNavigationParameter
    {
        private OrderType _OrderType;
        public OrderType OrderType { get { return this._OrderType; } }
        private CustomerPageNavigationParameter _CustomerPageNavigationParameter;
        public CustomerPageNavigationParameter CustomerPageNavigationParameter { get { return this._CustomerPageNavigationParameter; } }
        private SupplierPageNavigationParameter _SupplierPageNavigationParameter;
        public SupplierPageNavigationParameter SupplierPageNavigationParameter { get { return this._SupplierPageNavigationParameter; } }
        public PageNavigationParameter(OrderType orderType, CustomerPageNavigationParameter CPNV, SupplierPageNavigationParameter SPNV) {
            _OrderType = orderType;
            _CustomerPageNavigationParameter = CPNV;
            _SupplierPageNavigationParameter = SPNV;
        }
    }
}
