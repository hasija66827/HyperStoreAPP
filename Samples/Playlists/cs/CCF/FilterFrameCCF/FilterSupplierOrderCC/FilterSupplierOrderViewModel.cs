﻿using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public sealed class FilterSupplierOrderViewModel
    {
        [Required]
        [DateRange(ErrorMessage = "{0} is invalid, lb>ub")]
        public IRange<DateTime> OrderDateRange { get; set; }
        [Required]
        [DateRange(ErrorMessage = "{0} is invalid, lb>ub")]
        public IRange<DateTime> DueDateRange { get; set; }
        
        public bool? IncludePartiallyPaidOrdersOnly { get; set; }
        
        public FilterSupplierOrderViewModel()
        {
            OrderDateRange = new IRange<DateTime>(DateTime.Now, DateTime.Now.AddDays(1));
            DueDateRange = new IRange<DateTime>(DateTime.Now, DateTime.Now.AddMonths(2));
            IncludePartiallyPaidOrdersOnly = false;
        }
    }
}
