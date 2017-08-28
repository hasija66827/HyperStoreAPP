using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemp;
 
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SDKTemplate
{
    public class OrderSummaryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private decimal _totalSales;
        public decimal TotalSales
        {
            get { return this._totalSales; }
            set
            {
                this._totalSales = value;
                this.OnPropertyChanged(nameof(TotalSales));
            }
        }
        private decimal _totalSalesWithDiscount;
        public decimal TotalSalesWithDiscount
        {
            get { return this._totalSalesWithDiscount; }
            set
            {
                this._totalSalesWithDiscount = value;
                this.OnPropertyChanged(nameof(TotalSalesWithDiscount));
            }
        }
        private decimal _receivedNow;
        public decimal ReceivedNow
        {
            get { return this._receivedNow; }
            set
            {
                this._receivedNow = value;
                this.OnPropertyChanged(nameof(ReceivedNow));
            }
        }
        private decimal _receivedLater;
        public decimal ReceivedLater { get { return this._receivedLater; } set { this._receivedLater = value; } }
        public OrderSummaryViewModel()
        {
            this._totalSales = 0;
            this._totalSalesWithDiscount = 0;
            this._receivedNow = 0;
            this._receivedLater = 0;
            if (CustomerOrderListCCF.Current == null)
                throw new System.Exception("OrderListCC should be loaded before OrderSummaryCC");
            CustomerOrderListCCF.Current.CustomerOrderListUpdatedEvent += new CustomerOrderListChangedDelegate(ComputeSales);
        }
        public void ComputeSales(CustomerOrderListCCF orderListCC)
        {/*
            TotalSales = orderListCC.CustomerOrderList.Sum(s => s.BillAmount);
            TotalSalesWithDiscount = orderListCC.CustomerOrderList.Sum(ds => ds.DiscountedAmount);
            ReceivedNow = CalculatedReceivedNow(orderListCC.CustomerOrderList);
            ReceivedLater = CalculatedReceivedNow(orderListCC.CustomerOrderList);*/
        }
   

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
