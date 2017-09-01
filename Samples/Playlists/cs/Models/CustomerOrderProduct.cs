using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Models
{
    public class TCustomerOrderProduct
    {
        public Guid CustomerOrderProductId { get; set; }
        public decimal DisplayCostSnapShot { get; set; }
        public decimal DiscountPerSnapShot { get; set; }
        public decimal CGSTPerSnapShot { get; set; }
        public decimal SGSTPerSnapshot { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal QuantityConsumed { get; set; }
        public decimal NetValue { get; set; }
        /*
         * cop.DisplayCostSnapShot
                ((100 - cop.DiscountPerSnapShot) * (100 + cop.CGSTPerSnapShot + cop.SGSTPerSnapshot) / 10000
                * cop.QuantityConsumed)
         */
        public TCustomerOrderProduct() { }

        public Guid CustomerOrderId { get; set; }
        public TCustomerOrder CustomerOrder { get; set; }

        [Required]
        public Guid? ProductId { get; set; }
        public TProduct Product { get; set; }
    }
}