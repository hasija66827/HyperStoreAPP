using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class ProductListToPurchaseCollection : BindableBase
    {
        ObservableCollection<ProductListToPurchaseViewModel> feeds = new ObservableCollection<ProductListToPurchaseViewModel>();
        public ProductListToPurchaseCollection()
        {

        }
        public async Task LoadFeeds()
        {
            Feeds = new ObservableCollection<ProductListToPurchaseViewModel>(ProductDataSource.RetreiveProductListToPurchaseByRespectiveWholeSellers());
        }

        public ObservableCollection<ProductListToPurchaseViewModel> Feeds
        {
            get { return this.feeds; }
            set
            {
                Set<ObservableCollection<ProductListToPurchaseViewModel>>(ref this.feeds, value);
            }
        }
    }

    public class ProductListToPurchaseViewModel
    {
        public TSupplier WholeSellerViewModel { get; set; }
        public List<ProductViewModelBase> Products { get; set; }
    }

    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual bool Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (object.Equals(storage, value))
                return false;

            storage = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        public virtual void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;

            var handler = PropertyChanged;
            if (!object.Equals(handler, null))
            {
                var args = new PropertyChangedEventArgs(propertyName);
                handler.Invoke(this, args);

            }
        }
    }
}