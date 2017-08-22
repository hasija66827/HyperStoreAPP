using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{ 
    public delegate Task FilterPersonChangedDelegate();

    public class FilterPersonCriteria
    {
        public IRange<float> WalletBalance { get; set; }
        public FilterPersonCriteria(IRange<float> walletBalance)
        {
            this.WalletBalance = walletBalance;
        }
        public FilterPersonCriteria() { }
    }
}
