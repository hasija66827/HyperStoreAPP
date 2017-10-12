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
using SDKTemp.Data;
using Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SDKTemplate.DTO;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PayLaterOTPVerification : Page
    {
        private PayLaterModeViewModel _PLV { get; set; }
        private CustomerPageNavigationParameter _PageNavigationParameter { get; set; }
        public PayLaterOTPVerification()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._PageNavigationParameter = (CustomerPageNavigationParameter)e.Parameter;
            var PNP = this._PageNavigationParameter;
            _PLV = DataContext as PayLaterModeViewModel;
            _PLV.ErrorsChanged += _PLV_ErrorsChanged;
            _PLV.AmountToBePaid = PNP.SelectPaymentModeViewModelBase.ToBePaid;
            _PLV.PayingAmount = null;
        }

        private void _PLV_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
        }

        private async void ProceedBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = _PLV.ValidateProperties();
            if (IsValid)
            {
                var IsVerified = await _InitiateOTPVerificationAsync();
                if (IsVerified)
                {
                    var usingWalletAmount = await CustomerOrderDataSource.PlaceOrderAsync(this._PageNavigationParameter, Utility.TryToConvertToDecimal(this._PLV.PayingAmount));
                    if (usingWalletAmount != null)
                        MainPage.RefreshPage(ScenarioType.CustomerBilling);
                }
            }
        }

        private async Task<bool> _InitiateOTPVerificationAsync()
        {
            var SMSContent = OTPVConstants.SMSContents[OTPScenarioType.PlacingCustomerOrder_Credit];
            var fomattedSMSContent = String.Format(SMSContent, this._PLV?.AmountToBePaidLater, BaseURI.User.BusinessName, OTPVConstants.OTPLiteral);
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
