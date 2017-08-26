using Models;
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
        private SupplierFormViewModel _SFV { get; set; }
        public AddWholeSellerCC()
        {
            this.InitializeComponent();
            _SFV = new SupplierFormViewModel();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            _SFV = DataContext as SupplierFormViewModel;
            _SFV.ErrorsChanged += AddWholeSellerViewModel_ErrorsChanged; ;
        }

        private void AddWholeSellerViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
           //ErrorList.ItemsSource = addWholeSellerViewModel.Errors.Errors.Values.SelectMany(x => x);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage));
        }
    }
}
