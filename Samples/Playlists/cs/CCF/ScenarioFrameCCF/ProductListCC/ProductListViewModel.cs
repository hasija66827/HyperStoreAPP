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
    public delegate void ProductListChangedDelegate();
    public class ProductListViewModel
    {
        public event ProductListChangedDelegate ProductListViewModelChangedEvent;
        private ObservableCollection<ProductViewModel> _products = new ObservableCollection<ProductViewModel>();
        public ObservableCollection<ProductViewModel> Products { get { return this._products; } }
        public ProductListViewModel(){}

        public Int32 AddToBillingList(ProductViewModel product)
        {
            var prd = this._products.Where(p => p.ProductId == product.ProductId).FirstOrDefault();
            Int32 index;
            if (prd!=null)
            {
                index = this._products.IndexOf(prd);
                this._products[index].QuantityPurchased += 1;
            }
            else
            {
                this._products.Add(product);
                index = this._products.IndexOf(product);
                // If new product is added, subscrbing to the Quantity Changed event.
                this._products[index].QuantityChangedEvent += new QuantityChangedDelegate
                ((sender, quantity) => { this.ProductListViewModelChangedEvent?.Invoke(); });
                this._products[index].QuantityPurchased = 1;
            }
            return index;
        }
    }
}
