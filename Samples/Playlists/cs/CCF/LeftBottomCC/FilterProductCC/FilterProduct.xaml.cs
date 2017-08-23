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
    public delegate void FilterProductCriteriaChangedDelegate();

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FilterProductCC : Page
    {
        public event FilterProductCriteriaChangedDelegate FilterProductCriteriaChangedEvent;
        public static FilterProductCC Current;
        public FilterProductCriteria FilterProductCriteria;
        public FilterProductCC()
        {
            Current = this;
            this.InitializeComponent();
            DiscountPerRangeSlider.DragCompletedEvent += InvokeFilterProductCriteriaChangedEvent;
            QuantityRangeSlider.DragCompletedEvent += InvokeFilterProductCriteriaChangedEvent;
            ShowDeficientItemsOnly.Click += ShowDeficientItemsOnly_Click;
            QuantityRangeSlider.Maximum = ProductDataSource.GetMaximumQuantity();
            QuantityRangeSlider.RangeMax = QuantityRangeSlider.Maximum;
        }

        private void ShowDeficientItemsOnly_Click(object sender, RoutedEventArgs e)
        {
            InvokeFilterProductCriteriaChangedEvent(sender);
        }

        private void InvokeFilterProductCriteriaChangedEvent(object sender)
        {
            try
            {
                IRange<decimal> discounPerRange = new IRange<decimal>(Convert.ToDecimal(DiscountPerLB.Text), Convert.ToDecimal(DiscountPerUB.Text));
                IRange<Int32> quantityRange = new IRange<Int32>(Convert.ToInt32(QuantityLB.Text), Convert.ToInt32(QuantityUB.Text));
                this.FilterProductCriteria = new FilterProductCriteria(discounPerRange, quantityRange, ShowDeficientItemsOnly.IsChecked);
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
