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
using MasterDetailApp.Data;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PayNow : Page
    {
        private PageNavigationParameter PageNavigationParameter { get; set; }
        public PayNow()
        {
            this.InitializeComponent();
            PlaceOrderBtn.Click += PlaceOrderBtn_Click;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.PageNavigationParameter = (PageNavigationParameter)e.Parameter;
        }
        private void PlaceOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderDataSource.PlaceOrder(PageNavigationParameter);
            MainPage.Current.NotifyUser("Order Placed Successfully", NotifyType.StatusMessage);
            this.Frame.Navigate(typeof(BillingScenario));
        }
    }
}
