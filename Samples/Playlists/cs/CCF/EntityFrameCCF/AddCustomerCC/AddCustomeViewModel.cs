using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemp.ViewModel
{
    class AddCustomerViewModel : CustomerViewModel, INotifyPropertyChanged
    {
        public override float WalletBalance { get => base.WalletBalance;
            set
            {
                base.WalletBalance = value;
                this.OnPropertyChanged(nameof(WalletBalance));
            }
        }

        public AddCustomerViewModel() : base()
        {

        }
       
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
