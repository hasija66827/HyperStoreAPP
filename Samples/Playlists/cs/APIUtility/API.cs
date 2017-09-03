using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class API
    {
        public static string Customers { get { return "Customers"; } }
        public static string Products { get { return "Products"; } }
        public static string Suppliers { get { return "Suppliers"; } }
        public static string Tags { get { return "Tags"; } }
        public static string CustomerOrders { get { return "CustomerOrders"; } }
        public static string CustomerTransactions { get { return "CustomerTransactions"; } }
        public static string SupplierOrders { get { return "SupplierOrders"; } }
        public static string SupplierTransactions { get { return "SupplierTransactions"; } }

        public static string CustomerOrderProducts { get { return "CustomerOrderProducts"; } }
        public static string SupplierOrderProducts { get { return "SupplierOrderProducts"; } }
        public static string CustomerPurchaseTrend { get { return "CustomerPurchaseTrend"; } }
        public static string ProductConsumptionTrend { get { return "ProductConsumptionTrend"; } }
        public static string PriceQuotedBySupplier { get { return "PriceQuotedBySupplier"; } }
        public static string RecommendedProducts { get { return "RecommendedProducts"; } }
    }
}
