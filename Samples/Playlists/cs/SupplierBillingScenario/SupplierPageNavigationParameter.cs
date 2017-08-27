using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class SupplierPageNavigationParameter
    {
        public List<SupplierOrderProductViewModelBase> ProductPurchased { get; set; }
        public TSupplier SelectedSupplier { get; set; }
        public SupplierBillingSummaryViewModel WholeSellerBillingSummaryViewModel { get; set; }
        public SupplierPageNavigationParameter()
        { }
    }
}
