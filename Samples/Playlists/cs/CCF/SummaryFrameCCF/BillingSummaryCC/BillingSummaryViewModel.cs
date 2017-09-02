using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class CustomerBillingSummaryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public decimal TotalQuantity { get; set; }

        public decimal TotalItems { get; set; }

        public string Items_Quantity { get { return this.TotalItems + "/" + this.TotalQuantity; } }
        public decimal CartAmount { get; set; }

        private decimal _discountAmount;
        public decimal DiscountAmount { get { return this._discountAmount; } set { this._discountAmount = value; } }

        public decimal Tax { get; set; }
        public decimal PayAmount { get; set; }

        public decimal AdditionalDiscountPer { get; set; }

        public decimal DiscountedBillAmount { get { return ((100 - this.AdditionalDiscountPer) * this.PayAmount) / 100; } }

        public CustomerBillingSummaryViewModel()
        {
        }

        public void OnALLPropertyChanged()
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            foreach (PropertyInfo prop in typeof(CustomerBillingSummaryViewModel).GetProperties())
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop.Name));
        }
    }
}
