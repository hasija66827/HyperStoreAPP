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
        public AddProductViewModel AddProductViewModel;
        private Mode _Mode { get; set; }
        public ProductDetailsCC()
        {
            this.InitializeComponent();
        }

        private void ProductCodeTB_LostFocus(object sender, RoutedEventArgs e)
        {
            Utility.CheckIfValidProductCode(ProductCodeTB.Text);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var product = (ProductViewModelBase)e.Parameter;
            if (product == null)
            {
                this.AddProductViewModel = new AddProductViewModel();
                this._Mode = Mode.Create;
            }
            else
            {
                this.AddProductViewModel = new AddProductViewModel(product);
                ProductCodeTB.IsReadOnly = true;
                ProductNameTB.IsReadOnly = true;
                this._Mode = Mode.Update;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this._Mode == Mode.Create)
            {
                if (Utility.CheckIfValidProductCode(ProductCodeTB.Text)
                    && Utility.CheckIfUniqueProductName(ProductNameTB.Text)
                    && Utility.CheckIfUniqueProductCode(ProductCodeTB.Text)
                    )
                {
                    if (ProductDataSource.AddProduct(AddProductViewModel) == true)
                        MainPage.Current.NotifyUser("The product was created succesfully", NotifyType.StatusMessage);
                }
            }
            else if(this._Mode==Mode.Update)
            {
               
                    if (ProductDataSource.UpdateProductDetails(this.AddProductViewModel) == true)
                        MainPage.Current.NotifyUser("The Product was updated scuccesfully", NotifyType.StatusMessage);
            }
            this.Frame.Navigate(typeof(BlankPage));
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage));
        }
    }
}
