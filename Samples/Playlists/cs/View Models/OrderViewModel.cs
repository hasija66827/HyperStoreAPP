﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModel;
using Windows.Globalization.DateTimeFormatting;
using SDKTemp.Data;
namespace SDKTemp.ViewModel
{
    public class OrderViewModel
    {
        public Guid CustomerOrderId { get; set; }
        public float BillAmount { get { return this._billAmount; } }
        private float _billAmount;

        public string CustomerMobileNo { get; set; }
        public float DiscountedBillAmount { get { return this._discountedBillAmount; } }
        private float _discountedBillAmount;
        public string FormattedOrderDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month hour minute");
                return formatter.Format(this._orderDate);
            }
        }
        public DateTime OrderDate
        { get { return this._orderDate; } }
        private DateTime _orderDate;
        private List<OrderDetailViewModel> _orderDetails;
        public List<OrderDetailViewModel> OrderDetails
        {
            get
            {
                if (this._orderDetails.Count == 0)
                {
                    this._orderDetails = OrderDataSource.RetrieveOrderDetails(this.CustomerOrderId);
                }
                return this._orderDetails;
            }
        }
        public OrderViewModel(Guid customerOrderId, float billAmount, string customerMobileNo,
            DateTime orderDate, float paidAmount)
        {
            this.CustomerOrderId = customerOrderId;
            this._billAmount = billAmount;
            this.CustomerMobileNo = customerMobileNo;
            this._orderDate = orderDate;
            this._discountedBillAmount = paidAmount;
            this._orderDetails = new List<OrderDetailViewModel>();
        }
    }
    public class OrderDetailViewModel : SDKTemplate.ProductViewModelBase
    {
        private Int32 _quantityPurchased;
        public Int32 QuantityPurchased { get; set; }
        private float _netValue;
        public float NetValue { get { return SDKTemplate.Utility.RoundInt32(this._netValue); } }
        public OrderDetailViewModel() : base()
        {
            this._quantityPurchased = 0;
        }
        public OrderDetailViewModel(Guid productId, string barCode, float discountPerSnapShot, 
            float displayPriceSnapshot, string name, int qtyPurchased) 
            : base(productId, barCode, name, displayPriceSnapshot, discountPerSnapShot,0,0)
        {
            this._quantityPurchased = qtyPurchased;
            this._netValue = this._sellingPrice * this._quantityPurchased;
        }
    }
}
