using Models;
using SDKTemp.Data;
using SDKTemplate.Data_Source;
using SDKTemplate.DTO;
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
    public delegate Task SelectedCustomerChangedDelegate();
    public sealed partial class CustomerASBCC : Page
    {
        private class CustomerASBViewModel : TCustomer
        {
            public string Customer_MobileNo_Name
            {
                get { return string.Format("{0}({1})", MobileNo, Name); }
            }
            public CustomerASBViewModel(TCustomer parent)
            {
                foreach (PropertyInfo prop in parent.GetType().GetProperties())
                    GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
            }
        }

        public static CustomerASBCC Current;
        private List<CustomerASBViewModel> _Customers { get; set; }
        public TCustomer SelectedCustomerInASB { get { return this._selectedCustomerInASB; } }
        private CustomerASBViewModel _selectedCustomerInASB;
        public event SelectedCustomerChangedDelegate SelectedCustomerChangedEvent;
        public CustomerASBCC()
        {
            this.InitializeComponent();
            Current = this;
            this._Customers = null;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var customers = await CustomerDataSource.RetrieveCustomersAsync(null);
            this._Customers =customers.Select(c => new CustomerASBViewModel(c)).ToList();
        }

        /// <summary>
        /// This event gets fired anytime the text in the TextBox gets updated.
        /// It is recommended to check the reason for the text changing by checking against args.Reason
        /// </summary>
        /// <param name="sender">The AutoSuggestBox whose text got changed.</param>
        /// <param name="args">The event arguments.</param>
        private void CustomerASB_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (this._Customers == null)
                return;
            // We only want to get results when it was a user typing, 
            // otherwise we assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var matchingCustomers = GetMatchingCustomers(sender.Text);
                sender.ItemsSource = matchingCustomers.ToList();
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
        private void CustomerASB_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (this._Customers == null)
                return;
            if (args.ChosenSuggestion != null)
            {
                // User selected an item, take an action on it here
                var choosenCustomer = (CustomerASBViewModel)args.ChosenSuggestion;
                SelectCustomer(choosenCustomer);
            }
            else
            {
                CustomerASBViewModel matchingCustomer = null;
                if (args.QueryText != "")
                    matchingCustomer = GetMatchingCustomers(args.QueryText).FirstOrDefault();
                SelectCustomer(matchingCustomer);
            }
        }

        private void SelectCustomer(CustomerASBViewModel customer)
        {
            if (customer != null)
            {
                _selectedCustomerInASB = customer;
                NoResults.Visibility = Visibility.Collapsed;
                CustomerDetails.Visibility = Visibility.Visible;
                CustomerMobNo.Text = customer.MobileNo;
                CustomerName.Text = customer.Name;
                CustomerAddress.Text = customer.Address;
                CustomerWalletBalance.Text = Utility.FloatToRupeeConverter(customer.WalletBalance);
                CustomerGlyph.Text = Utility.GetGlyphValue(customer.Name);
            }
            else
            {
                _selectedCustomerInASB = null;
                NoResults.Visibility = Visibility.Visible;
                CustomerDetails.Visibility = Visibility.Collapsed;
            }
            SelectedCustomerChangedEvent?.Invoke();
        }

        /// <summary>
        /// Do a fuzzy search on all Product and order results based on a pre-defined rule set
        /// </summary>
        /// <param name="query">The part of the name or company to look for</param>
        /// <returns>An ordered list of mobileNumber that matches the query</returns>
        private List<CustomerASBViewModel> GetMatchingCustomers(string query)
        {
            if (this._Customers == null)
                return null;
            return _Customers
                .Where(item => item.MobileNo.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1
                            || item.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .OrderByDescending(item => item.MobileNo.StartsWith(query, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }
    }
}





