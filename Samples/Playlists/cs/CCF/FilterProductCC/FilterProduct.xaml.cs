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
    public delegate Int32 FilterProductCriteriaChangedDelegate(FilterProductCriteria filterProuductCriteria);

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FilterProductCC : Page
    {
        public event FilterProductCriteriaChangedDelegate FilterProductCriteriaChangedEvent;
        public static FilterProductCC Current;
       
        public FilterProductCC()
        {
            Current = this;
            this.InitializeComponent();
            DiscountPerRangeSlider.DragCompletedEvent += InvokeFilterProductCriteriaChangedEvent;
            QuantityRangeSlider.DragCompletedEvent += InvokeFilterProductCriteriaChangedEvent;
            ShowDeficientItemsOnly.Click += ShowDeficientItemsOnly_Click;
        }

        private void ShowDeficientItemsOnly_Click(object sender, RoutedEventArgs e)
        {
            InvokeFilterProductCriteriaChangedEvent(sender);
        }

        private void InvokeFilterProductCriteriaChangedEvent(object sender)
        {
            try
            {
                IRange<float> discounPerRange = new IRange<float>(Convert.ToSingle(DiscountPerLB.Text), Convert.ToSingle(DiscountPerUB.Text));
                IRange<Int32> quantityRange = new IRange<Int32>(Convert.ToInt32(QuantityLB.Text), Convert.ToInt32(QuantityUB.Text));
                FilterProductCriteria filterProductCriteria = new FilterProductCriteria(discounPerRange, quantityRange, ShowDeficientItemsOnly.IsChecked);
                FilterProductCriteriaChangedEvent?.Invoke(filterProductCriteria);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
                MainPage.Current.NotifyUser("Invalid Filter Criteria", NotifyType.ErrorMessage);
            }
        }
    }
}
