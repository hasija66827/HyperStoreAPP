using SDKTemp.Data;
using SDKTemplate.DTO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PayNow : Page
    {
        private CustomerPageNavigationParameter _PageNavigationParameter { get; set; }
        private PayNowViewModel PayNowViewModel { get; set; }
        public PayNow()
        {
            this.InitializeComponent();
            PlaceOrderBtn.Click += PlaceOrderBtn_Click;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._PageNavigationParameter = (CustomerPageNavigationParameter)e.Parameter;
            var p = this._PageNavigationParameter;
            this.PayNowViewModel = new PayNowViewModel()
            {
                ToBePaid = p.SelectPaymentModeViewModelBase.ToBePaid,
                ActuallyPaying = p.SelectPaymentModeViewModelBase.ToBePaid
            };
        }

        private async void PlaceOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            var usingWalletAmount = await CustomerOrderDataSource.PlaceOrderAsync(this._PageNavigationParameter, this.PayNowViewModel.ActuallyPaying);
            if (usingWalletAmount != null)
                MainPage.RefreshPage(ScenarioType.CustomerBilling);
        }
    }
}
