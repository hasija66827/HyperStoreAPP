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
using SDKTemplate.View_Models;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WholeSellerTransactionOTPVerification : Page
    {
        public WholeSellerTransactionViewModel WholeSellerTransactionViewModel { get; set; }
        public WholeSellerTransactionOTPVerification()
        {
            this.InitializeComponent();
            this.WholeSellerTransactionViewModel = new WholeSellerTransactionViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.WholeSellerTransactionViewModel = (WholeSellerTransactionViewModel)e.Parameter;
        }

        private void VerifyBtn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: verify OTP
            if (OTPTB.Text == "123456")
            {
                var db = new DatabaseModel.RetailerContext();
                var wholeSeller = this.WholeSellerTransactionViewModel.WholeSellerViewModel;
                var creditAmount = this.WholeSellerTransactionViewModel.CreditAmount;
                var transactionViewModel = new TransactionViewModel(creditAmount, DateTime.Now, wholeSeller);
                var updatedWalletBalance = TransactionDataSource.MakeTransaction(transactionViewModel, wholeSeller, db);
                MainPage.Current.NotifyUser("OTP Verified and The updated wallet balance of the wholeSeller is \u20b9" + updatedWalletBalance
                    , NotifyType.StatusMessage);
                this.Frame.Navigate(typeof(SupplierCCF));
            }
            else
            {
                MainPage.Current.NotifyUser("Invalid OTP", NotifyType.ErrorMessage);
            }
        }
    }
}
