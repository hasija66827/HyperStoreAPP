using HyperStoreService.CustomModels;
using Models;
using SDKTemplate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperStoreServiceAPP.DTO
{
    public class ProductDTO
    {
        [Required]
        [Range(0d, 100)]
        public decimal? CGSTPer { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public decimal? MRP { get; set; }

        [Required]
        [Range(0d, 100)]
        public decimal? DiscountPer { get; set; }

        [Required]
        public string Name { get; set; }

        public Int32? HSN { get; set; }

        [Required]
        public bool? IsNonInventoryProduct { get; set; }

        [Required]
        [Range(0d, 100)]
        public decimal? SGSTPer { get; set; }

        public List<Guid?> TagIds { get; set; }

    }

    public class ProductFilterCriteriaDTO
    {
        public Guid? ProductId { get; set; }
        public List<Guid?> TagIds { get; set; }
        public FilterProductQDT FilterProductQDT { get; set; }
    }



    public class FilterProductQDT
    {
        [Required]
        public bool? ShowInventoryProductsOnly { get; set; }

        [Required]
        [DiscountPerRange]
        public IRange<decimal?> DiscountPerRange { get; set; }

        [Required]
        [QuantityRange]
        public IRange<float?> QuantityRange { get; set; }

        [Required]
        [ConsumptionDayRange]
        public IRange<int?> ConsumptionDayRange { get; set; }
    }

    public sealed class QuantityRangeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            var quantityRange = value as IRange<float?>;
            return (quantityRange.LB <= quantityRange.UB);
        }
    }

    public sealed class ConsumptionDayRangeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            var consumptionDayRange = value as IRange<int?>;
            return (consumptionDayRange.LB <= consumptionDayRange.UB);
        }
    }

    public sealed class DiscountPerRangeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            var discountPerRange = value as IRange<decimal?>;
            bool valid = (discountPerRange.LB <= discountPerRange.UB && discountPerRange.LB >= 0 && discountPerRange.UB <= 100);
            return valid;
        }
    }

    public class ProductInsight
    {
        public Product Product { get; set; }
        public MapDay_ProductEstConsumption MapDay_ProductEstConsumption { get; set; }
        public DateTime? ProductExtinctionDate { get; set; }
    }
}
