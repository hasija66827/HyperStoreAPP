using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{ 
    public delegate Task FilterPersonChangedDelegate();

    public class FilterPersonCriteriaViewModel
    {
        public IRange<decimal> WalletBalance { get; set; }
        public FilterPersonCriteriaViewModel() { }
    }
}
