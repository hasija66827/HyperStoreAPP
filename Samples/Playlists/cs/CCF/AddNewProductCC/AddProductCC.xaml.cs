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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddProductCC : Page
    {
        public AddProductViewModel addProductViewModelBase;
        public AddProductCC()
        {
            this.addProductViewModelBase = new AddProductViewModel();
            this.InitializeComponent();
        }

        private void ProductCodeTB_LostFocus(object sender, RoutedEventArgs e)
        {
            Utility.CheckIfValidProductCode(ProductCodeTB.Text);
        }
        private void ProductNameTB_LostFocus(object sender, RoutedEventArgs e)
        {
            Utility.CheckIfValidProductName(ProductNameTB.Text);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Utility.CheckIfValidProductCode(ProductCodeTB.Text) && Utility.CheckIfValidProductName(ProductNameTB.Text))
            {
                if (ProductDataSource.AddProduct(addProductViewModelBase) == true)
                    MainPage.Current.NotifyUser("The product was created succesfully", NotifyType.StatusMessage);
            }
            this.Frame.Navigate(typeof(BlankPage));
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage));
        }
    }
}
