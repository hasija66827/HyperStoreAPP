using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class OrderSummaryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public decimal TotalBillAmount { get; set; }
        public decimal TotalPayedAmount { get; set; }
        public decimal TotalSettledAmount { get; set; }
        public decimal TotalRemainingAmount
        { get { return TotalBillAmount - TotalSettledAmount; } }

        public void OnAllPropertyChanged()
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            foreach (PropertyInfo prop in typeof(OrderSummaryViewModel).GetProperties())
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop.Name));
        }
    }
}
