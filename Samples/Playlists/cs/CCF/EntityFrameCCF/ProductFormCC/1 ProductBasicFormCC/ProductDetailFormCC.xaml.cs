using Models;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ProductBasicInformation
    {
        public ProductDetailFormViewModel _PDFV { get; set; }
        public List<Guid?> _SelectedTagIds { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductBasicFormCC : Page
    {
        private ProductBasicInformation _ProdBasicInfo { get; set; }
        private TagFormViewModel _TagFormViewModel { get; set; }
        private List<ProductTagViewModel> _ProductTags { get; set; }
        public static ProductBasicFormCC Current;
        public ProductBasicFormCC()
        {
            Current = this;
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._ProdBasicInfo = new ProductBasicInformation();
            this._ProdBasicInfo._PDFV = DataContext as ProductDetailFormViewModel;
            this._TagFormViewModel = new TagFormViewModel();
            this._ProductTags = await _RetrieveTags();
            TagItemControl.ItemsSource = this._ProductTags;
            TagDataSource.CreatedNewTagEvent += _AppendNewTag;
        }

        private async Task<List<ProductTagViewModel>> _RetrieveTags()
        {
            var tags = await TagDataSource.RetreiveTagsAsync();
            var Items = tags.Select(t => new ProductTagViewModel()
            {
                TagId = t.TagId,
                TagName = t.TagName,
                IsChecked = false,
            }).OrderBy(t => t.TagName).ToList();
            return Items;
        }

        private void _AppendNewTag(TTag TTag)
        {
            var tag = new ProductTagViewModel()
            {
                TagId = TTag.TagId,
                TagName = TTag.TagName,
                IsChecked = false,
            };
            this._ProductTags.Insert(0, tag);
            TagItemControl.ItemsSource = this._ProductTags;
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = this._ProdBasicInfo._PDFV.ValidateProperties();
            if (IsValid)
            {
                _ProdBasicInfo._SelectedTagIds = _ProductTags.Where(t => t.IsChecked == true).Select(t => t.TagId).ToList();
                this.Frame.Navigate(typeof(Pro), _ProdBasicInfo);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage));
        }

        private async void CreateTag_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = this._TagFormViewModel.ValidateProperties();
            if (IsValid)
            {
                var tagDTO = new TagDTO()
                {
                    TagName = this._TagFormViewModel.TagName,
                };
                var newTag = await TagDataSource.CreateNewTagAsync(tagDTO);
            }
        }
    }
}
