using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MasterDetailApp.ViewModel;
using MasterDetailApp.Data;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerInformation : Page
    {
        public static CustomerViewModel CustomerViewModel;
        public CustomerInformation()
        {
            this.InitializeComponent();
            CustomerMobileNumber.LostFocus += CustomerMobileNumber_LostFocus;
            AddCustomerBtn.Click += AddCustomerBtn_Click;
        }

        private void AddCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Ask for Customer Name and address.
            // Create a new customer and save the Customer.
            var mobileNumber = CustomerMobNoTB.Text;
            CustomerViewModel customer = new CustomerViewModel(mobileNumber);
            CustomerDataSource.AddCustomer(customer);
            // Setting the customer for the Billing.
            CustomerViewModel = customer;
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
                AddCustomerBtn.Visibility = Visibility.Collapsed;
                // Setting the null customer
                CustomerViewModel = null;
                MainPage.Current.NotifyUser("Mobile Number should be of 10 digits", NotifyType.ErrorMessage);
                return;
            }
            // Check If c.Text exist in customer database
            var customer = CustomerDataSource.GetCustomerByMobileNumber(mobileNumber);
            if (customer == null)
            {
                CustomerViewModel = null;
                DisplayAddCustomer();
            }
            else
            {
                // Setting the customer of the order.
                CustomerViewModel = customer;
                HideAddCustomer();
            }
        }
        public void HideAddCustomer()
        {
            AddCustomerBtn.Visibility = Visibility.Collapsed;
            IsVerified.Visibility = Visibility.Visible;
            CustomerNameTB.Text = CustomerViewModel.Name;
            CustomerWalletBalanceTB.Text = "\u20b9" + CustomerViewModel.WalletBalance;
        }
        public void DisplayAddCustomer()
        {
            AddCustomerBtn.Visibility = Visibility.Visible;
            IsVerified.Visibility = Visibility.Collapsed;
            CustomerNameTB.Text = "";
            CustomerWalletBalanceTB.Text = "\u20b9" + "0";
        }
    }
}
