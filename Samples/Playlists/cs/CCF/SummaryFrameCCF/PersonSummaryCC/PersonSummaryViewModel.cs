using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class PersonSummaryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public decimal TotalWalletBalance { get; set; }
        public void OnALLPropertyChanged()
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            foreach (PropertyInfo prop in typeof(PersonSummaryViewModel).GetProperties())
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop.Name));
        }
    }
}
