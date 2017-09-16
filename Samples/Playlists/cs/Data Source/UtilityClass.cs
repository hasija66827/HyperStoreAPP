using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    using SDKTemplate.SignUp;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    namespace DTO
    {
        public class PersonalInformationDTO
        {
            [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
            [MaxLength(9, ErrorMessage = "Try first name with atmost 9 charecters.")]
            [RegularExpression(@"[a-zA-Z]{1,9}[\s]{0,1}", ErrorMessage = "First name is Invalid")]
            public string FirstName { get; set; }

            [MaxLength(9, ErrorMessage = "Try last name with atmost 9 charecters.")]
            [RegularExpression(@"[a-zA-Z]{1,9}", ErrorMessage = "Last name is Invalid")]
            public string LastName { get; set; }

            [EmailAddress(ErrorMessage = "Email Id is Invalid")]
            public string EmailId { get; set; }

            public DateTime DateOfBirth { get; set; }

            [RegularExpression(@"[987]\d{9}", ErrorMessage = "{0} is Invalid.")]
            public string MobileNo { get; set; }

            [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
            [DataType(DataType.Password)]
            [MinLength(6, ErrorMessage = "Short passwords are easy to guess. Try one with at least 6 characters.")]
            public string Password { get; set; }

        }

        public class BusinessInformationDTO
        {
            [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
            [MaxLength(25, ErrorMessage = "Try Business name with atmost 25 charecters.")]
            [RegularExpression(@"[a-zA-Z\s]{1,25}", ErrorMessage = "Business name is Invalid")]
            public string BusinessName { get; set; }

            [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
            [MaxLength(25, ErrorMessage = "Try address with atmost 25 charecters.")]
            public string AddressLine { get; set; }

            [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
            [RegularExpression(@"\d{2}[a-zA-Z]{5}\d{4}[a-zA-Z]{1}\d{1}[zZ][a-zA-Z0-9]", ErrorMessage = "{0} is Invalid.")]
            public string GSTIN { get; set; }

            [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
            [MaxLength(15, ErrorMessage = "Try city with atmost 15 charecters.")]
            public string City { get; set; }

            [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
            [MaxLength(6, ErrorMessage = "Try pincode with 6 digits.")]
            [RegularExpression(@"[1-9]\d{5}", ErrorMessage = "Try pincode with 6 digits.")]
            public string PinCode { get; set; }

            public string BusinessType { get; set; }

            public string State { get; set; }

            public Cordinates Cordinates { get; set; }
        }


        public class UserDTO
        {
            public PersonalInformationDTO PI { get; set; }
            public BusinessInformationDTO BI { get; set; }
        }

        public class CustomerDTO
        {
            public string Address { get; set; }
            public string GSTIN { get; set; }
            [Required]
            [RegularExpression(@"[987]\d{9}")]
            public string MobileNo { get; set; }
            [Required]
            public string Name { get; set; }
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
        public class ProductConsumptionTrendDTO
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
            public decimal? QuantityConsumed { get; set; }
        }

        public class CustomerBillingSummaryDTO
        {
            public decimal TotalQuantity { get; set; }
            public int TotalItems { get; set; }
            public decimal CartAmount { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal Tax { get; set; }
            public decimal PayAmount { get; set; }
        }

        public class SupplierBillingSummaryDTO
        {
            public decimal BillAmount { get; set; }

            public int TotalItems { get; set; }

            public decimal TotalQuantity { get; set; }
        }

        public class CustomerOrderDTO
        {
            [Required]
            public List<ProductConsumedDTO> ProductsConsumed { get; set; }

            [Required]
            public CustomerBillingSummaryDTO CustomerBillingSummary { get; set; }

            [Required]
            public Guid? CustomerId { get; set; }

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
            public decimal? CGSTPer { get; set; }
            [Required]
            public string Code { get; set; }
            [Required]
            public decimal? DisplayPrice { get; set; }
            [Required]
            public decimal? DiscountPer { get; set; }
            [Required]
            public string Name { get; set; }
            public Int32 RefillTime { get; set; }
            public decimal? SGSTPer { get; set; }
            public decimal Threshold { get; set; }
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
                var quantityRange = value as IRange<decimal?>;
                return (quantityRange.LB >= 0 && quantityRange.LB <= quantityRange.UB);
            }
        }

        public sealed class DiscountPerRangeAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                var discountPerRange = value as IRange<decimal?>;
                bool valid = (discountPerRange.LB <= discountPerRange.UB && discountPerRange.LB >= 0 && discountPerRange.UB <= 100);
                return valid;
            }
        }

        public class ProductFilterCriteriaDTO
        {
            public Guid? ProductId { get; set; }
            public List<Guid?> TagIds { get; set; }
            [Required]
            public ProductFilterQDTDTO FilterProductQDT { get; set; }
        }

        public class ProductFilterQDTDTO
        {
            [Required]
            [DiscountPerRange]
            public IRange<decimal?> DiscountPerRange { get; set; }

            [Required]
            [QuantityRange]
            public IRange<decimal?> QuantityRange { get; set; }

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
            public decimal? QuantityPurchased { get; set; }

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
            public DateTime? DueDate { get; set; }

            public SupplierBillingSummaryDTO SupplierBillingSummary { get; set; }

            [Required]
            public decimal? PayingAmount { get; set; }

            [Required]
            [Range(0, 100)]
            public decimal IntrestRate { get; set; }
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

        #region CustomerTransactionDTO
        public class CustomerTransactionFilterCriteriaDTO
        {
            [Required]
            public Guid? CustomerId { get; set; }
        }

        public class CustomerTransactionDTO
        {
            [Required]
            public Guid? CustomerId { get; set; }

            [Required]
            public bool? IsCredit { get; set; }

            [Required]
            [Range(0, 98765432198765)]
            public decimal? TransactionAmount { get; set; }

            public string Description { get; set; }
        }
        #endregion
        #region SupplierTransaction
        public class SupplierTransactionFilterCriteriaDTO
        {
            [Required]
            public Guid? SupplierId { get; set; }
        }

        public class SupplierTransactionDTO
        {
            [Required]
            public bool? IsCredit { get; set; }

            [Required]
            public Guid? SupplierId { get; set; }

            [Required]
            [Range(0, 98765432198765)]
            public decimal? TransactionAmount { get; set; }

            public string Description { get; set; }
        }
        #endregion

        #region Supplier Controller
        public class SupplierFilterCriteriaDTO
        {
            public IRange<decimal> WalletAmount { get; set; }
            public Guid? SupplierId { get; set; }
        }
        #endregion

        #region TagController
        public class TagDTO
        {
            [Required]
            [StringLength(24)]
            public string TagName { get; set; }
        }
        #endregion
    }
}
