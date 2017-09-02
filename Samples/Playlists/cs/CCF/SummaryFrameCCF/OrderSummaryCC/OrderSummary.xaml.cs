using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderSummaryCC : Page
    {
        public static OrderSummaryCC Current;
        public OrderSummaryViewModel OrderSummaryViewModel;
        public OrderSummaryCC()
        {
            Current = this;
            this.OrderSummaryViewModel = new OrderSummaryViewModel();
            this.InitializeComponent();
            CustomerOrderListCCF.Current.CustomerOrderListUpdatedEvent += Current_CustomerOrderListUpdatedEvent; ;
        }

        private void Current_CustomerOrderListUpdatedEvent(IEnumerable<CustomerOrderViewModel> customerOrders)
        {
            this.OrderSummaryViewModel.TotalBillAmount = customerOrders.Sum(co => co.PayAmount);
            this.OrderSummaryViewModel.TotalPayedAmount = customerOrders.Sum(co => co.PayingAmount);
            this.OrderSummaryViewModel.OnAllPropertyChanged();
        }
    }
}
