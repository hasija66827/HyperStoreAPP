using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{ 
    public delegate void FilterPersonChangedDelegate();

    public class FilterPersonViewModel
    {
        public IRange<float> WalletBalance { get; set; }
        public FilterPersonViewModel(IRange<float> walletBalance)
        {
            this.WalletBalance = walletBalance;
        }
        public FilterPersonViewModel() { }
    }
}
