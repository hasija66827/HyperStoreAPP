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
    enum Person
    {
        WholeSeller,
        Customer
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddWholeSellerCC : Page
    {
        public AddWholeSellerViewModel addWholeSellerViewModel;
        public AddWholeSellerCC()
        {
            this.InitializeComponent();
            addWholeSellerViewModel = new AddWholeSellerViewModel();
            NameTB.LostFocus += NameTB_LostFocus;
            MobileNoTB.LostFocus += MobileNoTB_LostFocus;
        }

        private void NameTB_LostFocus(object sender, RoutedEventArgs e)
        {
            Utility.CheckIfValidName(NameTB.Text, Person.WholeSeller);
        }

        private void MobileNoTB_LostFocus(object sender, RoutedEventArgs e)
        {
            Utility.CheckIfValidMobileNumber(MobileNoTB.Text, Person.WholeSeller);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Utility.CheckIfValidName(NameTB.Text, Person.WholeSeller) && Utility.CheckIfValidMobileNumber(MobileNoTB.Text, Person.WholeSeller))
            {
                try
                {
                    WholeSellerDataSource.CreateWholeSeller(addWholeSellerViewModel);
                    MainPage.Current.NotifyUser("New wholesller was added succesfully ", NotifyType.StatusMessage);
                }
                catch
                {
                    MainPage.Current.NotifyUser("New wholeseller was not added succesfully ", NotifyType.ErrorMessage);
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
