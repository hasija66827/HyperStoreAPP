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
    public sealed partial class CustomerTransactionOTPVerification : Page
    {
        private CustomerNewTransactionViewModel _CNTV { get; set; }
        public CustomerTransactionOTPVerification()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._CNTV = (CustomerNewTransactionViewModel)e.Parameter;
        }

        private async void ProceedToPayBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsVerified = await _InitiateOTPVerificationAsync();
            if (IsVerified)
            {
                var transactionDTO = new CustomerTransactionDTO()
                {
                    CustomerId = this._CNTV?.Customer?.CustomerId,
                    IsCredit = false,
                    TransactionAmount = Utility.TryToConvertToDecimal(this._CNTV?.ReceivingAmount),
                    Description = this._CNTV.Description,
                    IsCashbackTransaction = this._CNTV.IsCashBackTransaction,
                };
                var transaction = await CustomerTransactionDataSource.CreateNewTransactionAsync(transactionDTO);
                if (transaction != null)
                    this.Frame.Navigate(typeof(CustomersCCF));
            }
        }

        private async Task<bool> _InitiateOTPVerificationAsync()
        {
            var SMSContent = OTPVConstants.SMSContents[OTPScenarioType.ReceiveFromCustomer_Transaction];
            var fomattedSMSContent = String.Format(SMSContent, this._CNTV?.ReceivingAmount, BaseURI.User.BusinessName, OTPVConstants.OTPLiteral);
            var OTPVerificationDTO = new OTPVerificationDTO()
            {
                UserID = BaseURI.User.UserId,
                ReceiverMobileNo = this._CNTV?.Customer?.MobileNo,
                SMSContent = fomattedSMSContent,
            };
            var IsVerified = await OTPDataSource.VerifyTransactionByOTPAsync(OTPVerificationDTO);
            return IsVerified;
        }
    }
}
