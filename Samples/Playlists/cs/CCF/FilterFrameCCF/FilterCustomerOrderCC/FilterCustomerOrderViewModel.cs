using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public sealed class FilterCustomerOrderViewModel
    {
        [Required]
        [DateRange(ErrorMessage = "{0} is invalid, lb>ub")]
        public IRange<DateTime> OrderDateRange { get; set; }
        public FilterCustomerOrderViewModel()
        {
            OrderDateRange = new IRange<DateTime>(DateTime.Now.AddDays(-15), DateTime.Now.AddDays(1));
        }
    }
}
