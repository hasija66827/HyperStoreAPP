using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public delegate void SupplierProductListUpdatedDelegate(List<SupplierOrderProductViewModelBase> Products);
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SupplierPurchasedProductListCC : Page
    {
        public static SupplierPurchasedProductListCC Current;
        public event SupplierProductListUpdatedDelegate SupplierProductListUpdatedEvent;
        private ObservableCollection<SupplierOrderProductViewModel> _Products { get; set; }
        public List<SupplierOrderProductViewModelBase> Products { get { return this._Products.Cast<SupplierOrderProductViewModelBase>().ToList(); } }
        public SupplierPurchasedProductListCC()
        {
            Current = this;
            this.InitializeComponent();
            this._Products = new ObservableCollection<SupplierOrderProductViewModel>();
            ProductASBCC.Current.OnAddProductClickedEvent += new OnAddProductClickedDelegate(this._AddProductToCart);
            CheckoutBtn.Click += CheckoutBtn_Click;
        }

        private void CheckoutBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedSupplierInASB = WholeSellerASBCC.Current.SelectedWholeSellerInASB;
            if (selectedSupplierInASB == null)
            {
                MainPage.Current.NotifyUser("Select the Wholeseller", NotifyType.ErrorMessage);
                return;
            }
            var navigationParameter = new SupplierPageNavigationParameter()
            {
                SelectedSupplier = selectedSupplierInASB,
                ProductPurchased = this.Products,
                WholeSellerBillingSummaryViewModel = WholeSellerBillingSummaryCC.Current.wholeSellerBillingSummaryViewModel,
            };
            this.Frame.Navigate(typeof(SupplierCheckoutCC), navigationParameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedProduct"></param>
        /// <returns></returns>
        private int _AddProductToCart(TProduct selectedProduct)
        {
            int index = 0;
            var existingProduct = this._Products.Where(p => p.ProductId == selectedProduct.ProductId).FirstOrDefault();
            if (existingProduct != null)
            {
                index = this._Products.IndexOf(existingProduct);
                this._Products[index].QuantityPurchased += 1;//Event will be triggered.
            }
            else
            {
                SupplierOrderProductViewModel w = new SupplierOrderProductViewModel(selectedProduct);
                this._Products.Add(w);
                index = this._Products.IndexOf(w);
                InvokeProductListChangeEvent();
            }
            return index;
        }

        public void InvokeProductListChangeEvent()
        {
            this.SupplierProductListUpdatedEvent?.Invoke(Products);
        }
    }
}
