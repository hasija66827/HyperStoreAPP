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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewTransactionCC : Page
    {
        private NewTransactionViewModel _NTV { get; set; }
        public NewTransactionCC()
        {
            this.InitializeComponent();
           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var selectedPerson = (Person)e.Parameter;
            _NTV = DataContext as NewTransactionViewModel;
            _NTV.ErrorsChanged += _SNTV_ErrorsChanged;
            _NTV.Person = selectedPerson;
            _NTV.Amount = null;
        }

        private void _SNTV_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
           
        }

        private void ProceedBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = _NTV.ValidateProperties();
            if (IsValid)
                this.Frame.Navigate(typeof(TransactionConfirmationCC), this._NTV);
        }
    }
}
