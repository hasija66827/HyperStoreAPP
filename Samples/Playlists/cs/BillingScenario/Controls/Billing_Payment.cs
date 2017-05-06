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
namespace SDKTemplate
{
    public sealed partial class BillingScenario : Page
    {
        private void PayNow_Click(object sender, RoutedEventArgs e)
        {
            var db = new RetailerContext();
            var customerOrder = new CustomerOrder(this._customer.CustomerId, 
                this.BillingViewModel.BillAmount, 
                this.BillingViewModel.BillAmount,
                this._customer.WalletBalance);
            // Creating Entity Record in customerOrder.
            db.CustomerOrders.Add(customerOrder);
            db.SaveChanges();
            foreach (var product in BillingViewModel.Products)
            {
                // Adding each product purchased in the order into the Entity CustomerOrderProduct.
                var customerOrderProduct = new CustomerOrderProduct(customerOrder.CustomerOrderId, 
                    product.ProductId, 
                    product.DiscountPer,
                    product.DisplayPrice, 
                    product.QuantityPurchased);
                db.CustomerOrderProducts.Add(customerOrderProduct);
                // TODO: Update the product entity with new total quantity.
                
            }
            // Saving the order.
            db.SaveChanges();
        }

        private void PayLater_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SelectPaymentMode));
        }
    }
}
