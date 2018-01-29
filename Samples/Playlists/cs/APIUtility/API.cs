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
        public static string HyperStoreService { get { return "https://hyperstoreservice20180129124709.azurewebsites.net/api/" + User.UserId + "/"; } }
    }

    class API
    {
        public static string Products { get { return "Products"; } }
        public static string Persons { get { return "Persons"; } }
        public static string Tags { get { return "Tags"; } }
        public static string Orders { get { return "Orders"; } }
        public static string Transactions { get { return "Transactions"; } }
        public static string OrderProducts { get { return "OrderProducts"; } }
        public static string PurhcaseHistory { get { return "PurchaseHistory"; } }
        public static string BusinessInsight { get { return "BusinessInsights"; } }
        public static string ProductInsights { get { return "ProductInsights"; } }
        public static string PaymentOptions { get { return "PaymentOptions"; } }
        public static string ProductConsumptionInsights { get { return "ProductConsumptionInsights"; } }

        public static string CustomerInsights { get { return "CustomerInsights"; } }


        public static string CustomerPurchaseTrend { get { return "CustomerPurchaseTrend"; } }
        public static string ProductConsumptionTrend { get { return "ProductConsumptionTrend"; } }
        public static string PriceQuotedBySupplier { get { return "PriceQuotedBySupplier"; } }
    }

    public class CustomAction
    {
        public static string GetBusinessInsight { get { return "GetBusinessInsight"; } }
        public static string GetSusceptibleProducts { get { return "GetSusceptibleProducts"; } }
        public static string GetNewCustomers { get { return "GetNewCustomers"; } }
        public static string GetDetachedCustomers { get { return "GetDetachedCustomers"; } }

        public static string GetRecommendedProducts { get { return "GetRecommendedProducts"; } }
        public static string GetTotalRecordsCount { get { return "GetTotalRecordsCount"; } }
        public static string GetPersonMetadata { get { return "GetPersonMetadata"; } }
        public static string GetProductMetadata { get { return "GetProductMetadata"; } }
    }

    public class AuthenticationServiceAPI
    {
        private static string BaseURI { get { return "https://loginsignupservice20180130030356.azurewebsites.net/api/"; } }
        //private static string BaseURI { get { return "https://localhost:44380/api/"; } }
        public static string Users { get { return BaseURI + "Users"; } }
        public static string OTPVerification { get { return BaseURI + "OTPVerification"; } }
    }

    class AuthenticationServiceCustomAPI
    {
        public static string AuthenticateUser { get { return "AuthenticateUser"; } }
    }
}
