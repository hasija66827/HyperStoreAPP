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
    public sealed partial class ProductBasicFormCC : Page
    {
        private ProductPricingDetailViewModel _PPDV { get; set; }
        private TagFormViewModel _Tag { get; set; }
        private ProductBasicFormViewModel _PBFV { get; set; }
        private List<ProductTagViewModel> _ProductTags { get; set; }
        private List<Guid?> _SelectedTagIds { get { return _ProductTags.Where(t => t.IsChecked == true).Select(t => t.TagId).ToList(); } }
        public static ProductBasicFormCC Current;
        public ProductBasicFormCC()
        {
            Current = this;
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._PPDV = (ProductPricingDetailViewModel)e.Parameter;
            this._Tag = new TagFormViewModel();
            this._PBFV = DataContext as ProductBasicFormViewModel;
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

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = this._PBFV.ValidateProperties();
            if (IsValid)
            {
                this.Frame.Navigate(typeof(ProductPricingFormCC),_PBFV);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage));
        }

        private async void CreateTag_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = this._Tag.ValidateProperties();
            if (IsValid)
            {
                var tagDTO = new TagDTO()
                {
                    TagName = this._Tag.TagName,
                };
                await TagDataSource.CreateNewTagAsync(tagDTO);
            }
        }
    }
}
