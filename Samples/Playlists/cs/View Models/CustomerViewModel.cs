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
        public virtual string Address { get; set; }
        public string GSTIN { get; set; }
        public virtual string MobileNo { get; set; }
        public virtual string Name { get; set; }
        public virtual float WalletBalance { get; set; }
        public CustomerViewModel()
        {
            this.CustomerId = Guid.NewGuid();
            this.Address = "";
            this.MobileNo = "";
            this.Name = "";
            this.GSTIN = "";
            this.WalletBalance = 0;
        }

        public CustomerViewModel(DatabaseModel.Customer customer)
        {
            this.CustomerId = customer.CustomerId;
            this.Address = customer.Address;
            this.GSTIN = customer.GSTIN;
            this.MobileNo = customer.MobileNo;
            this.Name = customer.Name;
            this.WalletBalance = customer.WalletBalance;
        }

        /// <summary>
        /// This poperty is used by CustomerASBCC for its display member property of SearchBox.
        /// </summary>
        public string Customer_MobileNo_Address
        {
            get
            { return string.Format("{0}({1})", this.MobileNo, this.Address); }
        }

    }
}
