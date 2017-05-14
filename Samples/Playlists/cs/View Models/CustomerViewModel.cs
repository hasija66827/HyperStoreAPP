using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModel;
using Windows.Globalization.DateTimeFormatting;
using MasterDetailApp.Data;
namespace MasterDetailApp.ViewModel
{
    public class CustomerViewModel
    {
        public Guid CustomerId;
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public float WalletBalance { get; set; }
        public bool IsVerifiedCustomer { get; set; }
        public CustomerViewModel()
        {
            this.CustomerId = Guid.NewGuid();
            this.Name = "m";
            this.MobileNo = "9987654321";
            this.Address = "aaaaaa";
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
