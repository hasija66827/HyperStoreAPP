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
        private TProduct _SelectedProduct { get; set; }
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
            this._SelectedProduct = (TProduct)e.Parameter;
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
            var productDTO = new ProductDTO()
            {
                TagIds = _SelectedTagIds,
                CGSTPer = _SelectedProduct.CGSTPer,
                Code = _SelectedProduct.Code,
                DiscountPer = _SelectedProduct.DiscountPer,
                DisplayPrice = _SelectedProduct.DisplayPrice,
                Name = _SelectedProduct.Name,
                RefillTime = _SelectedProduct.RefillTime,
                SGSTPer = _SelectedProduct.SGSTPer,
                Threshold = _SelectedProduct.Threshold
            };
            if (await ProductDataSource.CreateNewProductAsync(productDTO) != null)
                MainPage.Current.NotifyUser("The product was created succesfully", NotifyType.StatusMessage);
            this.Frame.Navigate(typeof(BlankPage));
        }
    }
}
