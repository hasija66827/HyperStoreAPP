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
using SDKTemp.ViewModel;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    public delegate void SelectedWholeSellerChangedDelegate(WholeSellerViewModel WholeSeller);
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WholeSellerASBCC : Page
    {
        public static WholeSellerASBCC Current;
        public WholeSellerViewModel SelectedWholeSellerInASB { get { return this._selectedWholeSellerInASB; } }
        private WholeSellerViewModel _selectedWholeSellerInASB;
        public event SelectedWholeSellerChangedDelegate SelectedWholeSellerChangedEvent;
        public WholeSellerASBCC()
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
        private void WholeSellerASB_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // We only want to get results when it was a user typing, 
            // otherwise we assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var matchingWholeSellers = WholeSellerDataSource.GetMatchingWholeSellers(sender.Text);
                sender.ItemsSource = matchingWholeSellers.ToList();
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
        private void WholeSellerASB_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                // User selected an item, take an action on it here
                var choosenWholeSeller = args.ChosenSuggestion;
                SelectWholeSeller((WholeSellerViewModel)choosenWholeSeller);
            }
            else
            {
                // if a text is present, find best possible match.
                var matchingWholeSeller = WholeSellerDataSource.GetMatchingWholeSellers(args.QueryText).FirstOrDefault();
                SelectWholeSeller(matchingWholeSeller);
            }
        }

        private void SelectWholeSeller(WholeSellerViewModel WholeSeller)
        {
            if (WholeSeller != null)
            {
                _selectedWholeSellerInASB = WholeSeller;
                NoResults.Visibility = Visibility.Collapsed;
                WholeSellerDetails.Visibility = Visibility.Visible;
                WholeSellerMobNo.Text = WholeSeller.MobileNo;
                WholeSellerName.Text = WholeSeller.Name;
                WholeSellerAddress.Text = WholeSeller.Address;
                WholeSellerWalletBalance.Text = WholeSeller.WalletBalance.ToString() + "\u20B9";
                WholeSellerGlyph.Text = Utility.GetGlyphValue(WholeSeller.Name);
            }
            else
            {
                _selectedWholeSellerInASB = null;
                NoResults.Visibility = Visibility.Visible;
                WholeSellerDetails.Visibility = Visibility.Collapsed;
            }
            SelectedWholeSellerChangedEvent?.Invoke(WholeSeller);
        }
    }
}
