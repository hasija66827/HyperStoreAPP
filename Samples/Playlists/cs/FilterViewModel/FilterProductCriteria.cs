using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class FilterProductCriteria
    {
        public IRange<decimal> DiscountPerRange { get; set; }
        public IRange<Int32> QuantityRange { get; set; }
        public bool IncludeDeficientItemsOnly { get; set; }

        public FilterProductCriteria(IRange<decimal> discounPerRange, IRange<Int32> quantityRange, bool? isChecked)
        {
            this.DiscountPerRange = discounPerRange;
            this.QuantityRange = quantityRange;
            this.IncludeDeficientItemsOnly = isChecked ?? false;
        }
    }

    public class IRange<T>
    {
        public T LB { get; set; }
        public T UB { get; set; }
        public IRange(T lb, T ub)
        {
            LB = lb;
            UB = ub;
        }
    }
}
