using SDKTemplate.Data_Source;
using SDKTemplate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PaymentOptionCC : Page
    {
        public static PaymentOptionCC Current;
        public ObservableCollection<PaymentOptionViewModel> PaymentOptions { get; set; }

        public PaymentOption SelectedPaymentOption
        {
            get
            {
               var selectedPaymentOptionViewModel= this.PaymentOptions.Where(p => p.IsChecked == true).FirstOrDefault();
                return selectedPaymentOptionViewModel?.PaymentOption;
            }
        }

        public PaymentOptionCC()
        {
            Current = this;
            this.InitializeComponent();
            this.PaymentOptions = new ObservableCollection<PaymentOptionViewModel>();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.InitializeAsync();        
            base.OnNavigatedTo(e);
        }

        private async Task InitializeAsync()
        {
            var paymentOptions = await PaymentOptionsDataSource.RetrievePaymentOptionsAsync();
            foreach (var paymentOption in paymentOptions)
            {
                var x = new PaymentOptionViewModel()
                {
                    PaymentOption = paymentOption,
                    IsChecked = true,
                };
                this.PaymentOptions.Add(x);
            }
        }
    }
}
