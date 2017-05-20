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
      
        public ProductASBCustomControl productASBCustomControl;
        public BillingScenario()
        {
            this.InitializeComponent();
            this.BillingViewModel = new BillingViewModel();
           
            ProductDataSource.RetrieveProductDataAsync();
            CustomerDataSource.RetrieveCustomersAsync();
          
            Checkout.Click += Checkout_Click;
          
            productASBCustomControl = new ProductASBCustomControl();
            ProductASBCustomControl.AddToCartClickEvent += new AddToCartDelegate(this.BillingViewModel.AddToCart);
        }
    }
}
