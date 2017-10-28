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
    public sealed partial class SupplierCheckoutCC : Page
    {
        private SupplierCheckoutViewModel _SCV { get; set; }
        private SupplierPageNavigationParameter SupplierPageNavigationParameter { get; set; }
        public SupplierCheckoutCC()
        {
            this.InitializeComponent();
            PlaceOrderBtn.Click += _PlaceOrderBtn_Click;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.SupplierPageNavigationParameter = (SupplierPageNavigationParameter)e.Parameter;
            var PNP = this.SupplierPageNavigationParameter;
            _SCV = DataContext as SupplierCheckoutViewModel;
            _SCV.ErrorsChanged += _SCV_ErrorsChanged;
            _SCV.AmountToBePaid = PNP.SupplierBillingSummaryViewModel.BillAmount;
            _SCV.PayingAmount = null;
            _SCV.DueDate = DateTime.Now.AddDays(45);
            _SCV.IntrestRate = null;
        }

        private void _SCV_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {

        }

        private async void _PlaceOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = _SCV.ValidateProperties();
            if (IsValid)
            {
                this.SupplierPageNavigationParameter.SupplierCheckoutViewModel = _SCV;
                var IsCreated = await SupplierOrderDataSource.InitiateSupplierOrderCreation(this.SupplierPageNavigationParameter);
            }
        }
    }
}
