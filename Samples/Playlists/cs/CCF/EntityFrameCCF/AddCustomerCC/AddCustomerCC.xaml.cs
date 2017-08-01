﻿using System;
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
    public sealed partial class AddCustomerCC : Page
    {
        private AddCustomerViewModel addCustomerViewModel;
        public AddCustomerCC()
        {
            this.InitializeComponent();
            addCustomerViewModel = new AddCustomerViewModel();// TODO: uncomment line, as it value changes when below reload fucntion is called.
            Loaded += AddCustomerCCPage_Loaded;
        }

        private void AddCustomerCCPage_Loaded(object sender, RoutedEventArgs e)
        {
            addCustomerViewModel = DataContext as AddCustomerViewModel;
            addCustomerViewModel.ErrorsChanged += AddCustomerViewModel_ErrorsChanged;
        }

        private void AddCustomerViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            //ErrorList.ItemsSource = addWholeSellerViewModel.Errors.Errors.Values.SelectMany(x => x);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage));
        }
    }
}
