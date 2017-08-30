using SDKTemplate.DTO;
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
    public sealed partial class CustomerTransactionOTPVerification : Page
    {
        private CustomerNewTransactionViewModel _CustomerNewTransactionViewModel { get; set; }
        public CustomerTransactionOTPVerification()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._CustomerNewTransactionViewModel = (CustomerNewTransactionViewModel)e.Parameter;
        }

        private async void ProceedToPayBtn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: verify OTP
            if (OTPLabel.Text == "123456")
            {
                var transactionDTO = new CustomerTransactionDTO()
                {
                    CustomerId = this._CustomerNewTransactionViewModel?.Customer?.CustomerId,
                    IsCredit = false,
                    TransactionAmount = this._CustomerNewTransactionViewModel?.ReceivingAmount,
                };
                var transaction = await CustomerTransactionDataSource.CreateNewTransactionAsync(transactionDTO);
                this.Frame.Navigate(typeof(CustomersCCF));
            }
        }
    }
}
