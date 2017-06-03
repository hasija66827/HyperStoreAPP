using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModel;
using Windows.Globalization.DateTimeFormatting;
using SDKTemp.Data;
namespace SDKTemp.ViewModel
{
    public class CustomerViewModel
    {
        public Guid CustomerId;
        public virtual string Name { get; set; }
        public virtual string MobileNo { get; set; }
        public virtual string Address { get; set; }
        public virtual float WalletBalance { get; set; }
        public virtual bool IsVerifiedCustomer { get; set; }
        public CustomerViewModel()
        {
            this.CustomerId = Guid.NewGuid();
            this.Name = "";
            this.MobileNo = "";
            this.Address = "";
            this.WalletBalance = 0;
            this.IsVerifiedCustomer = false;
        }
        public CustomerViewModel(string mobileNo):this()
        {
            this.MobileNo = mobileNo;
            this.Name += mobileNo;
            this.IsVerifiedCustomer = true;
        }
        public CustomerViewModel(Guid customerId, string name,
            string mobileNo, string address, float walletBalance, bool isVerifiedCustomer)
        {
            this.CustomerId = customerId;
            this.Name = name;
            this.MobileNo = mobileNo;
            this.Address = address;
            this.WalletBalance = walletBalance;
            this.IsVerifiedCustomer = IsVerifiedCustomer;
        }
        
        public string Customer_MobileNo_Address
        { get { return string.Format("{0}({1})", this.MobileNo, this.Address); } }
    }
}
