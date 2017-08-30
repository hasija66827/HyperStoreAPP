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
    public sealed partial class ProductTagCC : Page
    {
        public ProductTagCC()
        {
            this.InitializeComponent();
        }
        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            /*
            var productDetail = ProductDetailsCC.Current.ProductViewModelBase;
            var selectedTagIds = FilterProductByTagCC.Current.SelectedTagIds;
                var productDTO = new ProductDTO()
                {
                    TagIds = selectedTagIds,
                    CGSTPer = productDetail.CGSTPer,
                    Code = productDetail.Code,
                    DiscountPer = productDetail.DiscountPer,
                    DisplayPrice = productDetail.DisplayPrice,
                    Name = productDetail.Name,
                    RefillTime = productDetail.RefillTime,
                    SGSTPer = productDetail.SGSTPer,
                    Threshold = productDetail.Threshold
                };
                if (await ProductDataSource.CreateNewProductAsync(productDTO) != null)
                    MainPage.Current.NotifyUser("The product was created succesfully", NotifyType.StatusMessage);
                this.Frame.Navigate(typeof(BlankPage));*/
        }
    }
}
