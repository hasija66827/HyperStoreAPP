using Models;
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
    public sealed partial class WholeSellerTransactionCC : Page
    {
        public WholeSellerTransactionViewModel WholeSellerTransactionViewModel { get; set; }
        public WholeSellerTransactionCC()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.WholeSellerTransactionViewModel = new WholeSellerTransactionViewModel((Models.TSupplier)e.Parameter);
        }

        private void AddMoney_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WholeSellerTransactionOTPVerification), this.WholeSellerTransactionViewModel);
        }
    }

    public class WholeSellerTransactionViewModel
    {
        public TSupplier WholeSellerViewModel { get; set; }
        public decimal CreditAmount { get; set; }
        public WholeSellerTransactionViewModel(Models.TSupplier w)
        {
            this.WholeSellerViewModel = w;
            this.CreditAmount = 0;
        }
        public WholeSellerTransactionViewModel()
        {
            this.WholeSellerViewModel = new Models.TSupplier   ();
            this.CreditAmount = 0;
        }
    }
}
