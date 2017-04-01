//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Media.Playlists;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SDKTemplate
{
    public sealed partial class BillingScenario : Page
    {
        private MainPage rootPage = MainPage.Current;
        public ProductViewModel ViewModel { get; set; }
        private static Product _selectedProductInASB;
        public BillingScenario()
        {
            this.InitializeComponent();
            this.ViewModel = new ProductViewModel();
            ProductDataSource.RetrieveProductDataAsync();
            AddToCart.Click += AddToCart_Click;
            _selectedProductInASB = null;
            
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModel.AddToCart(_selectedProductInASB);
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
                sender.ItemsSource = matchingProducts.ToList();
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
            if (args.ChosenSuggestion != null)
            {
                // User selected an item, take an action on it here
                SelectProduct((Product)args.ChosenSuggestion);
            }
            else
            {
                // Do a fuzzy search on the query text.
                var matchingProducts = ProductDataSource.GetMatchingProducts(args.QueryText);

                // Choose the first match, or clear the selection if there are no matches.
                SelectProduct(matchingProducts.FirstOrDefault());
            }
        }

        /// <summary>
        /// Display details of the specified Product.
        /// </summary>
        /// <param name="Product"></param>
        private void SelectProduct(Product product)
        {
            if (product != null)
            {
                _selectedProductInASB = product;
                NoResults.Visibility = Visibility.Collapsed;
                ProductDetails.Visibility = Visibility.Visible;
                ProductId.Text = product.Id;
                ProductName.Text = product.Name;
                ProductSellingPrice.Text = product.SellingPrice.ToString();
                ProductCostPrice.Text = product.CostPrice.ToString();
                ProductDiscountPer.Text = product.DiscountPer+"% off";
            }
            else
            {
                NoResults.Visibility = Visibility.Visible;
                ProductDetails.Visibility = Visibility.Collapsed;
            }
        }
    }
}
