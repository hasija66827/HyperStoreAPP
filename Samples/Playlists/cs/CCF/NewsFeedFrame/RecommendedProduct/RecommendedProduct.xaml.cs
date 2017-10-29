﻿using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecommendedProductCC : Page
    {
        private ObservableCollection<RecommendedProductViewModel> _RecomProducts { get; set; }
        public RecommendedProductCC()
        {
            this.InitializeComponent();
            this._RecomProducts = new ObservableCollection<RecommendedProductViewModel>();
            SupplierASBCC.Current.SelectedSupplierChangedEvent += Current_SelectedCustomerChangedEvent;
            CustomerProductListCC.Current.NewProductAddedIntoListEvent += Current_NewProductAddedIntoListEvent;
        }

        private void Current_NewProductAddedIntoListEvent(TProduct product)
        {
            var purchasedRecomProduct = this._RecomProducts.Where(rp => rp.ProductId == product.ProductId).FirstOrDefault();
            if (purchasedRecomProduct != null)
                this._RecomProducts.Remove(purchasedRecomProduct);
        }

        private async Task Current_SelectedCustomerChangedEvent()
        {
            var customer = SupplierASBCC.Current.SelectedSupplierInASB;
            if (customer != null)
            {

                var TRecomProds = await AnalyticsDataSource.RetrieveRecommendedProductAsync((Guid)customer.SupplierId);
                var recomProds = TRecomProds.Select(rp => new RecommendedProductViewModel(rp));
                recomProds.OrderBy(rp => rp.LastOrderDate);
                this._RecomProducts.Clear();
                foreach (var recomProd in recomProds)
                {
                    var IsProductExistInList = CustomerProductListCC.Current.Products.Find(p => p.ProductId == recomProd.ProductId) != null;
                    if (!IsProductExistInList)
                        this._RecomProducts.Add(recomProd);
                }
            }
            else
                this._RecomProducts.Clear();
        }
    }
}
