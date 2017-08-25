using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    namespace DTO
    {
        public class CustomerDTO
        {
            public string Address { get; set; }
            public string GSTIN { get; set; }
            [Required]
            [RegularExpression(@"[987]\d{9}")]
            public string MobileNo { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public decimal? WalletBalance { get; set; }
        }

        public class SupplierDTO
        {
            public string Address { get; set; }
            public string GSTIN { get; set; }
            [Required]
            [RegularExpression(@"[987]\d{9}")]
            public string MobileNo { get; set; }
            [Required]
            public string Name { get; set; }
        }

        #region CustomerPurchaseTrend
        public class CustomerPurchaseTrendDTO
        {
            [Required]
            public Guid? CustomerId { get; set; }
            [Required]
            public int? MonthsCount { get; set; }
        }
        #endregion

        #region ProductConsumption Trend
        public class ProductTrendDTO
        {
            [Required]
            public Guid? ProductId { get; set; }
            [Required]
            public int? MonthsCount { get; set; }
        }
        #endregion

        #region CustomerOrder Controller
        public sealed class DateRangeAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value == null)
                    return false;
                var dateRange = value as IRange<DateTime>;
                return dateRange.LB <= dateRange.UB;
            }
        }

        public class CustomerOrderFilterCriteriaDTO
        {
            public Guid? CustomerId { get; set; }
            public string CustomerOrderNo { get; set; }

            [Required]
            [DateRange(ErrorMessage = "{0} is invalid, lb>ub")]
            public IRange<DateTime> OrderDateRange { get; set; }
        }

        public class ProductConsumedDTO
        {
            [Required]
            public Guid? ProductId { get; set; }

            [Required]
            [Range(0, float.MaxValue)]
            public float? QuantityConsumed { get; set; }
        }

        public class CustomerOrderDTO
        {
            [Required]
            public List<ProductConsumedDTO> ProductsConsumed { get; set; }

            [Required]
            public Guid? CustomerId { get; set; }

            [Required]
            public decimal? BillAmount { get; set; }

            [Required]
            public decimal? DiscountedAmount { get; set; }

            [Required]
            public bool? IsPayingNow { get; set; }

            [Required]
            public bool? IsUsingWallet { get; set; }

            [Required]
            public decimal? PayingAmount { get; set; }
        }
        #endregion

        #region Product Controller
        public class ProductDTO
        {
            public float? CGSTPer { get; set; }
            [Required]
            public string Code { get; set; }
            [Required]
            public decimal? DisplayPrice { get; set; }
            public float DiscountPer { get; set; }
            [Required]
            public string Name { get; set; }
            public Int32 RefillTime { get; set; }
            public float? SGSTPer { get; set; }
            public Int32 Threshold { get; set; }
            public List<Guid?> TagIds { get; set; }
        }

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
        public sealed class QuantityRangeAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                var quantityRange = value as IRange<float?>;
                return (quantityRange.LB >= 0 && quantityRange.LB <= quantityRange.UB);
            }
        }

        public sealed class DiscountPerRangeAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                var discountPerRange = value as IRange<float?>;
                bool valid = (discountPerRange.LB <= discountPerRange.UB && discountPerRange.LB >= 0 && discountPerRange.UB <= 100);
                return valid;
            }
        }

        public class FilterProductCriteriaDTO
        {
            public Guid? ProductId { get; set; }
            public List<Guid?> TagIds { get; set; }
            [Required]
            public FilterProductQDTDTO FilterProductQDT { get; set; }
        }

        public class FilterProductQDTDTO
        {
            [Required]
            [DiscountPerRange]
            public IRange<float?> DiscountPerRange { get; set; }

            [Required]
            [QuantityRange]
            public IRange<float?> QuantityRange { get; set; }

            [Required]
            public bool? IncludeDeficientItemsOnly { get; set; }
        }
        #endregion

        #region SupplierOrder Controller
        public class ProductPurchasedDTO
        {
            [Required]
            public Guid? ProductId { get; set; }

            [Required]
            [Range(0, float.MaxValue)]
            public float? QuantityPurchased { get; set; }

            [Required]
            public decimal? PurchasePricePerUnit { get; set; }
        }

        public class SupplierOrderDTO
        {
            [Required]
            public List<ProductPurchasedDTO> ProductsPurchased { get; set; }

            [Required]
            public Guid? SupplierId { get; set; }

            [Required]
            public decimal? BillAmount { get; set; }

            [Required]
            public decimal? PaidAmount { get; set; }

            [Required]
            public DateTime? DueDate { get; set; }

            [Required]
            [Range(0, 100)]
            public float IntrestRate { get; set; }
        }

        public class SupplierOrderFilterCriteriaDTO
        {
            public Guid? SupplierId { get; set; }

            public string SupplierOrderNo { get; set; }

            [Required]
            public bool? PartiallyPaidOrderOnly { get; set; }

            [Required]
            [DateRange(ErrorMessage = "{0} is invalid, lb>ub")]
            public IRange<DateTime> OrderDateRange { get; set; }

            [Required]
            [DateRange(ErrorMessage = "{0} is invalid, lb>ub")]
            public IRange<DateTime> DueDateRange { get; set; }
        }
        #endregion

        #region Transaction
        public class TransactionFilterCriteriaDTO
        {
            [Required]
            public Guid? SupplierId { get; set; }
        }

        public class TransactionDTO
        {
            [Required]
            public bool? IsCredit { get; set; }
            [Required]
            public Guid? SupplierId { get; set; }
            [Required]
            public decimal? TransactionAmount { get; set; }
        }
        #endregion

        #region Supplier Controller
        public class SupplierFilterCriteriaDTO
        {
            public IRange<decimal> WalletAmount { get; set; }
            public Guid? SupplierId { get; set; }
        }
        #endregion
    }
}
