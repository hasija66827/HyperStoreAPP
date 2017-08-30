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
    public sealed partial class SupplierTransactionOTPVerificationCC : Page
    {
        private SupplierNewTransactionViewModel _SupplierNewTransactionViewModel { get; set; }
        public SupplierTransactionOTPVerificationCC()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._SupplierNewTransactionViewModel = (SupplierNewTransactionViewModel)e.Parameter;
        }

        private async void VerifyBtn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: verify OTP
            if (OTPLabel.Text == "123456")
            {
                var transactionDTO = new SupplierTransactionDTO()
                {
                    SupplierId = this._SupplierNewTransactionViewModel?.Supplier?.SupplierId,
                    IsCredit = false,
                    TransactionAmount = this._SupplierNewTransactionViewModel?.PayingAmount,
                    Description=this._SupplierNewTransactionViewModel?.OptionalDescription,
                };
                var transaction = await SupplierTransactionDataSource.CreateNewTransactionAsync(transactionDTO);

                MainPage.Current.NotifyUser("OTP Verified and The updated wallet balance of the wholeSeller is \u20b9"
                    + (transaction.WalletSnapshot - transaction.TransactionAmount), NotifyType.StatusMessage);
                this.Frame.Navigate(typeof(SupplierCCF));
            }
            else
            {
                MainPage.Current.NotifyUser("Invalid OTP", NotifyType.ErrorMessage);
            }
        }
    }
}
