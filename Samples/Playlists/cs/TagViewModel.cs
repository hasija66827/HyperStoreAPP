using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{

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