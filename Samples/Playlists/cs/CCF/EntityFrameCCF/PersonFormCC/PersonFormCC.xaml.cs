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
        private PersonFormViewModel _PFV { get; set; }
        private FormMode _FormMode { get; set; }
        public PersonFormCC()
        {
            this.InitializeComponent();
            _PFV = new PersonFormViewModel();
            Current = this;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._EntityType = (EntityType)e.Parameter;
            _PFV = DataContext as PersonFormViewModel;
            _PFV.ErrorsChanged += AddSupplierViewModel_ErrorsChanged;
            if (e.Parameter == null)
            {
                var person = (Person)e.Parameter;
                _PFV.SupplierId = person.PersonId;
                _PFV.Address = person.Address;
                _PFV.GSTIN = person.GSTIN;
                _PFV.MobileNo = person.MobileNo;
                _PFV.Name = person.Name;
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
            var IsValid = _PFV.ValidateProperties();
            if (IsValid)
            {
                SupplierDTO supplierDTO = new SupplierDTO()
                {
                    EntityType = this._EntityType,
                    Address = _PFV.Address,
                    GSTIN = _PFV.GSTIN,
                    MobileNo = _PFV.MobileNo,
                    Name = _PFV.Name
                };
                Person supplier = null;
                if (_FormMode == FormMode.Create)
                {
                    supplier = await PersonDataSource.CreateNewPerson(supplierDTO);
                }
                else if (_FormMode == FormMode.Update)
                {
                    supplier = await PersonDataSource.UpdateSupplierAsync((Guid)_PFV.SupplierId, supplierDTO);
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
