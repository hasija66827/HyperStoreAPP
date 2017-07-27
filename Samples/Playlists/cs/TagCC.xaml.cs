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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public class TagViewModel : BindableBases
    {
        private Guid _tagId;
        public Guid TagId { get { return this._tagId; } }

        private string _tagName;
        public string TagName { get { return this._tagName; } set { this._tagName = value; } }

        bool _IsChecked = default(bool);
        public bool IsChecked { get { return _IsChecked; } set { SetProperty(ref _IsChecked, value); } }

        public TagViewModel()
        {
        }

        public TagViewModel(Guid tagId,string tagName, bool isChecked)
        {
            this._tagId = tagId;
            this._tagName = tagName;
            this._IsChecked = isChecked;
        }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
            var tags = TagCCF.Current.Tags.ToList();
            var selectedTagIds = tags.Where(t => t.IsChecked == true).Select(t=>t.TagId).ToList();

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
        public static TagCCF Current;
        public TagCCF()
        {
            Current = this;
            _Tags = new ObservableCollection<TagViewModel>(TagDataSource.Tags);
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

        ObservableCollection<TagViewModel> _Tags;
        public ObservableCollection<TagViewModel> Tags { get { return _Tags; } }
    }

    public abstract class BindableBases : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void SetProperty<T>(ref T storage, T value,
            [System.Runtime.CompilerServices.CallerMemberName] String propertyName = null)
        {
            if (!object.Equals(storage, value))
            {
                storage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
