using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RecommendedProduct
    {
        public Guid? ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime LastOrderDate { get; set; }
    }
}
