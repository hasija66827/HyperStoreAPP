using HyperStoreServiceAPP.DTO;
using Models;
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
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    public delegate Task SelectedPersonChangedDelegate();
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PersonASBCC : Page
    {
        public static PersonASBCC Current;
        private EntityType EntityType { get; set; }
        public Person SelectedPersonInASB { get { return this._selectePersonInASB; } }
        private List<PersonASBViewModel> _Persons { get; set; }
        private PersonASBViewModel _selectePersonInASB;
        public event SelectedPersonChangedDelegate SelectedPersonChangedEvent;
        public PersonASBCC()
        {
            Current = this;
            this.InitializeComponent();
            this._Persons = null;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.EntityType = (EntityType)e.Parameter;
            PersonASBTitleTB.Text = this.EntityType.ToString();
            RefreshThePersons();
            PersonDataSource.PersonCreatedEvent += RefreshThePersons;
            PersonDataSource.PersonUpdatedEvent += RefreshThePersons;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            PersonDataSource.PersonCreatedEvent -= RefreshThePersons;
            PersonDataSource.PersonUpdatedEvent -= RefreshThePersons;
            base.OnNavigatedFrom(e);
        }

        public void NotifyUser()
        {
            ErrorTB.Visibility = Visibility.Visible;
        }

        public async void RefreshThePersons()
        {
            SupplierASB.Text = "";
            NoResults.Visibility = Visibility.Collapsed;
            SupplierDetails.Visibility = Visibility.Collapsed;
            var persons = await PersonDataSource.RetrievePersonsAsync(new SupplierFilterCriteriaDTO()
            {
                EntityType = this.EntityType,
                WalletAmount = null,
                SupplierId = null,
            });
            if (persons != null)
                this._Persons = persons.Select(s => new PersonASBViewModel(s)).ToList();
        }

        /// <summary>
        /// This event gets fired anytime the text in the TextBox gets updated.
        /// It is recommended to check the reason for the text changing by checking against args.Reason
        /// </summary>
        /// <param name="sender">The AutoSuggestBox whose text got changed.</param>
        /// <param name="args">The event arguments.</param>
        private void _PersonASB_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (this._Persons == null)
                return;
            // We only want to get results when it was a user typing, 
            // otherwise we assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var matchingWholeSellers = _GetMatchingPersons(sender.Text);
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
        private void _PersonASB_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (this._Persons == null)
                return;

            if (args.ChosenSuggestion != null)
            {
                // User selected an item, take an action on it here
                var choosenSupplier = args.ChosenSuggestion;
                _SelectPerson((PersonASBViewModel)choosenSupplier);
            }
            else
            {
                PersonASBViewModel matchingSupplier = null;
                // if a text is present, find best possible match.
                if (args.QueryText != "")
                    matchingSupplier = (_GetMatchingPersons(args.QueryText)).FirstOrDefault();
                _SelectPerson(matchingSupplier);
            }
        }

        private async void _SelectPerson(PersonASBViewModel person)
        {
            if (person != null)
            {
                var retrievedPersons = await PersonDataSource.RetrieveThePersonAsync(person.PersonId);
                if (retrievedPersons == null)
                    return;
                _selectePersonInASB = new PersonASBViewModel(retrievedPersons);
                NoResults.Visibility = Visibility.Collapsed;
                SupplierDetails.Visibility = Visibility.Visible;
                WholeSellerMobNo.Text = _selectePersonInASB.MobileNo;
                WholeSellerName.Text = _selectePersonInASB.Name;
                WholeSellerAddress.Text = _selectePersonInASB.Address != null ? _selectePersonInASB.Address : "";
                WholeSellerWalletBalance.Text = Utility.ConvertToRupee(_selectePersonInASB.WalletBalance);
                SupplierGlyph.Text = Utility.GetGlyphValue(_selectePersonInASB.Name);
                GSTIN.Text = "GSTIN: " + _selectePersonInASB.GSTIN;
                NetWorth.Text = "Net Worth: " + Utility.ConvertToRupee(_selectePersonInASB.NetWorth);
                ErrorTB.Visibility = Visibility.Collapsed;
            }
            else
            {
                _selectePersonInASB = null;
                NoResults.Visibility = Visibility.Visible;
                SupplierDetails.Visibility = Visibility.Collapsed;
            }
            SelectedPersonChangedEvent?.Invoke();
        }

        /// <summary>
        /// Do a fuzzy search on all Product and order results based on a pre-defined rule set
        /// </summary>
        /// <param name="query">The part of the name or company to look for</param>
        /// <returns>An ordered list of mobileNumber that matches the query</returns>
        private List<PersonASBViewModel> _GetMatchingPersons(string query)
        {
            if (this._Persons == null)
                return null;
            return this._Persons
                .Where(item => item.MobileNo.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1
                            || item.Name?.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .OrderByDescending(item => item.MobileNo.StartsWith(query, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }
    }
}
