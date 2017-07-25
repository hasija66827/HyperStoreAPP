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
    public class Item : BindableBases
    {
        public string Text { get; set; }

        bool _IsChecked = default(bool);
        public bool IsChecked { get { return _IsChecked; } set { SetProperty(ref _IsChecked, value); } }
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            this.InitializeComponent();
        }
    }
    public class ViewModel : BindableBases
    {
        public ViewModel()
        {
            _Items = new ObservableCollection<Item>(Enumerable.Range(1, 10)
                .Select(x => new Item()
                {
                    Text = string.Format("Item {0}", x),
                    IsChecked = (x < 4) ? true : false,
                }));
            foreach (var item in this.Items)
                item.PropertyChanged += (s, e) => base.RaisePropertyChanged("Header");
        }

        public string Header
        {
            get
            {
                var array = this.Items
                    .Where(x => x.IsChecked)
                    .Select(x => x.Text).ToArray();
                if (!array.Any())
                    return "None";
                return string.Join("; ", array);
            }
        }

        ObservableCollection<Item> _Items;
        public ObservableCollection<Item> Items { get { return _Items; } }
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
