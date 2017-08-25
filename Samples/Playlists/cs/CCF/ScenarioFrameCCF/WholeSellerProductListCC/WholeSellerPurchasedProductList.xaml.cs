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
    public delegate void WholeSellerProductListUpdatedDelegate(ObservableCollection<WholeSellerProductVieModel> products);
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WholeSellerPurchasedProductListCC : Page
    {
        public static WholeSellerPurchasedProductListCC Current;
        public event WholeSellerProductListUpdatedDelegate WholeSellerProductListUpdatedEvent;
        private ObservableCollection<WholeSellerProductVieModel> _products = new ObservableCollection<WholeSellerProductVieModel>();
        public ObservableCollection<WholeSellerProductVieModel> Products { get { return this._products; } }
        public WholeSellerPurchasedProductListCC()
        {
            Current = this;
            this.InitializeComponent();
            ProductASBCC.Current.OnAddProductClickedEvent += new OnAddProductClickedDelegate(this._AddProductToCart);
            CheckoutBtn.Click += CheckoutBtn_Click;
        }

        private void CheckoutBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedWholesellerInASB = WholeSellerASBCC.Current.SelectedWholeSellerInASB;
            if (selectedWholesellerInASB == null)
            {
                MainPage.Current.NotifyUser("Select the Wholeseller", NotifyType.ErrorMessage);
                return;
            }
            var navigationParameter = new WholeSellerCheckoutNavigationParameter();
            navigationParameter.WholeSellerViewModel = selectedWholesellerInASB;
            navigationParameter.productViewModelList = this.Products.ToList();
            navigationParameter.WholeSellerBillingSummaryViewModel =
                                    WholeSellerBillingSummaryCC.Current.wholeSellerBillingSummaryViewModel;
            this.Frame.Navigate(typeof(WholeSellerCheckoutCC), navigationParameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedProduct"></param>
        /// <returns></returns>
        private int _AddProductToCart(CustomerProductViewModel selectedProduct)
        {
            int index = 0;
            var existingProduct = this._products.Where(p => p.ProductId == selectedProduct.ProductId).FirstOrDefault();
            if (existingProduct != null)
            {
                index = this._products.IndexOf(existingProduct);
                this._products[index].QuantityPurchased += 1;//Event will be triggered.
            }
            else
            {
                WholeSellerProductVieModel w = new WholeSellerProductVieModel(selectedProduct.ProductId, selectedProduct.Code,
                   selectedProduct.Name, 0, 1, selectedProduct.SellingPrice);
                this._products.Add(w);
                index = this._products.IndexOf(w);
                InvokeProductListChangeEvent();
            }
            return index;
        }

        public void InvokeProductListChangeEvent()
        {
            this.WholeSellerProductListUpdatedEvent?.Invoke(Current.Products);
        }
    }
}
