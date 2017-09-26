using Models;
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
    public sealed partial class CustomerNewTransactionCC : Page
    {
        private CustomerNewTransactionViewModel _CNTV { get; set; }
        public CustomerNewTransactionCC()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var selecetedCustomer = (TCustomer)e.Parameter;
            _CNTV = DataContext as CustomerNewTransactionViewModel;
            _CNTV.ErrorsChanged += _CNTV_ErrorsChanged;
            _CNTV.Customer = selecetedCustomer;
            _CNTV.ReceivingAmount = null;
            _CNTV.IsCashBackTransaction = false;
        }

        private void _CNTV_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
        }

        private void ProceedBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = _CNTV.ValidateProperties();
            if (IsValid)
            {
                this.Frame.Navigate(typeof(CustomerTransactionOTPVerification), this._CNTV);
            }
        }
    }
}
