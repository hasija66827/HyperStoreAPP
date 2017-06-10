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
    public delegate void WholeSellerProductListUpdatedDelegate(ObservableCollection<WholeSellerProductListVieModel> products);
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WholeSellerPurchasedProductListCC : Page
    {
        public event WholeSellerProductListUpdatedDelegate WholeSellerProductListUpdatedEvent;
        private ObservableCollection<WholeSellerProductListVieModel> _products = new ObservableCollection<WholeSellerProductListVieModel>();
        public ObservableCollection<WholeSellerProductListVieModel> Products { get { return this._products; } }
        public static WholeSellerPurchasedProductListCC Current;
        public WholeSellerPurchasedProductListCC()
        {
            Current = this;
            this.InitializeComponent();
            ProductASBCC.Current.OnAddProductClickedEvent += new OnAddProductClickedDelegate(this.AddToBillingList);
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
            var navigationParameter = new WholeSellerPurchaseNavigationParameter();
            navigationParameter.WholeSellerViewModel = selectedWholesellerInASB;
            navigationParameter.productViewModelList = this.Products.ToList();
            navigationParameter.WholeSellerBillingViewModel =
                WholeSellerBillingSummaryCC.Current.wholeSellerBillingSummaryViewModel;
            this.Frame.Navigate(typeof(WholeSellerPurchasedCheckoutCC), navigationParameter);
        }

        public void AddToBillingList(object sender, ProductViewModel selectedProduct)
        {
            var existingProduct = this._products.Where(p => p.BarCode == selectedProduct.BarCode);
            // If product does not exist
            if (existingProduct.Count() == 0)
            {
                WholeSellerProductListVieModel w = new WholeSellerProductListVieModel(selectedProduct.BarCode,
                    selectedProduct.Name, 0, 1);
                this._products.Add(w);
                InvokeProductListChangeEvent();
            }
            else
            {
                Int32 index = this._products.IndexOf(existingProduct.FirstOrDefault());
                this._products[index].QuantityPurchased += 1;
            }
        }
        public static void InvokeProductListChangeEvent()
        {
            Current.WholeSellerProductListUpdatedEvent?.Invoke(Current.Products);
        }
    }
}
