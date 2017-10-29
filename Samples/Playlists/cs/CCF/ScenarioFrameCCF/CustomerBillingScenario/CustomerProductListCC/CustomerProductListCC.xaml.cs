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
using System.Collections.ObjectModel;
using SDKTemplate.DTO;
using Models;
using SDKTemplate.CCF.ScenarioFrameCCF.SupplierBillingScenario;

namespace SDKTemplate
{
    public delegate void ProductQtyUpdatedDelegate();
    public delegate void NewProductAddedDelegate(Product product);

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
            CheckoutBtn.Click += Checkout_Click;
        }

        /// <summary>
        /// Adds the product into @Products and raise the list update event indirectly.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private void _AddProductToCart(Product product)
        {
            var customerProduct = new CustomerBillingProductViewModel(product);
            int index = 0;
            var existingProduct = this._Products.Where(p => p.ProductId == product.ProductId).FirstOrDefault();
            if (existingProduct != null)
            {
                index = this._Products.IndexOf(existingProduct);
                this._Products[index].QuantityPurchased += 1;//Event will be triggered.
            }
            else
            {
                this._Products.Add(customerProduct);
                index = this._Products.IndexOf(customerProduct);
                this._Products[index].QuantityPurchased = 1;//Event will be triggered.
                this.NewProductAddedIntoListEvent?.Invoke(customerProduct);
            }
        }

        public void InvokeProductListChangedEvent()
        {
            this.ProductQtyUpdatedEvent?.Invoke();
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (PersonASBCC.Current.SelectedPersonInASB == null)
            {
                PersonASBCC.Current.NotifyUser();
                return;
            }
            var selectedCustomer = PersonASBCC.Current.SelectedPersonInASB;
            var billSummary = BillingSummaryCC.Current.BillingSummaryViewModel;
            CustomerPageNavigationParameter customerNavigationParameter = new CustomerPageNavigationParameter()
            {
                ProductsPurchased = Products,
                SelectedCustomer = selectedCustomer,
                BillingSummaryViewModel = billSummary,
            };
            var navigationParameter = new PageNavigationParameter(OrderType.CustomerOrder, customerNavigationParameter, null);
            this.Frame.Navigate(typeof(PayNowCC), navigationParameter);
        }
    }
}