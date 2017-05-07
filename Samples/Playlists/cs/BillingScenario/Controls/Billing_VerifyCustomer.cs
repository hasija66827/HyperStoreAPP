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
        private void AddCustomerClick(object sender, RoutedEventArgs e)
        {
            //TODO: Ask for Customer Name and address.
            // Create a new customer and save the Customer.
            var mobileNumber = CustomerMobNoTB.Text;
            CustomerViewModel customer = new CustomerViewModel(mobileNumber);
            CustomerDataSource.AddCustomer(customer);
            // Setting the customer for the Billing.
            this._customer = customer;
            HideAddCustomer();
        }

        private void CustomerMobileNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            var mobileNumber = CustomerMobNoTB.Text;
            // Verify the Input MobileNumber
            if (!Utility.IsMobileNumber(mobileNumber))
            {
                CustomerMobNoTB.Text = "";
                DisplayAddCustomer();
                AddCustomer.Visibility = Visibility.Collapsed;
                // Setting the empty customer
                this._customer = new CustomerViewModel();
                //TODO: Show Notification Message
                return;
            }
            // Check If c.Text exist in customer database
            var customer = CustomerDataSource.GetCustomerByMobileNumber(mobileNumber);
            if (customer == null)
            {
                // Setting the empty customer
                this._customer = new CustomerViewModel();
                DisplayAddCustomer();
            }
            else
            {
                // Setting the customer of the order.
                this._customer = customer;
                HideAddCustomer();
            }
        }
        public void HideAddCustomer()
        {
            AddCustomer.Visibility = Visibility.Collapsed;
            IsVerified.Visibility = Visibility.Visible;
            CustomerNameTB.Text = this._customer.Name;
            CustomerWalletBalanceTB.Text = "\u20b9" + this._customer.WalletBalance;
        }
        public void DisplayAddCustomer()
        {
            AddCustomer.Visibility = Visibility.Visible;
            IsVerified.Visibility = Visibility.Collapsed;
            CustomerNameTB.Text = "";
            CustomerWalletBalanceTB.Text = "\u20b9" + "0";
        }
    }
}
