using SDKTemp.Data;
using SDKTemp.ViewModel;
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
    public delegate void SelectedCustomerChangedDelegate(object sender);
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerASBCC : Page
    {
        public static CustomerASBCC Current;
        public CustomerViewModel SelectedCustomerInASB { get { return this._selectedCustomerInASB; } }
        private CustomerViewModel _selectedCustomerInASB;
        public event SelectedCustomerChangedDelegate SelectedCustomerChangedEvent;
        public CustomerASBCC()
        {
            this.InitializeComponent();
            Current = this;
        }

        /// <summary>
        /// This event gets fired anytime the text in the TextBox gets updated.
        /// It is recommended to check the reason for the text changing by checking against args.Reason
        /// </summary>
        /// <param name="sender">The AutoSuggestBox whose text got changed.</param>
        /// <param name="args">The event arguments.</param>
        private void CustomerASB_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // We only want to get results when it was a user typing, 
            // otherwise we assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var matchingCustomers = CustomerDataSource.GetMatchingCustomers(sender.Text);
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
            if (args.ChosenSuggestion != null)
            {
                // User selected an item, take an action on it here
                var choosenCustomer = (CustomerViewModel)args.ChosenSuggestion;
                SelectCustomer(choosenCustomer);
            }
            else
            {
                CustomerViewModel matchingCustomer = null;
                if (args.QueryText != "")
                    matchingCustomer = CustomerDataSource.GetMatchingCustomers(args.QueryText).FirstOrDefault();
                SelectCustomer(matchingCustomer);
            }
        }

        private void SelectCustomer(CustomerViewModel customer)
        {
            if (customer != null)
            {
                _selectedCustomerInASB = customer;
                NoResults.Visibility = Visibility.Collapsed;
                CustomerDetails.Visibility = Visibility.Visible;
                CustomerMobNo.Text = customer.MobileNo;
                CustomerName.Text = customer.Name;
                CustomerAddress.Text = customer.Address;
                CustomerWalletBalance.Text = customer.WalletBalance.ToString() + "\u20B9";
                CustomerGlyph.Text = Utility.GetGlyphValue(customer.Name);
            }
            else
            {
                _selectedCustomerInASB = null;
                NoResults.Visibility = Visibility.Visible;
                CustomerDetails.Visibility = Visibility.Collapsed;
            }
            SelectedCustomerChangedEvent?.Invoke(this);
        }
    }
}





