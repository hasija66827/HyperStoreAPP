using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperStoreServiceAPP.DTO
{
    public class IRange<T>
    {
        [Required]
        public T LB { get; set; }
        [Required]
        public T UB { get; set; }
        public IRange(T lb, T ub)
        {
            LB = lb;
            UB = ub;
        }
    }
}
