﻿using SDKTemplate.DTO;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace SDKTemplate
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductPricingFormCC : Page
    {
        public static ProductPricingFormCC Current;
        private ProductBasicInformation _ProdBasicInfo { get; set; }
        private ProductPricingDetailViewModel _PPDV { get; set; }
        public ProductPricingFormCC()
        {
            Current = this;
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _ProdBasicInfo = (ProductBasicInformation)e.Parameter;
            _PPDV = DataContext as ProductPricingDetailViewModel;
            _PPDV.ErrorsChanged += _PFV_ErrorsChanged;
            base.OnNavigatedTo(e);
        }

        private void _PFV_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = this._PPDV.ValidateProperties();
            if (IsValid)
            {
                var productDTO = new ProductDTO()
                {
                    TagIds = _ProdBasicInfo._SelectedTagIds,
                    CGSTPer = _PPDV.CGSTPer,
                    Code = _ProdBasicInfo._PDFV.Code,
                    DiscountPer = _PPDV.DiscountPer,
                    DisplayPrice = _PPDV.DisplayPrice,
                    Name = _ProdBasicInfo._PDFV.Name,
                    RefillTime = 12,//remove it
                    SGSTPer = _PPDV.SGSTPer,
                    Threshold = _ProdBasicInfo._PDFV.Threshold
                };
                var product = await ProductDataSource.CreateNewProductAsync(productDTO);
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
