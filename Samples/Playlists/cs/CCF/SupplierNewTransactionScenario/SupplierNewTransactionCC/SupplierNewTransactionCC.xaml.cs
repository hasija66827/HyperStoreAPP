﻿using Models;
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
    public sealed partial class SupplierNewTransactionCC : Page
    {
        private SupplierNewTransactionViewModel _SupplierNewTransactionViewModel { get; set; }
        public SupplierNewTransactionCC()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var selectedSupplier = (TSupplier)e.Parameter;
            this._SupplierNewTransactionViewModel = new SupplierNewTransactionViewModel()
            {
                CreditAmount=0,
                Supplier=selectedSupplier
            };
        }

        private void AddMoney_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SupplierTransactionOTPVerificationCC), this._SupplierNewTransactionViewModel);
        }
    }
}
