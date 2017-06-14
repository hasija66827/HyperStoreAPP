using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class PriceQuotedByWholeSellerViewModel
    {
        private string _wholeSellerName;
        public string WholeSellerName { get { return this._wholeSellerName; } }
        private string _wholeSellerMobileNo;
        public string WholeSellerMobileNo { get { return this._wholeSellerMobileNo; } }
        private float _purchasePrice;
        public float PurchasePrice { get { return this._purchasePrice; } }
        private DateTime _orderDate;
        public DateTime OrderDate { get { return this._orderDate; } }

        public PriceQuotedByWholeSellerViewModel()
        {
            this._wholeSellerName = "";
            this._wholeSellerMobileNo = "";
            this._purchasePrice = 0;
            this._orderDate = DateTime.Now.AddYears(-100);
        }
        public PriceQuotedByWholeSellerViewModel(PriceQuotedByWholeSeller p)
        {
            var wholeSellerViewModel = WholeSellerDataSource.GetWholeSellerById(p.wholeSellerId);
            this._wholeSellerName = wholeSellerViewModel!=null? wholeSellerViewModel.Name:"xxxxx";
            this._wholeSellerMobileNo = wholeSellerViewModel != null ? wholeSellerViewModel.MobileNo : "99xxx88xxx";
            this._purchasePrice = p.purchasePrice;
            this._orderDate = p.orderDate;
        }
    }
    /// <summary>
    /// This class is used by detail page of Product in stock page.
    /// </summary>
    public class PriceQuotedByWholeSellerCollection
    {
        private List<PriceQuotedByWholeSellerViewModel> _priceQuotedByWholeSellers;
        public List<PriceQuotedByWholeSellerViewModel> PriceQuotedByWholeSellers
        {
            get { return this._priceQuotedByWholeSellers; }
            set
            {
                this._priceQuotedByWholeSellers = value;
            }
        }
        public PriceQuotedByWholeSellerCollection(List<PriceQuotedByWholeSeller> items)
        {
            this._priceQuotedByWholeSellers = new List<PriceQuotedByWholeSellerViewModel>();
            foreach (var item in items)
            {
                this._priceQuotedByWholeSellers.Add(new PriceQuotedByWholeSellerViewModel(item));
            }
        }

    }
}
