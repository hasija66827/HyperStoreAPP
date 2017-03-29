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
    public class ProductViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public float TotalValue
        {
            get
            {
                double sum = 0;
                foreach (Product item in _products)
                    sum += item.NetValue;
                return Utility.RoundInt32((float)sum);
            }
        }
        public Int32 TotalProducts { get { return _products.Count(); } }

        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products { get { return this._products; } }
        private Int32 FirstMatchingProductIndex(Product product)
        {
            Int32 i = 0;
            foreach (var p in this._products)
            {
                if (p.Id == product.Id)
                    return i;
                i++;

            }
            return -1;
        }
        public bool AddProductInList(Product product)
        {
            if (product == null)
                return false;
            // Check if product exist in the billing
            Int32 index = FirstMatchingProductIndex(product);
            if (index >= 0)
                this._products[index].Quantity += 1;
            else
                this._products.Add(product);
            //TODO: Check if multiple productsId exist
            // Updating the Total Value on addition of items in the list.
            this.OnPropertyChanged(nameof(TotalValue));
            this.OnPropertyChanged(nameof(TotalProducts));
            return true;
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
