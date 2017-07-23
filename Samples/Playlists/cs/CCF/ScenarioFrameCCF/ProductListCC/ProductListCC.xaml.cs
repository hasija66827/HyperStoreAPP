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
using System.Collections.ObjectModel;

namespace SDKTemplate
{
    public delegate void ProductListCCUpdatedDelegate(ObservableCollection<ProductViewModel> products);
    public sealed partial class ProductListCC : Page
    {
        public static ProductListCC Current;
        public event ProductListCCUpdatedDelegate ProductListCCUpdatedEvent;
        public ProductListViewModel ProductListViewModel { get; set; }
        public ProductListCC()
        {
            Current = this;
            this.InitializeComponent();
            this.ProductListViewModel = new ProductListViewModel();
            this.ProductListViewModel.ProductListViewModelChangedEvent += ProductListViewModel_ProductListChangedEvent;
            ProductASBCC.Current.OnAddProductClickedEvent += new OnAddProductClickedDelegate(this.AddProductToCart);
            Checkout.Click += Checkout_Click;
        }

        private void ProductListViewModel_ProductListChangedEvent()
        {
            this.ProductListCCUpdatedEvent?.Invoke(this.ProductListViewModel.Products);
        }

        /// <summary>
        /// Adds the product in product Billing list.
        /// Method is function on event triggered from ProductASB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="product"></param>
        public void AddProductToCart(object sender, ProductViewModel product)
        {
            var index = this.ProductListViewModel.AddToBillingList(product);
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerASBCC.Current.SelectedCustomerInASB== null)
            {
                MainPage.Current.NotifyUser("Customer not selected in search box", NotifyType.ErrorMessage);
                return;
            }
            
            PageNavigationParameter pageNavigationParameter = 
                new PageNavigationParameter(
                                             this.ProductListViewModel,
                                             CustomerASBCC.Current.SelectedCustomerInASB,
                                             BillingSummaryCC.Current.BillingSummaryViewModel);
                                             this.Frame.Navigate(typeof(SelectPaymentMode), pageNavigationParameter);
        }
    }
}