using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace SDKTemplate
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductPricingFormCC : Page
    {
        public static ProductPricingFormCC Current;
        private ProductPricingDetailViewModel _PPDV;
        public ProductPricingFormCC()
        {
            Current = this;
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _PPDV = DataContext as ProductPricingDetailViewModel;
            _PPDV.ErrorsChanged += _PFV_ErrorsChanged;
            base.OnNavigatedTo(e);
        }

        private void _PFV_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage));
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = this._PPDV.ValidateProperties();
            if (IsValid)
                this.Frame.Navigate(typeof(ProductBasicFormCC), this._PPDV);
        }
    }
}
