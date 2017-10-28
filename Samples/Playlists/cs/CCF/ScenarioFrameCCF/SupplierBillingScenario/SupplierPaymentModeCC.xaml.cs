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
    public sealed partial class SupplierPaymentModeCC : Page
    {
        private SupplierPageNavigationParameter SupplierPageNavigationParameter { get; set; }
        private SupplierCheckoutViewModel _SCV { get; set; }

        public SupplierPaymentModeCC()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.SupplierPageNavigationParameter = (SupplierPageNavigationParameter)e.Parameter;
            var PNP = this.SupplierPageNavigationParameter;
            _SCV = DataContext as SupplierCheckoutViewModel;
            _SCV.ErrorsChanged += _SCV_ErrorsChanged;
            _SCV.AmountToBePaid = PNP.SupplierBillingSummaryViewModel.BillAmount;
            _SCV.PayingAmount = PNP.SupplierBillingSummaryViewModel.BillAmount.ToString();
            _SCV.DueDate = DateTime.Now.AddHours(1);
            _SCV.IntrestRate = "0";
            base.OnNavigatedTo(e);
        }

        private void _SCV_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {

        }

        private async void ProceedToPayment_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = _SCV.ValidateProperties();
            if (IsValid)
            {
                if (PayNowRadBtn.IsChecked == true)
                {
                    this.SupplierPageNavigationParameter.SupplierCheckoutViewModel = _SCV;
                    var IsCreated = await SupplierOrderDataSource.InitiateSupplierOrderCreation(this.SupplierPageNavigationParameter);
                }
                else if (PayLaterRadBtn.IsChecked == true)
                {
                    this.Frame.Navigate(typeof(SupplierCheckoutCC), this.SupplierPageNavigationParameter);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
