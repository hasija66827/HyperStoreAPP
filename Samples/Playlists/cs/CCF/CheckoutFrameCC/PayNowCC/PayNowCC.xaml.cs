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

namespace SDKTemplate.CCF.ScenarioFrameCCF.SupplierBillingScenario
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PayNowCC : Page
    {
        private PageNavigationParameter PageNavigationParameter { get; set; }
        private CheckoutViewModel _CV { get; set; }

        public PayNowCC()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.PageNavigationParameter = (PageNavigationParameter)e.Parameter;
            _CV = DataContext as CheckoutViewModel;
            
            _CV.ErrorsChanged += _SCV_ErrorsChanged;
            _CV.DueDate = DateTime.Now.AddHours(1);
            _CV.IntrestRate = "0";
            if (this.PageNavigationParameter.OrderType == OrderType.SupplierOrder)
            {
                var SPNP = this.PageNavigationParameter.SupplierPageNavigationParameter;
                _CV.AmountToBePaid = SPNP.SupplierBillingSummaryViewModel.BillAmount;
            }
            else
            {
                var CPNP = this.PageNavigationParameter.CustomerPageNavigationParameter;
                _CV.AmountToBePaid = CPNP.BillingSummaryViewModel.BillAmount;
            }
            _CV.PayingAmount = _CV.AmountToBePaid.ToString();
            base.OnNavigatedTo(e);
        }

        private void _SCV_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {

        }

        private async void ProceedToPayment_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = _CV.ValidateProperties();
            if (IsValid)
            {
                if (PayNowRadBtn.IsChecked == true)
                {
                    if (this.PageNavigationParameter.OrderType == OrderType.SupplierOrder)
                    {
                        this.PageNavigationParameter.SupplierPageNavigationParameter.SupplierCheckoutViewModel = _CV;
                        var IsCreated = await OrderDataSource.InitiateSupplierOrderCreationAsync(this.PageNavigationParameter.SupplierPageNavigationParameter);
                        MainPage.RefreshPage(ScenarioType.SupplierBilling);
                    }
                    else
                    {
                        this.PageNavigationParameter.CustomerPageNavigationParameter.CustomerCheckoutViewModel = _CV;
                        var IsCreated = await OrderDataSource.InitiateCustomerOrderCreationAsync(this.PageNavigationParameter.CustomerPageNavigationParameter);
                        MainPage.RefreshPage(ScenarioType.CustomerBilling);
                    }
                }
                else if (PayLaterRadBtn.IsChecked == true)
                {
                    this.Frame.Navigate(typeof(PayLaterCC), this.PageNavigationParameter);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
