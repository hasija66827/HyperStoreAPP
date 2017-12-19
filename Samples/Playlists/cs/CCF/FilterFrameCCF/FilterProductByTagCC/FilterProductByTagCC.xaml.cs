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
        private FilterTagCollection _TagCollection { get; set; }
        public event TagListChangedDelegate TagListChangedEvent;

        public List<Guid?> SelectedTagIds { get { return _TagCollection.Tags.Where(t => t.IsChecked == true).Select(t => t.TagId).ToList(); } }
        public static FilterProductByTagCC Current;
        public FilterProductByTagCC()
        {
            Current = this;
            this.InitializeComponent();
            this._TagCollection = new FilterTagCollection();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var tags = await TagDataSource.RetreiveTagsAsync();
            TagItemControl.ItemsSource = null;
            if (tags != null)
            {
                var Items = tags.Select(t => new FilterTagViewModel(t.TagId, t.TagName, false)).ToList();
                this._TagCollection = new FilterTagCollection(Items);
                TagItemControl.ItemsSource = this._TagCollection.Tags;
            }
        }

        public void InvokeTagListchangedEvent()
        {
            TagHeader.Text = _TagCollection.Header;
            this.TagListChangedEvent?.Invoke();
        }
    }
}
