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
    public delegate void ProductListChangedDelegate(object sender, Int32 updatedSize, float updateBillAmount);
    public class ProductListViewModel
    {
        public event ProductListChangedDelegate ProductListChangedEvent;
        public float TotalBillAmount
        {
            get
            {
                double sum = 0;
                foreach (ProductViewModel item in _products)
                    sum += item.NetValue;
                return Utility.RoundInt32((float)sum);
            }
        }
        public Int32 TotalProducts
        {
            get
            {
                Int32 count = 0;
                foreach (ProductViewModel item in _products)
                    count += item.QuantityPurchased;
                return count;
            }
        }
        // To be set by event subscribed to billingsummaryViewModel
        private float _additionalDiscountPer;
        public float AdditonalDiscountPer { get => this._additionalDiscountPer; set => this._additionalDiscountPer = value; }
        public float DiscountedBillAmount { get { return this.TotalBillAmount * (100 - this.AdditonalDiscountPer) / 100; } }

        private ObservableCollection<ProductViewModel> _products = new ObservableCollection<ProductViewModel>();
        public ObservableCollection<ProductViewModel> Products { get { return this._products; } }

        public ProductListViewModel()
        {
            this._additionalDiscountPer = 0;
        }
        private Int32 FirstMatchingProductIndex(ProductViewModel product)
        {
            Int32 i = 0;
            foreach (var p in this._products)
            {
                if (p.BarCode == product.BarCode)
                    return i;
                i++;

            }
            return -1;
        }
        public Int32 AddToBillingList(ProductViewModel product)
        {
            Int32 index = FirstMatchingProductIndex(product);
            // If product does not exist
            if (index == -1)
            {
                this._products.Add(product);
                index = this._products.IndexOf(product);
                //if new product is added, subscrbing to the Quantity Changed event.
                this._products[index].QuantityChangedEvent += new QuantityChangedDelegate
                ((sender, quantity) => { this.ProductListChangedEvent?.Invoke(this, TotalProducts, TotalBillAmount); });
                this._products[index].QuantityPurchased = 1;
            }
            else
            {
                /* Will trigger the event QuantityPropertyChange
                 and which will inturn invoke the function TotalValue_TotalProductsPropertyChanged.*/
                this._products[index].QuantityPurchased += 1;
            }
            return index;
        }
    }
}
