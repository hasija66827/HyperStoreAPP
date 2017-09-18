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
        public ProductFilterQDTDTO ProductFilterQDT;
        public FilterProductCC()
        {
            Current = this;
            this.InitializeComponent();
            this.ProductFilterQDT = null;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            DiscountPerRangeSlider.DragCompletedEvent += InvokeFilterProductCriteriaChangedEvent;
            QuantityRangeSlider.DragCompletedEvent += InvokeFilterProductCriteriaChangedEvent;
            ShowDeficientItemsOnly.Click += ShowDeficientItemsOnly_Click;
            var productMetadata = await ProductDataSource.RetrieveProductMetadataAsync();
            if (productMetadata != null)
            {
                InitializeDiscountRangeSlider(productMetadata.DiscountPerRange);
                InitializeQuantityRangeSlider(productMetadata.QuantityRange);
            }
            this.ProductFilterQDT = GetCurrentState();
        }

        private void ShowDeficientItemsOnly_Click(object sender, RoutedEventArgs e)
        {
            InvokeFilterProductCriteriaChangedEvent(sender);
        }

        private void InitializeDiscountRangeSlider(IRange<decimal?> discountPerRange)
        {
            DiscountPerRangeSlider.Maximum = (double)discountPerRange.UB + 1;
            DiscountPerRangeSlider.RangeMax = DiscountPerRangeSlider.Maximum;
            DiscountPerRangeSlider.Minimum = (double)discountPerRange.LB;
            DiscountPerRangeSlider.RangeMin = DiscountPerRangeSlider.Minimum;
        }

        private void InitializeQuantityRangeSlider(IRange<decimal> quantityRange)
        {
            QuantityRangeSlider.Maximum = (double)quantityRange.UB + 1;
            QuantityRangeSlider.RangeMax = QuantityRangeSlider.Maximum;
            QuantityRangeSlider.Minimum = (double)quantityRange.LB;
            QuantityRangeSlider.RangeMin = QuantityRangeSlider.Minimum;
        }

        private ProductFilterQDTDTO GetCurrentState()
        {
            IRange<decimal?> discounPerRange = new IRange<decimal?>(Convert.ToDecimal(DiscountPerRangeSlider.RangeMin), Convert.ToDecimal(DiscountPerRangeSlider.RangeMax));
            IRange<decimal?> quantityRange = new IRange<decimal?>(Convert.ToDecimal(QuantityRangeSlider.RangeMin), Convert.ToDecimal(QuantityRangeSlider.RangeMax));
            var productFilterQDT = new ProductFilterQDTDTO()
            {
                DiscountPerRange = discounPerRange,
                QuantityRange = quantityRange,
                IncludeDeficientItemsOnly = ShowDeficientItemsOnly.IsChecked
            };
            return productFilterQDT;
        }

        private void InvokeFilterProductCriteriaChangedEvent(object sender)
        {
            try
            {
                this.ProductFilterQDT = GetCurrentState();
                FilterProductCriteriaChangedEvent?.Invoke();
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
                MainPage.Current.NotifyUser("Invalid Filter Criteria", NotifyType.ErrorMessage);
            }
        }
    }
}
