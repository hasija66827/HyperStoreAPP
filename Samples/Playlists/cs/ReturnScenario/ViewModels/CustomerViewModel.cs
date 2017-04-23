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
        public string MobileNo { get; set; }
        public CustomerViewModel(string mobileNo, string address)
        {
            this.MobileNo = mobileNo;
            this.Address = address;
        }
        public CustomerViewModel() { }
        public string Address { get; set; }
        public string Customer_MobileNo_Address { get { return string.Format("{0}({1})", this.MobileNo, this.Address); } }
    }
}
