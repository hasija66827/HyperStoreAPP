﻿using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
namespace SDKTemplate
{
    enum ProductPage
    {
        AddTheProduct,
        SearchTheProduct
    }

    public delegate void OnAddProductClickedDelegate(TProduct product);
    public delegate Task SelectedProductChangedDelegate();


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductASBCC : Page
    {
        public static ProductASBCC Current;
        public TProduct SelectedProductInASB { get { return this._selectedProductInASB; } }
        public event OnAddProductClickedDelegate OnAddProductClickedEvent;
        public event SelectedProductChangedDelegate SelectedProductChangedEvent;
        private ProductASBViewModel _selectedProductInASB;
        private List<ProductASBViewModel> _Products { get; set; }
        public ProductASBCC()
        {
            Current = this;
            this.InitializeComponent();
            AddToCartBtn.Click += AddToCartBtn_Click;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                var pageType = (ProductPage)e.Parameter;
                if (pageType == ProductPage.SearchTheProduct)
                {
                    AddToCartBtn.Visibility = Visibility.Collapsed;
                }
                else if (pageType == ProductPage.AddTheProduct)
                {
                    AddToCartBtn.Visibility = Visibility.Visible;
                }
            }
            var products = await ProductDataSource.RetrieveProductDataAsync(null);
            if (products != null)
                this._Products = products.Select(p => new ProductASBViewModel(p)).ToList();
        }

        private void AddToCartBtn_Click(object sender, RoutedEventArgs e)
        {
            this.OnAddProductClickedEvent?.Invoke(this.SelectedProductInASB);
        }

        /// <summary>
        /// This event gets fired anytime the text in the TextBox gets updated.
        /// It is recommended to check the reason for the text changing by checking against args.Reason
        /// </summary>
        /// <param name="sender">The AutoSuggestBox whose text got changed.</param>
        /// <param name="args">The event arguments.</param>
        private void ProductASB_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (this._Products == null)
                return;
            // We only want to get results when it was a user typing, 
            // otherwise we assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                sender.ItemsSource = GetMatchingProducts(sender.Text);
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
            if (this._Products == null)
                return;

            ProductASBViewModel selectedProductInASB;
            if (args.ChosenSuggestion != null)
            {
                selectedProductInASB = (ProductASBViewModel)args.ChosenSuggestion;
            }
            else if (args.QueryText == "")
            {
                selectedProductInASB = null;
            }
            else
            {
                var matchingProducts = GetMatchingProducts(args.QueryText);
                // Choose the first match, or clear the selection if there are no matches.
                var product = matchingProducts.FirstOrDefault();
                if (product == null)
                    selectedProductInASB = null;
                else
                    selectedProductInASB = new ProductASBViewModel(matchingProducts.FirstOrDefault());
            }
            SelectProduct(selectedProductInASB);
            Current.SelectedProductChangedEvent?.Invoke();
        }

        /// <summary>
        /// Display details of the specified Product.
        /// </summary>
        /// <param name="Product"></param>
        private void SelectProduct(ProductASBViewModel product)
        {
            this._selectedProductInASB = product;
            if (product != null)
            {
                NoResults.Visibility = Visibility.Collapsed;
                ProductDetails.Visibility = Visibility.Visible;
                ProductId.Text = product.Code;
                ProductName.Text = product.FormattedNameQuantity;
                ProductSellingPrice.Text = Utility.ConvertToRupee(product.ValueIncTax);
                ProductCostPrice.Text = Utility.ConvertToRupee(product.MRP);
                ProductDiscountPer.Text = product.DiscountPer + "% Off";
                ProductGSTPer.Text = product.SGSTPer + product.CGSTPer + "%GST";
                ProductGlyph.Text = Utility.GetGlyphValue(product.Name);
            }
            else
            {
                NoResults.Visibility = Visibility.Visible;
                ProductDetails.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Do a fuzzy search on all Product and order results based on product name or barcode
        /// </summary>
        /// <param name="query">The part of the name or company to look for</param>
        /// <returns>An ordered list of Product that matches the query</returns>
        private List<ProductASBViewModel> GetMatchingProducts(string query)
        {
            if (_Products == null)
                return null;
            return _Products
                .Where(p => p.Code.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1 ||
                            p.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .OrderByDescending(c => c.Code.StartsWith(query, StringComparison.CurrentCultureIgnoreCase))
                .ThenByDescending(c => c.Name.StartsWith(query, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }
    }
}
