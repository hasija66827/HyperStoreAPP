using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public delegate Task TagListChangedDelegate();

    public sealed partial class FilterProductByTagCC : Page
    {
        private TagCollection _TagCollection { get; set; }
        public event TagListChangedDelegate TagListChangedEvent;

        public List<Guid?> SelectedTagIds { get { return _TagCollection?.Tags?.Where(t => t.IsChecked == true).Select(t => t.TagId).ToList(); } }
        public static FilterProductByTagCC Current;
        public FilterProductByTagCC()
        {
            Current = this;
            this.InitializeComponent();
            this._TagCollection = new TagCollection();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var tags = await TagDataSource.RetreiveTagsAsync();
            var Items = tags.Select(t => new TagViewModel()
            {
                TagId = t.TagId,
                TagName = t.TagName,
                IsChecked = false,
            }).ToList();
            this._TagCollection = new TagCollection(Items);
            TagItemControl.ItemsSource = this._TagCollection.Tags;
        }

        public void InvokeTagListchangedEvent()
        {
            TagHeader.Text = _TagCollection.Header;
            this.TagListChangedEvent?.Invoke();
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var Mode = ProductDetailsCC.Current.Mode;
            var productDetail = ProductDetailsCC.Current.ProductViewModelBase;
            var selectedTagIds = FilterProductByTagCC.Current.SelectedTagIds;

            if (Mode == Mode.Create)
            {
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
                this.Frame.Navigate(typeof(BlankPage));
            }

            else if (Mode == Mode.Update)
            {
                /*
                if (ProductDataSource.UpdateProductDetails(productDetail) == true)
                {
                    MainPage.Current.NotifyUser("The Product was updated scuccesfully", NotifyType.StatusMessage);
                    this.Frame.Navigate(typeof(BlankPage));
                }*/
            }
            else
            {
                throw new NotImplementedException();
            }

        }
    }

    
}
