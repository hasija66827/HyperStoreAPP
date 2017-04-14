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
using DatabaseModel;
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
            this.DateTimeLbl.Text = DateTime.Now.ToString();
            ProductDataSource.RetrieveProductDataAsync();
    
            AddToCart.Click += AddToCart_Click;
            CustomerMobileNumber.LostFocus += CustomerMobileNumber_LostFocus;
            Verify.Click += Verify_Click;
            _selectedProductInASB = null;
            
        }

        private void Verify_Click(object sender, RoutedEventArgs e)
        {
            //Open a Dialogue for OTP.
            //If Succesfully Verified.
            // Ask for Customer Name and address.
            // Create a new customer and save the Customer.
            using (var db = new RetailerContext())
            {
                var mobileNumber = CustomerMobNoTB.Text;
                Customer customer = new Customer("abc" + mobileNumber, mobileNumber);
                db.Customers.Add(customer);
                db.SaveChanges();
                ShowVerified(customer.Name, customer.WalletBalance);
            }
        }

        private void CustomerMobileNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            var mobileNumber = CustomerMobNoTB.Text;
            //TODO: Verify the input charecter

            using (var db = new RetailerContext())
            {
                // Check If c.Text exist in customer database
                var customers=db.Customers.Where(c => c.MobileNo.Equals(mobileNumber));
                if (customers.Count()==0)
                {
                    ShowUnverified();
                }
                else
                {
                    var customer = customers.First();
                    ShowVerified(customer.Name, customer.WalletBalance);
                }
            }
        }
        public void ShowVerified(string customerName, float customerWalletBalance)
        {
            Verify.Visibility = Visibility.Collapsed;
            IsVerified.Visibility = Visibility.Visible;
            CustomerNameTB.Text = customerName;
            CustomerWalletBalanceTB.Text = "\u20b9" + customerWalletBalance;
        }
        public void ShowUnverified()
        {
            Verify.Visibility = Visibility.Visible;
            IsVerified.Visibility = Visibility.Collapsed;
            CustomerNameTB.Text = "";
            CustomerWalletBalanceTB.Text = "\u20b9" + "0";
        }
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            Int32 index= this.ViewModel.AddToCart(_selectedProductInASB);
            // Scrolling the list to the last element in the list. FYI: only works for distinct items.
            //TODO: Below loe is affecting the performance of addition of items in cart.
            //ListView.ScrollIntoView(ListView.Items[index]);
            ListView.SelectedIndex = index;
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
                ProductSellingPrice.Text = "\u20B9"+product.SellingPrice.ToString();
                ProductCostPrice.Text = "\u20B9"+product.CostPrice.ToString();
                ProductDiscountPer.Text = product.DiscountPer+"% Off";
            }
            else
            {
                NoResults.Visibility = Visibility.Visible;
                ProductDetails.Visibility = Visibility.Collapsed;
            }
        }
    }
}
