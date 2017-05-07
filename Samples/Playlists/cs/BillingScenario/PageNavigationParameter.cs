using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace SDKTemplate
{
    class PageNavigationParameter
    {
        public BillingViewModel billingViewModel;
        public string MobNo { get; set; }
        public float TotalBillAmount { get; set; }
        public float AdditionalDiscountPer { get; set; }
        public float DiscountedBillAmount { get; set; }
        public float WalletBalance { get; set; }
        public float WalletBalanceToBeDeducted { get;  }
        public float ToBePaid { get; set; }
        public float Paid { get; set; }
        public PageNavigationParameter(float totalBillAmount, float additionalDiscountPer,float walletBalance) {
            TotalBillAmount = totalBillAmount;
            AdditionalDiscountPer = additionalDiscountPer;
            DiscountedBillAmount = ((100 - additionalDiscountPer) * totalBillAmount) / 100;
            WalletBalance = walletBalance;
            WalletBalanceToBeDeducted = (walletBalance<=DiscountedBillAmount)?walletBalance:DiscountedBillAmount;
            ToBePaid = DiscountedBillAmount-WalletBalanceToBeDeducted;
            Paid = ToBePaid;
        }
    }
}
