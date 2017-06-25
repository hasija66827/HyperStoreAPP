using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
namespace SDKTemplate
{
    enum ProductPage
    {
        AddTheProduct,
        SearchTheProduct
    }
    public delegate void OnAddProductClickedDelegate(object sender, ProductViewModel productViewModel);
    public delegate void SelectedProductChangedDelegate(ProductASBViewModel productASBViewModel);
    public class ProductASBViewModel : ProductViewModelBase
    {
        // Property is used by ASB(AutoSuggestBox) for display member path and text member path property
        public string Product_Id_Name { get { return string.Format("{0} ({1})", BarCode, Name); } }
        //Constructor to convert parent obect to child object.
        public ProductASBViewModel(ProductViewModelBase parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
            {
                //If Property can be set then only we will set it.
                if (prop.CanWrite)
                    GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent));
            }
        }
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductASBCC : Page
    {
        public static ProductASBCC Current;
        public event OnAddProductClickedDelegate OnAddProductClickedEvent;
        public event SelectedProductChangedDelegate SelectedProductChangedEvent;
        private static ProductASBViewModel _selectedProductInASB;
        public ProductASBCC()
        {
            Current = this;
            this.InitializeComponent();
            AddToCartBtn.Click += AddToCartBtn_Click;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                var pageType = (ProductPage)e.Parameter;
                if (pageType == ProductPage.SearchTheProduct)
                {
                    Header.Text = "Search by Product";
                    AddToCartBtn.Visibility = Visibility.Collapsed;
                }
                else if(pageType==ProductPage.AddTheProduct)
                {
                    Header.Text = "Add the Product";
                    AddToCartBtn.Visibility = Visibility.Visible;
                }
            }
        }

        private void AddToCartBtn_Click(object sender, RoutedEventArgs e)
        {
            Current.OnAddProductClickedEvent?.Invoke(this, new ProductViewModel(_selectedProductInASB));
            this.ProductASB.Text = "";
        }

        /// <summary>
        /// This event gets fired anytime the text in the TextBox gets updated.
        /// It is recommended to check the reason for the text changing by checking against args.Reason
        /// </summary>
        /// <param name="sender">The AutoSuggestBox whose text got changed.</param>
        /// <param name="args">The event arguments.</param>
        private void ProductASB_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // We only want to get results when it was a user typing, 
            // otherwise we assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var matchingProducts = ProductDataSource.GetMatchingProducts(sender.Text);
                List<ProductASBViewModel> lstChild = matchingProducts.Select(product => new ProductASBViewModel(product)).ToList();
                sender.ItemsSource = lstChild;
            }
        }

        /// <summary>
        /// This event gets fired when:
        ///     * a user presses Enter while focus is in the TextBox
        ///     * a user clicks or tabs to and invokes the query button (defined using the QueryIcon API)
        ///     * a user presses selects (clicks/taps/presses Enter) a suggestion
        /// </summary>
        /// <param name="sender">The AutoSuggestBox that fired the event.</param>
        /// <param name="args">The args contain the QueryText, which is the text in the TextBox, 
        /// and also ChosenSuggestion, which is only non-null when a user selects an item in the list.</param>
        private void ProductASB_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            ProductASBViewModel selectedProductInASB;
            if (args.ChosenSuggestion != null)
            {
                selectedProductInASB = (ProductASBViewModel)args.ChosenSuggestion;
            }
            else
            {
                var matchingProducts = ProductDataSource.GetMatchingProducts(args.QueryText);
                // Choose the first match, or clear the selection if there are no matches.
                selectedProductInASB = new ProductASBViewModel(matchingProducts.FirstOrDefault());
            }
            SelectProduct(selectedProductInASB);
            Current.SelectedProductChangedEvent?.Invoke(selectedProductInASB);
        }
        /// <summary>
        /// Display details of the specified Product.
        /// </summary>
        /// <param name="Product"></param>
        private void SelectProduct(ProductASBViewModel product)
        {
            if (product != null)
            {
                _selectedProductInASB = product;
                NoResults.Visibility = Visibility.Collapsed;
                ProductDetails.Visibility = Visibility.Visible;
                ProductId.Text = product.BarCode;
                ProductName.Text = product.Name;
                ProductSellingPrice.Text = "\u20B9" + product.DisplayPrice * (100 - product.DiscountPer) / 100;
                ProductCostPrice.Text = "\u20B9" + product.DisplayPrice;
                ProductDiscountPer.Text = product.DiscountPer + "% Off";
                ProductGlyph.Text = Utility.GetGlyphValue(product.Name);
            }
            else
            {
                NoResults.Visibility = Visibility.Visible;
                ProductDetails.Visibility = Visibility.Collapsed;
            }
        } 
    }
}
