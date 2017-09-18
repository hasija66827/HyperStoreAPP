using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public enum ScenarioType
    {
        PayToSupplier_Transaction,
        PlacingCustomerOrder_Credit,
        PlacingCustomerOrder_Debit,
        PlacingSupplierOrder_Credit,
        ReceiveFromCustomer_Transaction,
    }

    public class OTPVConstants
    {
        public static readonly int OTPLength = 6;
        public static readonly string PayToSupplier_Transaction = "OTP for Paying INR {0} Now to Supplier {1} is {2}. Please donot share it with anyone for security reasons.";
        public static readonly string PlacingCustomerOrder_Credit = "OTP for Deducting INR {0} from your wallet at {1} is {2}. Please share it with merchant only for security reasons.";
        public static readonly string PlacingCustomerOrder_Debit = "OTP for Adding INR {0} to your wallet at {1} is {2}. Please share it with merchant only for security reasons.";
        public static readonly string PlacingSupplierOrder_Credit = "OTP for Paying INR {0} Later to Supplier {1} is {2}. Please donot share it with anyone for security reasons.";
        public static readonly string ReceiveFromCustomer_Transaction = "OTP for Adding INR {0} to your wallet at {1} is {2}. Please share it with merchant only for security reasons.";
        public static readonly string OTPLiteral = "{0}";
        public static Dictionary<ScenarioType, string> SMSContents = new Dictionary<ScenarioType, string>();

        static OTPVConstants()
        {
            SMSContents.Add(ScenarioType.PayToSupplier_Transaction, PayToSupplier_Transaction);
            SMSContents.Add(ScenarioType.PlacingCustomerOrder_Credit, PlacingCustomerOrder_Credit);
            SMSContents.Add(ScenarioType.PlacingCustomerOrder_Debit, PlacingCustomerOrder_Debit);
            SMSContents.Add(ScenarioType.PlacingSupplierOrder_Credit, PlacingSupplierOrder_Credit);
            SMSContents.Add(ScenarioType.ReceiveFromCustomer_Transaction, ReceiveFromCustomer_Transaction);
        }
    }
}
