//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Media.Playlists;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using DatabaseModel;
using MasterDetailApp.ViewModel;
using MasterDetailApp.Data;
namespace SDKTemplate
{
    public sealed partial class BillingScenario : Page
    {
        private MainPage rootPage = MainPage.Current;
        public BillingViewModel BillingViewModel { get; set; }
        public static CustomerViewModel CustomerViewModel;
        private static ProductASBViewModel _selectedProductInASB;
        public BillingScenario()
        {
            this.InitializeComponent();
            this.BillingViewModel = new BillingViewModel();
            this.DateTimeLbl.Text = DateTime.Now.ToString();
            ProductDataSource.RetrieveProductDataAsync();
            CustomerDataSource.RetrieveCustomersAsync();
            AddToCart.Click += AddToCart_Click;
            CustomerMobileNumber.LostFocus += CustomerMobileNumber_LostFocus;
            AddCustomer.Click += AddCustomerClick;
            Checkout.Click += Checkout_Click;
            _selectedProductInASB = null;
            CustomerViewModel = null;
        }
    }
}
