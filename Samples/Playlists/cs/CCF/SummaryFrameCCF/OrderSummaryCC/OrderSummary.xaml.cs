﻿using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderSummary : Page
    {
        private OrderSummaryViewModel _OrderSummaryViewModel { get; set; }
        public OrderSummary()
        {
            this.InitializeComponent();
            this._OrderSummaryViewModel = new OrderSummaryViewModel();
            OrderCCF.Current.OrderListUpdatedEvent += Current_SupplierOrderListUpdatedEvent;
        }

        private void Current_SupplierOrderListUpdatedEvent(IEnumerable<Order> supplierOrders)
        {
            this._OrderSummaryViewModel.TotalBillAmount = supplierOrders.Sum(so => so.BillAmount);
            this._OrderSummaryViewModel.TotalPayedAmount = supplierOrders.Sum(so => so.PayedAmount);
            this._OrderSummaryViewModel.TotalSettledAmount = supplierOrders.Sum(so => so.SettledPayedAmount);
            this._OrderSummaryViewModel.OnAllPropertyChanged();
        }
    }
}
