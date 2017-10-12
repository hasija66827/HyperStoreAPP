using Models;
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
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    public delegate Task SelectedSupplierChangedDelegate();
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SupplierASBCC : Page
    {
        public static SupplierASBCC Current;
        public TSupplier SelectedSupplierInASB { get { return this._selectedSupplierInASB; } }
        private List<SupplierASBViewModel> _Suppliers { get; set; }
        private SupplierASBViewModel _selectedSupplierInASB;
        public event SelectedSupplierChangedDelegate SelectedSupplierChangedEvent;
        public SupplierASBCC()
        {
            Current = this;
            this.InitializeComponent();
            this._Suppliers = null;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RefreshTheSuppliers();
            SupplierDataSource.SupplierCreatedEvent += RefreshTheSuppliers;
            SupplierDataSource.SupplierUpdatedEvent += RefreshTheSuppliers;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            SupplierDataSource.SupplierCreatedEvent -= RefreshTheSuppliers;
            SupplierDataSource.SupplierUpdatedEvent -= RefreshTheSuppliers;
            base.OnNavigatedFrom(e);
        }

        public void NotifyUser()
        {
            ErrorTB.Visibility = Visibility.Visible;
        }

        public async void RefreshTheSuppliers()
        {
            SupplierASB.Text = "";
            NoResults.Visibility = Visibility.Collapsed;
            SupplierDetails.Visibility = Visibility.Collapsed;
            var suppliers = await SupplierDataSource.RetrieveSuppliersAsync(null);
            if (suppliers != null)
                this._Suppliers = suppliers.Select(s => new SupplierASBViewModel(s)).ToList();
        }

        /// <summary>
        /// This event gets fired anytime the text in the TextBox gets updated.
        /// It is recommended to check the reason for the text changing by checking against args.Reason
        /// </summary>
        /// <param name="sender">The AutoSuggestBox whose text got changed.</param>
        /// <param name="args">The event arguments.</param>
        private void _SupplierASB_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (this._Suppliers == null)
                return;
            // We only want to get results when it was a user typing, 
            // otherwise we assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var matchingWholeSellers = _GetMatchingSuppliers(sender.Text);
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
        private void _SupplierASB_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (this._Suppliers == null)
                return;

            if (args.ChosenSuggestion != null)
            {
                // User selected an item, take an action on it here
                var choosenSupplier = args.ChosenSuggestion;
                _SelectSupplier((SupplierASBViewModel)choosenSupplier);
            }
            else
            {
                SupplierASBViewModel matchingWholeSeller = null;
                // if a text is present, find best possible match.
                if (args.QueryText != "")
                    matchingWholeSeller = (_GetMatchingSuppliers(args.QueryText)).FirstOrDefault();
                _SelectSupplier(matchingWholeSeller);
            }
        }

        private async void _SelectSupplier(SupplierASBViewModel supplier)
        {
            if (supplier != null)
            {
                var retrievedSupplier = await SupplierDataSource.RetrieveTheSupplierAsync(supplier.SupplierId);
                if (retrievedSupplier == null)
                    return;
                _selectedSupplierInASB = new SupplierASBViewModel(retrievedSupplier);
                NoResults.Visibility = Visibility.Collapsed;
                SupplierDetails.Visibility = Visibility.Visible;
                WholeSellerMobNo.Text = _selectedSupplierInASB.MobileNo;
                WholeSellerName.Text = _selectedSupplierInASB.Name;
                WholeSellerAddress.Text = _selectedSupplierInASB.Address != null ? _selectedSupplierInASB.Address : "";
                WholeSellerWalletBalance.Text = Utility.ConvertToRupee(_selectedSupplierInASB.WalletBalance);
                SupplierGlyph.Text = Utility.GetGlyphValue(_selectedSupplierInASB.Name);
                ErrorTB.Visibility = Visibility.Collapsed;
            }
            else
            {
                _selectedSupplierInASB = null;
                NoResults.Visibility = Visibility.Visible;
                SupplierDetails.Visibility = Visibility.Collapsed;
            }
            SelectedSupplierChangedEvent?.Invoke();
        }

        /// <summary>
        /// Do a fuzzy search on all Product and order results based on a pre-defined rule set
        /// </summary>
        /// <param name="query">The part of the name or company to look for</param>
        /// <returns>An ordered list of mobileNumber that matches the query</returns>
        private List<SupplierASBViewModel> _GetMatchingSuppliers(string query)
        {
            if (this._Suppliers == null)
                return null;
            return this._Suppliers
                .Where(item => item.MobileNo.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1
                            || item.Name?.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .OrderByDescending(item => item.MobileNo.StartsWith(query, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }
    }
}
