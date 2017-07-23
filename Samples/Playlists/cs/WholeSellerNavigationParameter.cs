using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class WholeSellerCheckoutNavigationParameter
    {
        public WholeSellerViewModel WholeSellerViewModel { get; set; }
        public WholeSellerBillingSummaryViewModel WholeSellerBillingSummaryViewModel { get; set; }
        public WholeSellerCheckoutViewModel WholeSellerPurchaseCheckoutViewModel { get; set; }
        public List<WholeSellerProductVieModel> productViewModelList;
        public WholeSellerCheckoutNavigationParameter()
        { }
    }
}
