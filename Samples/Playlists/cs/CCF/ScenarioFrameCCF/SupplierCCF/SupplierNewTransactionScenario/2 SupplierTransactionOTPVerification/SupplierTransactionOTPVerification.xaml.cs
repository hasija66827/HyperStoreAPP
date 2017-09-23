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
            var IsVerified = await _InitiateOTPVerificationAsync();
            if (IsVerified)
            {
                var transactionDTO = new SupplierTransactionDTO()
                {
                    SupplierId = this._SupplierNewTransactionViewModel?.Supplier?.SupplierId,
                    IsCredit = false,
                    TransactionAmount = Utility.TryToConvertToDecimal(this._SupplierNewTransactionViewModel?.PayingAmount),
                    Description = this._SupplierNewTransactionViewModel?.Description,
                };
                var transaction = await SupplierTransactionDataSource.CreateNewTransactionAsync(transactionDTO);
                if (transaction != null)
                    this.Frame.Navigate(typeof(SupplierCCF));
            }
        }

        private async Task<bool> _InitiateOTPVerificationAsync()
        {
            var SMSContent = OTPVConstants.SMSContents[ScenarioType.PayToSupplier_Transaction];
            var fomattedSMSContent = String.Format(SMSContent, this._SupplierNewTransactionViewModel?.PayingAmount, _SupplierNewTransactionViewModel?.Supplier?.Name, OTPVConstants.OTPLiteral);
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
