using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class PriceQuotedByWholeSellersViewModel
    {
        public Guid? wholeSellerId;
        public DateTime orderDate;
        public Guid? productId;
        public float purchasePrice;

        public PriceQuotedByWholeSellersViewModel(Guid? wholeSellerId, DateTime orderDate, Guid? productId, float purchasePrice)
        {
            this.wholeSellerId = wholeSellerId;
            this.orderDate = orderDate;
            this.productId = productId;
            this.purchasePrice = purchasePrice;
        }

        public PriceQuotedByWholeSellersViewModel() {
            this.wholeSellerId = null;
            this.orderDate = DateTime.Now.AddYears(-100);
            this.productId = null;
            this.purchasePrice = 0;
        }
    }
    public class PriceListByWholeSellers:INotifyPropertyChanged
    {
        private List<PriceQuotedByWholeSellersViewModel> _priceQuotedByWholeSellers;
        public List<PriceQuotedByWholeSellersViewModel> PriceQuotedByWholeSellers { get {return this._priceQuotedByWholeSellers; }
            set{ this._priceQuotedByWholeSellers = value;
                this.OnPropertyChanged(nameof(PriceListByWholeSellers));
            } }
        public PriceListByWholeSellers() {
            this.PriceQuotedByWholeSellers = new List<PriceQuotedByWholeSellersViewModel>();
            this.PriceQuotedByWholeSellers.Add(new PriceQuotedByWholeSellersViewModel(null, DateTime.Now, null, 212));
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
