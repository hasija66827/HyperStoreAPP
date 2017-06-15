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
using SDKTemp.ViewModel;
using SDKTemp.Data;
namespace SDKTemplate
{
    public sealed partial class ProductListCC : Page
    {
        public static ProductListCC Current;
        public event ProductListChangedDelegate ProductListChangedEvent;
        public ProductListViewModel ProductListViewModel { get; set; }
        public ProductListCC()
        {
            Current = this;
            this.InitializeComponent();
            this.ProductListViewModel = new ProductListViewModel();
            this.ProductListViewModel.ProductListChangedEvent += ProductListViewModel_ProductListChangedEvent;
            Checkout.Click += Checkout_Click;
            ProductASBCC.Current.OnAddProductClickedEvent += new OnAddProductClickedDelegate(this.AddProductToCart);
            BillingSummaryViewModel.AdditionalDiscountPerChangedEvent += new AdditionalDiscountPerDiscountedBillAmountChangedDelegate
                ((sender, additonalDiscountPer) =>
                {
                    this.ProductListViewModel.AdditonalDiscountPer = additonalDiscountPer;
                });
        }

        private void ProductListViewModel_ProductListChangedEvent(object sender, int updatedSize, float updateBillAmount)
        {
            this.ProductListChangedEvent?.Invoke(this, updatedSize, updateBillAmount);
        }

        // Will be invoked on an event in ProductASBCC
        public void AddProductToCart(object sender, ProductViewModel product)
        {
            var index = this.ProductListViewModel.AddToBillingList(product);
            //ListView.SelectedIndex = index;
        }
        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            //TODO: need to changed
            if (CustomerASBCC.Current.SelectedCustomerInASB== null)
            {
                MainPage.Current.NotifyUser("Customer not added, add the customer by clicking on Add Customer", NotifyType.ErrorMessage);
                return;
            }
            if (!Utility.IsMobileNumber(CustomerASBCC.Current.SelectedCustomerInASB.MobileNo))
            {
                MainPage.Current.NotifyUser("Mobile Number is required", NotifyType.ErrorMessage);
         
                return;
            }
            PageNavigationParameter pageNavigationParameter = new PageNavigationParameter(
               this.ProductListViewModel,
               CustomerASBCC.Current.SelectedCustomerInASB);
            this.Frame.Navigate(typeof(SelectPaymentMode), pageNavigationParameter);
        }
    }
}