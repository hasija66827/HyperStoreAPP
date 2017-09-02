using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemp;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace SDKTemplate
{
    public class OrderSummaryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public decimal TotalBillAmount { get; set; }
        public decimal TotalPayedAmount { get; set; }
        public OrderSummaryViewModel()
        {
        }

        public void OnAllPropertyChanged()
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            foreach (PropertyInfo prop in typeof(OrderSummaryViewModel).GetProperties())
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop.Name));
        }
    }
}
