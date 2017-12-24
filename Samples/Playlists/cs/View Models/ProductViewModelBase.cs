using HyperStoreService.CustomModels;
using HyperStoreServiceAPP.DTO;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class ProductViewModelBase : Product
    {
        public decimal? DiscountAmount
        {
            get { return this.MRP * (this.DiscountPer) / 100; }
        }
        public decimal? ValueIncTax
        {
            get { return this.MRP - this.DiscountAmount; }
        }

        public virtual decimal? TotalGSTPer
        {
            get { return this.CGSTPer + this.SGSTPer; }
        }

        public decimal? ValueExcTax { get { return ValueIncTax * 100 / (100 + CGSTPer + SGSTPer); } }

        public virtual decimal? TotalGSTAmount
        {
            get { return this.ValueIncTax - ValueExcTax; }
        }

        //TODO: place it in another viewmodel
        public string FormattedNameQuantity
        {
            get
            {
                if (this.TotalQuantity != null)
                    return this.Name + " (" + this.TotalQuantity + ")";
                else
                    return this.Name;
            }
        }

        public MapDay_ProductEstConsumption MapDay_ProductEstConsumption { get; set; }
        public DateTime? ProductExtinctionDate { get; set; }
        public string ProductExtinctionDays
        {
            get
            {
                if (ProductExtinctionDate == null && this.TotalQuantity == null)
                    return "NA";
                if (ProductExtinctionDate == null && this.TotalQuantity != null)
                    return "Not Computed";
                var productExtinctionDays = (this.ProductExtinctionDate?.Date - DateTime.Now.Date)?.TotalDays;
                if (productExtinctionDays == -1)
                    return "Yesterday";
                if (productExtinctionDays == 0)
                    return "Today";
                else if (productExtinctionDays == 1)
                    return "Tomorrow";
                else
                    return "In " + productExtinctionDays + " days";
            }
        }
        public string ProductUnitConsumedPerWeek
        {
            get
            {
                string productUnitConsumedPerWeek = "";
                if (MapDay_ProductEstConsumption != null)
                {
                    var unitsConsumed = MapDay_ProductEstConsumption.ProductEstConsumption.Sum(p => p.Value);
                    productUnitConsumedPerWeek = Math.Round(unitsConsumed) + " units per week";
                }
                else
                    productUnitConsumedPerWeek = "Not Computed";
                return productUnitConsumedPerWeek;
            }
        }

        public ProductViewModelBase() { }

        public ProductViewModelBase(ProductInsight productInsight)
        {
            this.ProductExtinctionDate = productInsight.ProductExtinctionDate;
            this.MapDay_ProductEstConsumption = productInsight.MapDay_ProductEstConsumption;
            var parent = productInsight.Product;
            foreach (PropertyInfo prop in typeof(Product).GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }

        public ProductViewModelBase(Product parent)
        {
            this.ProductExtinctionDate = null;
            this.MapDay_ProductEstConsumption = null;
            foreach (PropertyInfo prop in typeof(Product).GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
