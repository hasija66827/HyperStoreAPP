using Models;
using Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed class FilterTagViewModel : BindableBases, ITag
    {
        public Guid? TagId { get; set; }
        public string TagName { get; set; }

        public bool _IsChecked = default(bool);
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                SetProperty(ref _IsChecked, value); FilterProductByTagCC.Current.InvokeTagListchangedEvent();
            }
        }

        public FilterTagViewModel(Guid? tagId, string tagName, bool isChecked)
        {
            _IsChecked = IsChecked;
            TagId = tagId;
            TagName = tagName;
        }
    }


    public class FilterTagCollection
    {
        public List<FilterTagViewModel> Tags { get; set; }
        public string Header
        {
            get
            {
                var array = this.Tags
                    .Where(x => x.IsChecked)
                    .Select(x => x.TagName).ToArray();
                if (!array.Any())
                    return "All Tags";
                return string.Join("; ", array);
            }
        }

        public FilterTagCollection()
        {
            this.Tags = new List<FilterTagViewModel>();
        }
        public FilterTagCollection(List<FilterTagViewModel> tags)
        {
            this.Tags = tags;

        }
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
