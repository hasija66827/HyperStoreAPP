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
        private void Verify_Click(object sender, RoutedEventArgs e)
        {
            //Open a Dialogue for OTP.
            //If Succesfully Verified.
            // Ask for Customer Name and address.
            // Create a new customer and save the Customer.
            using (var db = new RetailerContext())
            {
                var mobileNumber = CustomerMobNoTB.Text;
                Customer customer = new Customer("abc" + mobileNumber, mobileNumber);
                db.Customers.Add(customer);
                db.SaveChanges();
                ShowVerified(customer.Name, customer.WalletBalance);
            }
        }

        private void CustomerMobileNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            var mobileNumber = CustomerMobNoTB.Text;
            //TODO: Verify the input charecter

            using (var db = new RetailerContext())
            {
                // Check If c.Text exist in customer database
                var customers = db.Customers.Where(c => c.MobileNo.Equals(mobileNumber));
                if (customers.Count() == 0)
                {
                    ShowUnverified();
                }
                else
                {
                    var customer = customers.First();
                    ShowVerified(customer.Name, customer.WalletBalance);
                }
            }
        }
        public void ShowVerified(string customerName, float customerWalletBalance)
        {
            Verify.Visibility = Visibility.Collapsed;
            IsVerified.Visibility = Visibility.Visible;
            CustomerNameTB.Text = customerName;
            CustomerWalletBalanceTB.Text = "\u20b9" + customerWalletBalance;
        }
        public void ShowUnverified()
        {
            Verify.Visibility = Visibility.Visible;
            IsVerified.Visibility = Visibility.Collapsed;
            CustomerNameTB.Text = "";
            CustomerWalletBalanceTB.Text = "\u20b9" + "0";
        }
    }
}
