using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class BaseURI
    {
        public static TUser User { get; set; }
        public static string HyperStoreService { get { return "http://hyperstoreservicewebapp20170924100256.azurewebsites.net/api/" + User.UserId + "/"; } }
    }

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

    public class AuthenticationServiceAPI
    {
        private static string BaseURI { get { return "http://loginsignupserviceapp.azurewebsites.net/api/"; } }
        public static string Users { get { return BaseURI + "Users"; } }
        public static string OTPVerification { get { return BaseURI + "OTPVerification"; } }
    }

    class AuthenticationServiceCustomAPI
    {
        public static string AuthenticateUser { get { return "AuthenticateUser"; } }
    }
}
