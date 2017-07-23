﻿using System;
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
        public FilterPersonViewModel FilterPersonCriteria;
        public static FilterPersonCC Current;

        public FilterPersonCC()
        {
            Current = this;
            this.InitializeComponent();
            this.FilterPersonCriteria = new FilterPersonViewModel();
            WalletRangeSlider.DragCompletedEvent += WalletRangeSlider_DragCompletedEvent;
            
        }

        public void InitializeRangeSlider(float min, float max)
        {
            WalletRangeSlider.RangeMin = min - 10;
            WalletRangeSlider.RangeMax = max + 10;
            WalletRangeSlider.Minimum = WalletRangeSlider.RangeMin;
            WalletRangeSlider.Maximum = WalletRangeSlider.RangeMax;
        }

        private void WalletRangeSlider_DragCompletedEvent(object sender)
        {
            IRange<float> walletBalance = new IRange<float>(Convert.ToSingle(WalletRangeSlider.RangeMin), Convert.ToSingle(WalletRangeSlider.RangeMax));
            this.FilterPersonCriteria.WalletBalance = walletBalance;
            FilterPersonChangedEvent?.Invoke();
        }
    }
}
