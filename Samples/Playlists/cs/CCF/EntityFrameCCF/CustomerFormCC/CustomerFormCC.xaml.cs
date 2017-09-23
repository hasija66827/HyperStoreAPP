using Models;
using SDKTemplate.DTO;
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
    enum FormMode
    {
        Create,
        Update,
    };
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerFormCC : Page
    {
        public static CustomerFormCC Current;
        private CustomerFormViewModel _CFV { get; set; }
        private FormMode? _FormMode { get; set; }
        public CustomerFormCC()
        {
            Current = this;
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _CFV = DataContext as CustomerFormViewModel;
            _CFV.ErrorsChanged += AddCustomerViewModel_ErrorsChanged;
            if (e.Parameter != null)
            {
                var customer = (TCustomer)e.Parameter;
                _CFV.CustomerId = customer.CustomerId;
                _CFV.Address = customer.Address;
                _CFV.GSTIN = customer.GSTIN;
                _CFV.MobileNo = customer.MobileNo;
                _CFV.Name = customer.Name;
                _FormMode = FormMode.Update;
                SaveBtn.Content = "Update";
            }
            else
            {
                _FormMode = FormMode.Create;
                SaveBtn.Content = "Create";
            }
            base.OnNavigatedTo(e);
        }


        private void AddCustomerViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            //ErrorList.ItemsSource = addWholeSellerViewModel.Errors.Errors.Values.SelectMany(x => x);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage));
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = this._CFV.ValidateProperties();
            if (IsValid)
            {
                CustomerDTO customerDTO = new CustomerDTO()
                {
                    Address = this._CFV.Address,
                    GSTIN = this._CFV.GSTIN,
                    MobileNo = this._CFV.MobileNo,
                    Name = this._CFV.Name,
                };
                if (_FormMode == FormMode.Create)
                {
                    var customer = await CustomerDataSource.CreateNewCustomerAsync(customerDTO);
                }
                else if (_FormMode == FormMode.Update)
                {
                    var customer = await CustomerDataSource.UpdateCustomerAsync((Guid)this._CFV.CustomerId, customerDTO);
                }
                else
                { throw new NotImplementedException(); }
            }
        }
    }
}
