using Models;
using SDKTemp.Data;
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
        public static CustomerASBCC Current;
        public TCustomer SelectedCustomerInASB { get { return this._selectedCustomerInASB; } }
        public event SelectedCustomerChangedDelegate SelectedCustomerChangedEvent;
        private CustomerASBViewModel _selectedCustomerInASB;
        private List<CustomerASBViewModel> _Customers { get; set; }
        public CustomerASBCC()
        {
            this.InitializeComponent();
            Current = this;
            this._Customers = null;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RefreshTheCustomers();
            CustomerDataSource.CustomerCreatedEvent += RefreshTheCustomers;
            CustomerDataSource.CustomerUpdatedEvent += RefreshTheCustomers;
            CustomerOrderDataSource.CustomerBalanceUpdatedEvent += RefreshTheCustomers;
            CustomerTransactionDataSource.CustomerUpdatedEvent += RefreshTheCustomers;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            CustomerDataSource.CustomerCreatedEvent -= RefreshTheCustomers;
            CustomerDataSource.CustomerUpdatedEvent -= RefreshTheCustomers;
            CustomerOrderDataSource.CustomerBalanceUpdatedEvent -= RefreshTheCustomers;
            CustomerTransactionDataSource.CustomerUpdatedEvent -= RefreshTheCustomers;
            base.OnNavigatedFrom(e);
        }

        public async void RefreshTheCustomers()
        {
            CustomerASB.Text = "";
            NoResults.Visibility = Visibility.Collapsed;
            CustomerDetails.Visibility = Visibility.Collapsed;
            var customers = await CustomerDataSource.RetrieveCustomersAsync(null);
            if (customers != null)
                this._Customers = customers.Select(c => new CustomerASBViewModel(c)).ToList();
        }

        /// <summary>
        /// This event gets fired anytime the text in the TextBox gets updated.
        /// It is recommended to check the reason for the text changing by checking against args.Reason
        /// </summary>
        /// <param name="sender">The AutoSuggestBox whose text got changed.</param>
        /// <param name="args">The event arguments.</param>
        private void _CustomerASB_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (this._Customers == null)
                return;
            // We only want to get results when it was a user typing, 
            // otherwise we assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var matchingCustomers = _GetMatchingCustomers(sender.Text);
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
        private void _CustomerASB_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (this._Customers == null)
                return;
            if (args.ChosenSuggestion != null)
            {
                // User selected an item, take an action on it here
                var choosenCustomer = (CustomerASBViewModel)args.ChosenSuggestion;
                _SelectCustomer(choosenCustomer);
            }
            else
            {
                CustomerASBViewModel matchingCustomer = null;
                if (args.QueryText != "")
                    matchingCustomer = _GetMatchingCustomers(args.QueryText).FirstOrDefault();
                _SelectCustomer(matchingCustomer);
            }
        }

        private void _SelectCustomer(CustomerASBViewModel customer)
        {
            if (customer != null)
            {
                _selectedCustomerInASB = customer;
                NoResults.Visibility = Visibility.Collapsed;
                CustomerDetails.Visibility = Visibility.Visible;
                CustomerMobNo.Text = customer.MobileNo;
                CustomerName.Text = customer.Name;
                CustomerAddress.Text = customer.Address != null ? customer.Address : "";
                CustomerWalletBalance.Text = Utility.ConvertToRupee(customer.WalletBalance);
                CustomerNetWorth.Text = "Total Sales: " + Utility.ConvertToRupee(customer.NetWorth);
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
        private List<CustomerASBViewModel> _GetMatchingCustomers(string query)
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





