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
    enum Person
    {
        Supplier,
        Customer
    }
     /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SupplierFormCC : Page
    {
        public static SupplierFormCC Current;
        private SupplierFormViewModel _SFV { get; set; }
        private FormMode _FormMode { get; set; }
        public SupplierFormCC()
        {
            this.InitializeComponent();
            _SFV = new SupplierFormViewModel();
            Current = this;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _SFV = DataContext as SupplierFormViewModel;
            _SFV.ErrorsChanged += AddSupplierViewModel_ErrorsChanged;
            if (e.Parameter != null)
            {
                var supplier = (TSupplier)e.Parameter;
                _SFV.SupplierId = supplier.SupplierId;
                _SFV.Address = supplier.Address;
                _SFV.GSTIN = supplier.GSTIN;
                _SFV.MobileNo = supplier.MobileNo;
                _SFV.Name = supplier.Name;
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

        private void AddSupplierViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            //ErrorList.ItemsSource = addWholeSellerViewModel.Errors.Errors.Values.SelectMany(x => x);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage));
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = _SFV.ValidateProperties();
            if (IsValid)
            {
                SupplierDTO supplierDTO = new SupplierDTO()
                {
                    Address = _SFV.Address,
                    GSTIN = _SFV.GSTIN,
                    MobileNo = _SFV.MobileNo,
                    Name = _SFV.Name
                };
                if (_FormMode == FormMode.Create)
                {
                    var supplier = await SupplierDataSource.CreateNewSupplier(supplierDTO);
                }
                else if (_FormMode == FormMode.Update)
                {
                    var supplier = await SupplierDataSource.UpdateSupplierAsync((Guid)_SFV.SupplierId, supplierDTO);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
