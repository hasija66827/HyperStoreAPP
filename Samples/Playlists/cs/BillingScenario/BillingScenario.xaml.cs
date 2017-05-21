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
    public delegate void ListSizeChangedDelegate(Int32 updatedSize, float updateBillAmount);
    public sealed partial class BillingScenario : Page
    {
        private MainPage rootPage = MainPage.Current;
        public BillingViewModel BillingViewModel { get; set; }
 
        public static event ListSizeChangedDelegate ListSizeChangedEvent;
        public ProductASBCustomControl productASBCustomControl;
        public BillingScenario()
        {
            this.InitializeComponent();
            this.BillingViewModel = new BillingViewModel();
           
            ProductDataSource.RetrieveProductDataAsync();
            CustomerDataSource.RetrieveCustomersAsync();
          
            Checkout.Click += Checkout_Click;
          
            ProductASBCustomControl.AddToCartClickEvent += new AddToCartDelegate(this.AddToCart);
            BillingSummaryViewModel.AdditionalDiscountPerDiscountedBillAmountChangedEvent += new AdditionalDiscountPerDiscountedBillAmountChangedDelegate
                ((additonalDiscountPer, discountedBillAmount) =>
                {
                    this.BillingViewModel.AdditonalDiscountPer = additonalDiscountPer;
                    this.BillingViewModel.DiscountedBillAmount = discountedBillAmount;
                });
        }
        public void AddToCart(ProductViewModel product)
        {
            var index=this.BillingViewModel.AddToBillingList(product);
            ListView.SelectedIndex = index;
            BillingScenario.ListSizeChangedEvent?.Invoke(this.BillingViewModel.TotalProducts, this.BillingViewModel.TotalBillAmount);
        }
    }
}
