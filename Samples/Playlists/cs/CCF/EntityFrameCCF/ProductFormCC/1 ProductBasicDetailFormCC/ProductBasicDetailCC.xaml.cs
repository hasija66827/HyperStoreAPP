using Models;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class ProductTagCC : Page
    {
        private ProductPricingDetailViewModel _PPDV { get; set; }
        private ProductBasicDetailViewModel _PBDV { get; set; }
        private List<ProductTagViewModel> _ProductTags { get; set; }
        private List<Guid?> _SelectedTagIds { get { return _ProductTags.Where(t => t.IsChecked == true).Select(t => t.TagId).ToList(); } }
        public static ProductTagCC Current;
        public ProductTagCC()
        {
            Current = this;
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._PPDV = (ProductPricingDetailViewModel)e.Parameter;
            this._PBDV = DataContext as ProductBasicDetailViewModel;
            var tags = await TagDataSource.RetreiveTagsAsync();
            var Items = tags.Select(t => new ProductTagViewModel()
            {
                TagId = t.TagId,
                TagName = t.TagName,
                IsChecked = false,
            }).ToList();
            this._ProductTags = Items;
            TagItemControl.ItemsSource = this._ProductTags;
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = this._PBDV.ValidateProperties();
            if (IsValid)
            {
                var productDTO = new ProductDTO()
                {
                    TagIds = _SelectedTagIds,
                    CGSTPer = _PPDV.CGSTPer,
                    Code = _PBDV.Code,
                    DiscountPer = _PPDV.DiscountPer,
                    DisplayPrice = _PPDV.DisplayPrice,
                    Name = _PBDV.Name,
                    RefillTime = 12,//remove it
                    SGSTPer = _PPDV.SGSTPer,
                    Threshold = _PBDV.Threshold
                };
                var product = await ProductDataSource.CreateNewProductAsync(productDTO);
            } 
        }
    }
}
