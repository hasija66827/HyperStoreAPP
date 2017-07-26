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
        private string _tagName;
        public string TagName { get { return this._tagName; } set { this._tagName = value; } }

        bool _IsChecked = default(bool);
        public bool IsChecked { get { return _IsChecked; } set { SetProperty(ref _IsChecked, value); } }

        public TagViewModel()        {
        }

        public TagViewModel(string tagName, bool isChecked=false) {
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

        private void NewTag_Click(object sender, RoutedEventArgs e)
        {
            TagDataSource.CreateTag(new TagViewModel("hasija"));
        }
    }

    public class TagCCF : BindableBases
    {
        public TagCCF()
        {
            _Tags = new ObservableCollection<TagViewModel> (TagDataSource.RetrieveTags());
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
