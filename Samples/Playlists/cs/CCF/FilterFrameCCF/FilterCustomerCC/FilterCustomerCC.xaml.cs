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
    public delegate void FilterCustomerCCChangedDelegate();

    public class FilterCustomerCriteria
    {
        public IRange<float> WalletBalance { get; set; }
        public FilterCustomerCriteria(IRange<float> walletBalance)
        {
            this.WalletBalance = walletBalance;
        }
        public FilterCustomerCriteria() { }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FilterCustomerCC : Page
    {
        public event FilterCustomerCCChangedDelegate FilterCustomerCChangedEvent;
        public FilterCustomerCriteria FilterCustomerCriteria;
        public static FilterCustomerCC Current;

        public FilterCustomerCC()
        {
            Current = this;
            this.InitializeComponent();
            this.FilterCustomerCriteria = new FilterCustomerCriteria();
            WalletRangeSlider.DragCompletedEvent += WalletRangeSlider_DragCompletedEvent;
            WalletRangeSlider.RangeMin = CustomerDataSource.GetMinimumWalletBalance()-10;
            WalletRangeSlider.RangeMax = CustomerDataSource.GetMaximumWalletBalance()+10;
            WalletRangeSlider.Minimum = WalletRangeSlider.RangeMin;
            WalletRangeSlider.Maximum = WalletRangeSlider.RangeMax;
        }

        private void WalletRangeSlider_DragCompletedEvent(object sender)
        {
            IRange<float> walletBalance = new IRange<float>(Convert.ToSingle(WalletRangeSlider.RangeMin), Convert.ToSingle(WalletRangeSlider.RangeMax));
            this.FilterCustomerCriteria.WalletBalance = walletBalance;
            FilterCustomerCChangedEvent?.Invoke();
        }
    }
}
