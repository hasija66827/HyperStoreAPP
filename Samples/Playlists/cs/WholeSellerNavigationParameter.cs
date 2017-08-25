using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class WholeSellerCheckoutNavigationParameter
    {
        public TSupplier WholeSellerViewModel { get; set; }
        public WholeSellerBillingSummaryViewModel WholeSellerBillingSummaryViewModel { get; set; }
        public WholeSellerCheckoutViewModel WholeSellerCheckoutViewModel { get; set; }
        public List<WholeSellerProductVieModel> productViewModelList;
        public WholeSellerCheckoutNavigationParameter()
        { }
    }
}
