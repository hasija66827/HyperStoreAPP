using Models;
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
            CustomerASBCC.Current.SelectedCustomerChangedEvent += Current_SelectedCustomerChangedEvent;
            ProductASBCC.Current.OnAddProductClickedEvent += Current_OnAddProductClickedEvent;
        }

        private void Current_OnAddProductClickedEvent(TProduct product)
        {
            var purchasedRecomProduct = this._RecomProducts.Where(rp => rp.ProductId == product.ProductId).FirstOrDefault();
            if (purchasedRecomProduct != null)
                this._RecomProducts.Remove(purchasedRecomProduct);
        }

        private async Task Current_SelectedCustomerChangedEvent()
        {
            var customer = CustomerASBCC.Current.SelectedCustomerInASB;
            if (customer != null)
            {

                var recomProd = await AnalyticsDataSource.RetrieveRecommendedProductAsync((Guid)customer.CustomerId);
                var items = recomProd.Select(rp => new RecommendedProductViewModel(rp));
                items.OrderBy(rp => rp.LastOrderDate);
                this._RecomProducts.Clear();
                foreach (var item in items)
                {
                    this._RecomProducts.Add(item);
                }
            }
            else
                this._RecomProducts.Clear();
        }
    }
}
