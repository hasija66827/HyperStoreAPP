using SDKTemplate.DTO;
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
    public sealed class SupplierBillingSummaryViewModelBase : SupplierBillingSummaryDTO, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public string Items_Quantity { get { return this.TotalItems + "/" + this.TotalQuantity; } }

        public SupplierBillingSummaryViewModelBase()
        {
        }

        public void OnALLPropertyChanged()
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            foreach (PropertyInfo prop in typeof(SupplierBillingSummaryViewModelBase).GetProperties())
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop.Name));
        }
    }
}
