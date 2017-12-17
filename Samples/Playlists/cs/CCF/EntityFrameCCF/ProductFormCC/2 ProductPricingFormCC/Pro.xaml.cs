using HyperStoreServiceAPP.DTO;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pro : Page
    {
        private ProViewModel _ProViewModel { get; set; }
        private ProductBasicInformation _ProdBasicInfo { get; set; }
        public Pro()
        {
            this._ProViewModel = new ProViewModel();
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _ProdBasicInfo = (ProductBasicInformation)e.Parameter;
            _ProViewModel = DataContext as ProViewModel;
            _ProViewModel.ErrorsChanged += _ProViewModel_ErrorsChanged;
            base.OnNavigatedTo(e);
        }

        private void _ProViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {

        }
        //TODO: Take carre of non invnotroy item
        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = this._ProViewModel.ValidateProperties();
            if (IsValid)
            {
                var productDTO = new ProductDTO()
                {
                    CGSTPer = Utility.TryToConvertToDecimal(_ProViewModel.CGSTPer),
                    Code = _ProdBasicInfo._PDFV.Code,
                    DiscountPer = Utility.TryToConvertToDecimal(_ProViewModel.DiscPer),
                    IsNonInventoryProduct = _ProdBasicInfo._PDFV.IsNonInventoryProduct,
                    MRP = Utility.TryToConvertToDecimal(_ProViewModel.MRP),
                    Name = _ProdBasicInfo._PDFV.Name,
                    HSN = _ProdBasicInfo._PDFV.HSN,
                    SGSTPer = Utility.TryToConvertToDecimal(_ProViewModel.SGSTPer),
                    TagIds = _ProdBasicInfo._SelectedTagIds,
                };
                var product = await ProductDataSource.CreateNewProductAsync(productDTO);
                if (product != null)
                    MainPage.Current.CloseSplitPane();
            }
        }
    }
}
