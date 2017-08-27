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
using SDKTemp.Data;
using System.Collections.ObjectModel;
using SDKTemplate.DTO;
using Models;

namespace SDKTemplate
{
    public delegate void ProductListCCUpdatedDelegate();
    public sealed partial class CustomerProductListCC : Page
    {
        public static CustomerProductListCC Current;
        public event ProductListCCUpdatedDelegate ProductListCCUpdatedEvent;
        public List<CustomerOrderProductViewModelBase> Products { get { return this._products.Cast<CustomerOrderProductViewModelBase>().ToList(); } }
        private ObservableCollection<CustomerProductViewModel> _products;

        public CustomerProductListCC()
        {
            Current = this;
            this.InitializeComponent();
            ProductASBCC.Current.OnAddProductClickedEvent += new OnAddProductClickedDelegate(this._AddProductToCart);
            _products = new ObservableCollection<CustomerProductViewModel>();
            Checkout.Click += Checkout_Click;
        }

        /// <summary>
        /// Adds the product into @Products and raise the list update event indirectly.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private int _AddProductToCart(TProduct product)
        {
            var customerProduct = new CustomerProductViewModel(product);
            int index = 0;
            var existingProduct = this._products.Where(p => p.ProductId == product.ProductId).FirstOrDefault();
            if (existingProduct != null)
            {
                index = this._products.IndexOf(existingProduct);
                this._products[index].QuantityConsumed += 1;//Event will be triggered.
            }
            else
            {
                this._products.Add(customerProduct);
                index = this._products.IndexOf(customerProduct);
                this._products[index].QuantityConsumed = 1;//Event will be triggered.
            }
            return index;
        }

        public void InvokeProductListChangedEvent()
        {
            this.ProductListCCUpdatedEvent?.Invoke();
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerASBCC.Current.SelectedCustomerInASB == null)
            {
                MainPage.Current.NotifyUser("Customer not selected in search box", NotifyType.ErrorMessage);
                return;
            }
            var selectedCustomer = CustomerASBCC.Current.SelectedCustomerInASB;
            var billSummary = BillingSummaryCC.Current.BillingSummaryViewModel;
            PageNavigationParameter pageNavigationParameter = new PageNavigationParameter()
            {
                ProductsConsumed = Products,
                SelectedCustomer = selectedCustomer,
                BillingSummaryViewModel = billSummary,
                UseWallet = selectedCustomer?.WalletBalance == 0 ? false : true,
                WalletAmountToBePaidLater = billSummary.DiscountedBillAmount
            };
            this.Frame.Navigate(typeof(SelectPaymentMode), pageNavigationParameter);
        }
    }
}