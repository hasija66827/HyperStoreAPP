using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public delegate void TagListChangedDelegate();

    public sealed partial class TagCC : Page
    {
        public TagCC()
        {
            this.InitializeComponent();

        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var Mode = ProductDetailsCC.Current.Mode;
            var productDetail = ProductDetailsCC.Current.ProductDetailViewModel;
            var selectedTagIds = TagCCF.Current.SelectedTagIds;

            if (Mode == Mode.Create)
            {
                if (ProductDataSource.CreateNewProduct(productDetail) == true)
                {
                    TagProductDataSource.CreateTagProduct(productDetail.ProductId, selectedTagIds);
                    MainPage.Current.NotifyUser("The product was created succesfully", NotifyType.StatusMessage);
                    this.Frame.Navigate(typeof(BlankPage));
                }

            }

            else if (Mode == Mode.Update)
            {
                if (ProductDataSource.UpdateProductDetails(productDetail) == true)
                {
                    MainPage.Current.NotifyUser("The Product was updated scuccesfully", NotifyType.StatusMessage);
                    this.Frame.Navigate(typeof(BlankPage));
                }
            }
            else
            {
                throw new NotImplementedException();
            }

        }
    }

    public class TagCCF : BindableBases
    {

        public event TagListChangedDelegate TagListChangedEvent;
        public static TagCCF Current;
        public TagCCF()
        {
            Current = this;
            _tags = new ObservableCollection<TagViewModel>(TagDataSource.Tags);
            foreach (var tag in this.Tags)
                tag.PropertyChanged += (s, e) => base.RaisePropertyChanged("Header");
        }

        public string Header
        {
            get
            {
                var array = this.Tags
                    .Where(x => x.IsChecked)
                    .Select(x => x.TagName).ToArray();
                if (!array.Any())
                    return "None";
                return string.Join("; ", array);
            }
        }

        public void InvokeTagListchangedEvent()
        {
            this.TagListChangedEvent?.Invoke();
        }

        ObservableCollection<TagViewModel> _tags;
        public ObservableCollection<TagViewModel> Tags { get { return _tags; } }

        public List<Guid?> SelectedTagIds { get { return _tags.Where(t => t.IsChecked == true).Select(t => t.TagId).ToList(); } }
    }
}
