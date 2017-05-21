﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class ProductListViewModel
    {
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
        public float AdditonalDiscountPer { get; set; }
        public float DiscountedBillAmount { get; set; }

        private ObservableCollection<ProductViewModel> _products = new ObservableCollection<ProductViewModel>();
        public ObservableCollection<ProductViewModel> Products { get { return this._products; } }

        public ProductListViewModel()
        {

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
