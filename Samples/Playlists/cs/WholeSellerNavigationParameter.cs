using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class WholeSellerPurchaseNavigationParameter
    {
        public WholeSellerViewModel WholeSellerViewModel { get; set; }
        public WholeSellerBillingSummaryViewModel WholeSellerBillingViewModel { get; set; }
        public WholeSellerPurchaseCheckoutViewModel WholeSellerPurchaseCheckoutViewModel { get; set; }
        public List<WholeSellerProductListVieModel> productViewModelList;
        public WholeSellerPurchaseNavigationParameter()
        { }
    }
}
