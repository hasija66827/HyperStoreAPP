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
    public sealed partial class PersonFormCC : Page
    {
        public static PersonFormCC Current;
        private EntityType _EntityType { get; set; }
        private PersonFormViewModel _SFV { get; set; }
        private FormMode _FormMode { get; set; }
        public PersonFormCC()
        {
            this.InitializeComponent();
            _SFV = new PersonFormViewModel();
            Current = this;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._EntityType = (EntityType)e.Parameter;
            _SFV = DataContext as PersonFormViewModel;
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
            MainPage.Current.CloseSplitPane();
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = _SFV.ValidateProperties();
            if (IsValid)
            {
                SupplierDTO supplierDTO = new SupplierDTO()
                {
                    EntityType = this._EntityType,
                    Address = _SFV.Address,
                    GSTIN = _SFV.GSTIN,
                    MobileNo = _SFV.MobileNo,
                    Name = _SFV.Name
                };
                TSupplier supplier = null;
                if (_FormMode == FormMode.Create)
                {
                    supplier = await PersonDataSource.CreateNewPerson(supplierDTO);
                }
                else if (_FormMode == FormMode.Update)
                {
                    supplier = await PersonDataSource.UpdateSupplierAsync((Guid)_SFV.SupplierId, supplierDTO);
                }
                else
                {
                    throw new NotImplementedException();
                }
                if (supplier != null)
                    MainPage.Current.CloseSplitPane();
            }
        }
    }
}
