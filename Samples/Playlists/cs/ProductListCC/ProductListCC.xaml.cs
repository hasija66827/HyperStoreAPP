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
    public delegate void ProductListChangedDelegate(object sender, Int32 updatedSize, float updateBillAmount);
    public sealed partial class ProductListCC : Page
    {
        public static event ProductListChangedDelegate ProductListChangedEvent;
        private MainPage rootPage = MainPage.Current;
        public ProductListViewModel ProductistViewModel { get; set; }
        public ProductListCC()
        {
            this.InitializeComponent();
            this.ProductistViewModel = new ProductListViewModel();
            Checkout.Click += Checkout_Click;
            ProductASBCustomControl.OnAddProductClickedEvent += new OnAddProductClickedDelegate(this.AddProductToCart);
            BillingSummaryViewModel.AdditionalDiscountPerDiscountedBillAmountChangedEvent += new AdditionalDiscountPerDiscountedBillAmountChangedDelegate
                ((sender, additonalDiscountPer, discountedBillAmount) =>
                {
                    this.ProductistViewModel.AdditonalDiscountPer = additonalDiscountPer;
                    this.ProductistViewModel.DiscountedBillAmount = discountedBillAmount;
                });
        }
        // Will be invoked on an event in ProductASBCC
        public void AddProductToCart(object sender, ProductViewModel product)
        {
            var index = this.ProductistViewModel.AddToBillingList(product);
            ListView.SelectedIndex = index;
            ProductListCC.ProductListChangedEvent?.Invoke(this, this.ProductistViewModel.TotalProducts, this.ProductistViewModel.TotalBillAmount);
        }
        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerInformation.CustomerViewModel == null)
            {
                MainPage.Current.NotifyUser("Customer not added, add the customer by clicking on Add Customer", NotifyType.ErrorMessage);
                //TODO: got focus on the mobile textBox
                return;
            }
            if (!Utility.IsMobileNumber(CustomerInformation.CustomerViewModel.MobileNo))
            {
                MainPage.Current.NotifyUser("Mobile Number is required", NotifyType.ErrorMessage);
                //TODO: got focus on the mobile textBox
                return;
            }
            PageNavigationParameter pageNavigationParameter = new PageNavigationParameter(
               this.ProductistViewModel,
               CustomerInformation.CustomerViewModel);
            this.Frame.Navigate(typeof(SelectPaymentMode), pageNavigationParameter);
        }
    }
}

