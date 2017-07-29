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
using SDKTemp.ViewModel;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddCustomerCC : Page
    {
        private AddCustomerViewModel addCustomerViewModel;
        public AddCustomerCC()
        {
            this.InitializeComponent();
            addCustomerViewModel = new AddCustomerViewModel();
            NameTB.LostFocus += NameTB_LostFocus;
            MobileNoTB.LostFocus += MobileNoTB_LostFocus;
        }

        private void NameTB_LostFocus(object sender, RoutedEventArgs e)
        {
            Utility.CheckIfValidName(NameTB.Text, Person.Customer);
        }

        private void MobileNoTB_LostFocus(object sender, RoutedEventArgs e)
        {
            Utility.CheckIfUniqueMobileNumber(MobileNoTB.Text, Person.Customer);
        }


        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Utility.CheckIfValidName(NameTB.Text, Person.Customer) && Utility.CheckIfUniqueMobileNumber(MobileNoTB.Text, Person.Customer))
            {
                try
                {
                    CustomerDataSource.CreateNewCustomer((CustomerViewModel)addCustomerViewModel);
                    MainPage.Current.NotifyUser("New customer was added succesfully ", NotifyType.StatusMessage);
                }
                catch
                {
                    MainPage.Current.NotifyUser("New customer was not added succesfully ", NotifyType.ErrorMessage);
                }
            }
            this.Frame.Navigate(typeof(BlankPage));
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage));
        }
    }
}
