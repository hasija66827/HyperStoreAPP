using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemp;
using SDKTemp.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SDKTemplate
{
    public class OrderSummaryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private float _totalSales;
        public float TotalSales
        {
            get { return this._totalSales; }
            set
            {
                this._totalSales = value;
                this.OnPropertyChanged(nameof(TotalSales));
            }
        }
        private float _totalSalesWithDiscount;
        public float TotalSalesWithDiscount
        {
            get { return this._totalSalesWithDiscount; }
            set
            {
                this._totalSalesWithDiscount = value;
                this.OnPropertyChanged(nameof(TotalSalesWithDiscount));
            }
        }
        private float _receivedNow;
        public float ReceivedNow
        {
            get { return this._receivedNow; }
            set
            {
                this._receivedNow = value;
                this.OnPropertyChanged(nameof(ReceivedNow));
            }
        }
        private float _receivedLater;
        public float ReceivedLater { get { return this._receivedLater; } set { this._receivedLater = value; } }
        public OrderSummaryViewModel()
        {
            this._totalSales = 0;
            this._totalSalesWithDiscount = 0;
            this._receivedNow = 0;
            this._receivedLater = 0;
            OrderListCC.Current.OrderListChangedEvent += new OrderListChangedDelegate(OrderListChangedSubscriber);
        }
        public void OrderListChangedSubscriber(OrderListCC orderListCC)
        {
            TotalSales = orderListCC.orderList.Sum(s => s.BillAmount);
            TotalSalesWithDiscount = orderListCC.orderList.Sum(ds => ds.DiscountedBillAmount);
            ReceivedNow = CalculatedReceivedNow(orderListCC.orderList);
            OrderSummaryCC.Current.orderSummaryViewModel.ReceivedLater = CalculatedReceivedNow(orderListCC.orderList);
        }
        private float CalculatedReceivedNow(List<OrderViewModel> orderList)
        {
            float f = 2;
            return f;
            //TODO:
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
