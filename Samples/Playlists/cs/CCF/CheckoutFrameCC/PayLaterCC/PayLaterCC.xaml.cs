using SDKTemp.Data;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class PayLaterCC : Page
    {
        private CheckoutViewModel _CV { get; set; }
        private PageNavigationParameter PageNavigationParameter { get; set; }
        public PayLaterCC()
        {
            this.InitializeComponent();
            PlaceOrderBtn.Click += _PlaceOrderBtn_Click;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _CV = DataContext as CheckoutViewModel;
            _CV.ErrorsChanged += _SCV_ErrorsChanged;
            _CV.PayingAmount = null;
            _CV.DueDate = DateTime.Now.AddDays(45);
            _CV.IntrestRate = null;

            this.PageNavigationParameter = (PageNavigationParameter)e.Parameter;
            if (this.PageNavigationParameter.OrderType == OrderType.SupplierOrder)
                _CV.AmountToBePaid = this.PageNavigationParameter.SupplierPageNavigationParameter.SupplierBillingSummaryViewModel.BillAmount;
            else
                _CV.AmountToBePaid = this.PageNavigationParameter.CustomerPageNavigationParameter.BillingSummaryViewModel.BillAmount;
        }

        private void _SCV_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {

        }

        private async void _PlaceOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = _CV.ValidateProperties();
            if (IsValid)
            {
                if (this.PageNavigationParameter.OrderType == OrderType.SupplierOrder)
                {
                    this.PageNavigationParameter.SupplierPageNavigationParameter.SupplierCheckoutViewModel = _CV;
                    var IsCreated = await SupplierOrderDataSource.InitiateSupplierOrderCreationAsync(this.PageNavigationParameter.SupplierPageNavigationParameter);
                    MainPage.RefreshPage(ScenarioType.SupplierBilling);
                }
                else
                {
                    this.PageNavigationParameter.CustomerPageNavigationParameter.CustomerCheckoutViewModel = _CV;
                    var IsCreated = await SupplierOrderDataSource.InitiateCustomerOrderCreationAsync(this.PageNavigationParameter.CustomerPageNavigationParameter);
                    MainPage.RefreshPage(ScenarioType.CustomerBilling);
                }
            }
        }
    }
}
