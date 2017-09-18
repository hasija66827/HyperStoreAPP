using Models;
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
using SDKTemplate.DTO;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OTPVerificationForPayingNow : Page
    {
        private CustomerPageNavigationParameter _PageNavigationParameter { get; set; }
        private OTPVerificationForPayingNowViewModel _OTPVerificationForPayingNow { get; set; }
        public OTPVerificationForPayingNow()
        {
            this.InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._PageNavigationParameter = (CustomerPageNavigationParameter)e.Parameter;
            var p = this._PageNavigationParameter;
            this._OTPVerificationForPayingNow = new OTPVerificationForPayingNowViewModel()
            {
                Customer = p.SelectedCustomer,
                WalletAmountToBeDeducted = p.SelectPaymentModeViewModelBase.WalletAmountToBeDeducted
            };
        }



        private async void ProceedBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsVerified = await _InitiateOTPVerificationAsync();
            if (IsVerified)
            {

                this.Frame.Navigate(typeof(PayNow), this._PageNavigationParameter);
            }
        }

        private async Task<bool> _InitiateOTPVerificationAsync()
        {
            string fomattedSMSContent = "";
            if (_OTPVerificationForPayingNow.WalletAmountToBeDeducted > 0)
            {
                var SMSContent = OTPVConstants.SMSContents[ScenarioType.PlacingCustomerOrder_Credit];
                fomattedSMSContent = String.Format(SMSContent, this._OTPVerificationForPayingNow.WalletAmountToBeDeducted,
                                                                    BaseURI.User.BusinessName, OTPVConstants.OTPLiteral);
            }
            else
            {
                var SMSContent = OTPVConstants.SMSContents[ScenarioType.PlacingCustomerOrder_Debit];
                fomattedSMSContent = String.Format(SMSContent, Math.Abs(this._OTPVerificationForPayingNow.WalletAmountToBeDeducted),
                                                                    BaseURI.User.BusinessName, OTPVConstants.OTPLiteral);
            }

            var OTPVerificationDTO = new OTPVerificationDTO()
            {
                UserID = BaseURI.User.UserId,
                ReceiverMobileNo = this._PageNavigationParameter?.SelectedCustomer?.MobileNo,
                SMSContent = fomattedSMSContent,
            };
            var IsVerified = await OTPDataSource.VerifyTransactionByOTPAsync(OTPVerificationDTO);
            return IsVerified;
        }
    }
}
