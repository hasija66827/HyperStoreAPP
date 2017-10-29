using SDKTemplate.DTO;
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
    public sealed class CustomerBillingSummaryViewModelBase : BillingSummaryDTO, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public decimal? CartAmount { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? Tax { get; set; }

        public string Items_Quantity { get { return this.TotalItems + "/" + this.TotalQuantity; } }

        public CustomerBillingSummaryViewModelBase()
        {
        }

        public void OnALLPropertyChanged()
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            foreach (PropertyInfo prop in typeof(CustomerBillingSummaryViewModelBase).GetProperties())
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop.Name));
        }
    }
}
