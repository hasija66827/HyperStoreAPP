﻿//*********************************************************
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
 
using SDKTemp.Data;
using System.Collections.ObjectModel;
using SDKTemplate.DTO;
using Models;

namespace SDKTemplate
{
    public delegate void ProductQtyUpdatedDelegate();
    public delegate void NewProductAddedDelegate(TProduct product);

    public sealed partial class CustomerProductListCC : Page
    {
        public static CustomerProductListCC Current;
        public event ProductQtyUpdatedDelegate ProductQtyUpdatedEvent;
        public event NewProductAddedDelegate NewProductAddedIntoListEvent;
        public List<CustomerBillingProductViewModelBase> Products { get { return this._Products.Cast<CustomerBillingProductViewModelBase>().ToList(); } }
        private ObservableCollection<CustomerBillingProductViewModel> _Products { get; set; }

        public CustomerProductListCC()
        {
            Current = this;
            this.InitializeComponent();
            ProductASBCC.Current.OnAddProductClickedEvent += new OnAddProductClickedDelegate(this._AddProductToCart);
            _Products = new ObservableCollection<CustomerBillingProductViewModel>();
            Checkout.Click += Checkout_Click;
        }

        /// <summary>
        /// Adds the product into @Products and raise the list update event indirectly.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private void _AddProductToCart(TProduct product)
        {
            var customerProduct = new CustomerBillingProductViewModel(product);
            int index = 0;
            var existingProduct = this._Products.Where(p => p.ProductId == product.ProductId).FirstOrDefault();
            if (existingProduct != null)
            {
                index = this._Products.IndexOf(existingProduct);
                this._Products[index].QuantityConsumed += 1;//Event will be triggered.
            }
            else
            {
                this._Products.Add(customerProduct);
                index = this._Products.IndexOf(customerProduct);
                this._Products[index].QuantityConsumed = 1;//Event will be triggered.
                this.NewProductAddedIntoListEvent?.Invoke(customerProduct);
            }   
        }

        public void InvokeProductListChangedEvent()
        {
            this.ProductQtyUpdatedEvent?.Invoke();
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
            CustomerPageNavigationParameter pageNavigationParameter = new CustomerPageNavigationParameter()
            {
                ProductsConsumed = Products,
                SelectedCustomer = selectedCustomer,
                BillingSummaryViewModel = billSummary,
            };
            this.Frame.Navigate(typeof(SelectPaymentMode), pageNavigationParameter);
        }
    }
}