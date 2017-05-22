using SDKTemp.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using SDKTemp.ViewModel;
namespace SDKTemp
{
    public sealed partial class OrderListCC : Page
    {
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
                UpdateMasterListViewItemSource(choosenCustomer);
                ShowMasterList(true);
            }
            else if (CustomerASB.Text == "")
            {   //If no text present then show all the orders
                UpdateMasterListViewItemSource(null);
                ShowMasterList(true);
            }
            else
            {
                // if a text is present, find best possible match.
                var matchingCustomer = CustomerDataSource.GetMatchingCustomers(args.QueryText).FirstOrDefault();
                UpdateMasterListViewItemSource(matchingCustomer);
                ShowMasterList((matchingCustomer == null) ? false : true);
            }
        }
        /// <summary>
        /// Display details of the specified Product.
        /// </summary>
        /// <param name="Product"></param>
        private void ShowMasterList(bool IsOrderListFull)
        {
            if (!IsOrderListFull)
            {
                NoResults.Visibility = Visibility.Visible;
                MasterListView.Visibility = Visibility.Collapsed;
            }
            else
            {
                NoResults.Visibility = Visibility.Collapsed;
                MasterListView.Visibility = Visibility.Visible;
            }
        }
    }
}
