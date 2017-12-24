using HyperStoreServiceAPP.DTO;
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
using LoginSignUpService.DTO;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TransactionConfirmationCC : Page
    {
        private NewTransactionViewModel _NewTransactionViewModel { get; set; }
        public TransactionConfirmationCC()
        {
            this.InitializeComponent();
            PaymentModeFrame.Navigate(typeof(PaymentOptionCC));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._NewTransactionViewModel = (NewTransactionViewModel)e.Parameter;
        }

        private async void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsVerified = await _InitiatePasscodeVerificationAsync();
            if (IsVerified)
            {
                var transactionDTO = new TransactionDTO()
                {
                    PersonId = this._NewTransactionViewModel?.Person?.PersonId,
                    IsCredit = this._NewTransactionViewModel.Person.EntityType == EntityType.Supplier ? false : true,
                    TransactionAmount = Utility.TryToConvertToDecimal(this._NewTransactionViewModel?.Amount),
                    Description = this._NewTransactionViewModel?.Description,
                    PaymentOptionId = PaymentOptionCC.Current?.SelectedPaymentOption?.PaymentOptionId,
                };
                var transaction = await TransactionDataSource.CreateNewTransactionAsync(transactionDTO);
                if (transaction != null)
                {
                    if (this._NewTransactionViewModel.Person.EntityType == EntityType.Supplier)
                        MainPage.RefreshPage(ScenarioType.Suppliers);
                    else
                        MainPage.RefreshPage(ScenarioType.Customers);
                }
            }
        }

        private async Task<bool> _InitiatePasscodeVerificationAsync()
        {
            var passcodeDialog = new PasscodeDialogCC.PasscodeDialogCC(BaseURI.User.Passcode);
            await passcodeDialog.ShowAsync();
            return passcodeDialog.IsVerified;
        }

        //Currently we are not allowing OTPVerification for supplier transaction.
        private async Task<bool> _InitiateOTPVerificationAsync()
        {
            var SMSContent = OTPVConstants.SMSContents[OTPScenarioType.PayToSupplier_Transaction];
            var fomattedSMSContent = String.Format(SMSContent, this._NewTransactionViewModel?.Amount, _NewTransactionViewModel?.Person?.Name, OTPVConstants.OTPLiteral);
            var OTPVerificationDTO = new OTPVerificationDTO()
            {
                UserID = BaseURI.User.UserId,
                ReceiverMobileNo = BaseURI.User.MobileNo,
                SMSContent = fomattedSMSContent,
            };
            var IsVerified = await OTPDataSource.VerifyTransactionByOTPAsync(OTPVerificationDTO);
            return IsVerified;
        }
    }
}
