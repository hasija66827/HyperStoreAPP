using HyperStoreServiceAPP.DTO;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public delegate Task FilterProductCriteriaChangedDelegate();

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FilterProductCC : Page
    {
        public event FilterProductCriteriaChangedDelegate FilterProductCriteriaChangedEvent;
        public static FilterProductCC Current;
        public FilterProductQDT ProductFilterQDT { get { return GetCurrentState(); } }
        public FilterProductCC()
        {
            Current = this;
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            DiscountPerRangeSlider.DragCompletedEvent += InvokeFilterProductCriteriaChangedEvent;
            QuantityRangeSlider.DragCompletedEvent += InvokeFilterProductCriteriaChangedEvent;
            ConsumptionDayRangeSlider.DragCompletedEvent += InvokeFilterProductCriteriaChangedEvent;
            ShowInventoryItemChkBox.Click += ShowInventoryItem_Click;
            var productMetadata = await ProductDataSource.RetrieveProductMetadataAsync();
            if (productMetadata != null)
            {
                InitializeDiscountRangeSlider(productMetadata.DiscountPerRange);
                InitializeQuantityRangeSlider(productMetadata.QuantityRange);
                InitializeConsumptionDayRangeSlider(new IRange<int?>(-1, 30));
                ShowInventoryItemChkBox.IsChecked = true;
            }
        }

        private void ShowInventoryItem_Click(object sender, RoutedEventArgs e)
        {
            InvokeFilterProductCriteriaChangedEvent(sender);
        }

        private void InitializeConsumptionDayRangeSlider(IRange<int?> consumptionDayRange)
        {
            ConsumptionDayRangeSlider.Maximum = (double)consumptionDayRange.UB + 1;
            ConsumptionDayRangeSlider.RangeMax = ConsumptionDayRangeSlider.Maximum;
            ConsumptionDayRangeSlider.Minimum = (double)consumptionDayRange.LB;
            ConsumptionDayRangeSlider.RangeMin = ConsumptionDayRangeSlider.Minimum;
        }

        private void InitializeDiscountRangeSlider(IRange<decimal?> discountPerRange)
        {
            DiscountPerRangeSlider.Maximum = (double)discountPerRange.UB + 1;
            DiscountPerRangeSlider.RangeMax = DiscountPerRangeSlider.Maximum;
            DiscountPerRangeSlider.Minimum = (double)discountPerRange.LB;
            DiscountPerRangeSlider.RangeMin = DiscountPerRangeSlider.Minimum;
        }

        private void InitializeQuantityRangeSlider(IRange<float?> quantityRange)
        {
            QuantityRangeSlider.Maximum = (double)quantityRange.UB + 1;
            QuantityRangeSlider.RangeMax = QuantityRangeSlider.Maximum;
            QuantityRangeSlider.Minimum = (double)quantityRange.LB;
            QuantityRangeSlider.RangeMin = QuantityRangeSlider.Minimum;
        }

        private FilterProductQDT GetCurrentState()
        {
            var discounPerRange = new IRange<decimal?>(Convert.ToDecimal(DiscountPerRangeSlider.RangeMin), Convert.ToDecimal(DiscountPerRangeSlider.RangeMax));
            var quantityRange = new IRange<float?>(Convert.ToSingle(QuantityRangeSlider.RangeMin), Convert.ToSingle(QuantityRangeSlider.RangeMax));
            var consumptionDayRange = new IRange<int?>(Convert.ToInt32(ConsumptionDayRangeSlider.RangeMin), Convert.ToInt32(ConsumptionDayRangeSlider.RangeMax));
            var productFilterQDT = new FilterProductQDT()
            {
                DiscountPerRange = discounPerRange,
                QuantityRange = quantityRange,
                ConsumptionDayRange = consumptionDayRange,
                ShowInventoryProductsOnly = ShowInventoryItemChkBox.IsChecked
            };
            return productFilterQDT;
        }

        private void InvokeFilterProductCriteriaChangedEvent(object sender)
        {
            try
            {
                FilterProductCriteriaChangedEvent?.Invoke();
            }
            catch (Exception e)
            {
                MainPage.Current.NotifyUser("Invalid Filter Criteria", NotifyType.ErrorMessage);
            }
        }
    }
}
