using SDKTemplate.DTO;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FilterPersonCC : Page
    {
        public event FilterPersonChangedDelegate FilterPersonChangedEvent;

        public FilterPersonCriteriaViewModel FilterPersonCriteria { get { return this._FilterPersonCriteria; } }
        private FilterPersonCriteriaViewModel _FilterPersonCriteria;
        public static FilterPersonCC Current;

        public FilterPersonCC()
        {
            Current = this;
            this.InitializeComponent();
            this._FilterPersonCriteria = new FilterPersonCriteriaViewModel();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var person = (EntityType)e.Parameter;
            IRange<double> walletBalanceRange;
            if (person == EntityType.Supplier)
                walletBalanceRange = await PersonDataSource.RetrieveWalletRangeAsync<double>();
            else
                walletBalanceRange = await PersonDataSource.RetrieveWalletRangeAsync<double>();

            _IntitializeWalletRangeSlider(walletBalanceRange);
            WalletRangeSlider.DragCompletedEvent += WalletRangeSlider_DragCompletedEvent;
        }

        private void _IntitializeWalletRangeSlider(IRange<double> walletBalanceRange)
        {
            if (walletBalanceRange != null)
            {
                WalletRangeSlider.Maximum = (double)(walletBalanceRange.UB + 1);
                WalletRangeSlider.RangeMax = WalletRangeSlider.Maximum;
                WalletRangeSlider.Minimum = (double)(walletBalanceRange.LB);
                WalletRangeSlider.RangeMin = WalletRangeSlider.Minimum;
            }
        }

        private void WalletRangeSlider_DragCompletedEvent(object sender)
        {
            var walletBalance = new IRange<decimal>(Convert.ToDecimal(WalletRangeSlider.RangeMin), Convert.ToDecimal(WalletRangeSlider.RangeMax));
            this.FilterPersonCriteria.WalletBalance = walletBalance;
            FilterPersonChangedEvent?.Invoke();
        }
    }
}
