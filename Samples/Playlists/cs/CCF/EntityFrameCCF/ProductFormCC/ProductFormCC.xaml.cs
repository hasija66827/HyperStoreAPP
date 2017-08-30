using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace SDKTemplate
{
    public enum Mode
    {
        Create,
        Update
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductDetailsCC : Page
    {
        public static ProductDetailsCC Current;
        public ProductViewModelBase ProductViewModelBase { get { return this._PFV; } }
        public Mode Mode { get { return this._mode; } }
        private ProductFormViewModel _PFV;
        private Mode _mode;
        public ProductDetailsCC()
        {
            Current = this;
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var product = (ProductViewModelBase)e.Parameter;
            if (product == null)
            {
                this._PFV = new ProductFormViewModel();
                this._mode = Mode.Create;
            }
            else
            {
                this._PFV = new ProductFormViewModel(product);
                ProductCodeTB.IsReadOnly = true;
                ProductNameTB.IsReadOnly = true;
                this._mode = Mode.Update;
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage));
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Utility.CheckIfValidProductCode(ProductCodeTB.Text)
                && Utility.CheckIfUniqueProductName(ProductNameTB.Text)
                && Utility.CheckIfUniqueProductCode(ProductCodeTB.Text)
                )
            {
                this.Frame.Navigate(typeof(FilterProductByTagCC), this._mode);
            }
        }
    }
}
