using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MasterDetailApp.Data;
using Windows.Globalization.DateTimeFormatting;

namespace MasterDetailApp.ViewModels
{
    public class ItemViewModel
    {
        public float BillAmount { get; set; }
        public string CustomerMobileNo { get; set; }
        public string OrderDate
        {
            get
            {
                var formatter = new DateTimeFormatter("hour minute");
                return formatter.Format(orderDate);
            }
        }
        public DateTime orderDate;
        public float PaidAmount { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public ItemViewModel()
        {
        }

        public static ItemViewModel FromItem(Order item)
        {
            var viewModel = new ItemViewModel();
            viewModel.BillAmount = item.BillAmount;
            viewModel.CustomerMobileNo = item.CustomerMobileNo;
            viewModel.orderDate = item.OrderDate;
            viewModel.PaidAmount = item.PaidAmount;
            viewModel.OrderDetails = item.OrderDetails;
            return viewModel;
        }
    }
}
