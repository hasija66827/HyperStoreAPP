﻿using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class FilterOrderViewModel
    {
        [Required]
        [DateRange(ErrorMessage = "{0} is invalid, lb>ub")]
        public IRange<DateTime> OrderDateRange { get; set; }
        public FilterOrderViewModel()
        {
            OrderDateRange = new IRange<DateTime>(DateTime.Now.AddDays(-15), DateTime.Now);
        }
    }
}
