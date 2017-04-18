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
        public string CustomerMobileNo { get; set; }
        public float BillAmount { get; set; }
        public string OrderDate
        {
            get
            {
                var formatter = new DateTimeFormatter("hour minute");
                return formatter.Format(orderDate);
            }
        }
        public DateTime orderDate;

        public string Text { get; set; }
        public ItemViewModel()
        {
        }

        public static ItemViewModel FromItem(Item item)
        {
            var viewModel = new ItemViewModel();
            viewModel.Text = "hasija Rocks";
            viewModel.CustomerMobileNo = item.CustomerMobileNo;
            viewModel.BillAmount = item.BillAmount;
            viewModel.orderDate = item.orderDate;
            return viewModel;
        }
    }
}
