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
        public static User User { get; set; }
        //public static string HyperStoreService { get { return "https://hyperstoreservicewebapp20170924100256.azurewebsites.net/api/" + User.UserId + "/"; } }
        public static string HyperStoreService { get { return "https://localhost:44346/api/" + User.UserId + "/"; } }
    }

    class API
    {
        public static string Products { get { return "Products"; } }
        public static string Persons { get { return "Persons"; } }
        public static string Tags { get { return "Tags"; } }
        public static string Orders { get { return "Orders"; } }
        public static string Transactions { get { return "Transactions"; } }

        public static string OrderProducts { get { return "OrderProducts"; } }
        public static string CustomerPurchaseTrend { get { return "CustomerPurchaseTrend"; } }
        public static string ProductConsumptionTrend { get { return "ProductConsumptionTrend"; } }
        public static string PriceQuotedBySupplier { get { return "PriceQuotedBySupplier"; } }
        public static string RecommendedProducts { get { return "RecommendedProducts"; } }
    }

    public class CustomAction {
        public static string GetWalletBalanceRange { get { return "GetWalletBalanceRange"; } }
        public static string GetTotalRecordsCount { get { return "GetTotalRecordsCount"; } }
        public static string GetProductMetadata { get { return "GetProductMetadata"; } }
    }

    public class AuthenticationServiceAPI
    {
        //private static string BaseURI { get { return "https://loginsignupserviceapp.azurewebsites.net/api/"; } }
        private static string BaseURI { get { return "https://localhost:44381/api/"; } }
        public static string Users { get { return BaseURI + "Users"; } }
        public static string OTPVerification { get { return BaseURI + "OTPVerification"; } }
    }

    class AuthenticationServiceCustomAPI
    {
        public static string AuthenticateUser { get { return "AuthenticateUser"; } }
    }
}
