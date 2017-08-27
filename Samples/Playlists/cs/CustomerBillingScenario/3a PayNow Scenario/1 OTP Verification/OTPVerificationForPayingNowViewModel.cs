using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class OTPVerificationForPayingNowBase
    {
        public TCustomer Customer { get; set; }
        public decimal WalletAmountToBeDeducted { get; set; }
    }
    public class OTPVerificationForPayingNowViewModel : OTPVerificationForPayingNowBase
    {

    }
}
