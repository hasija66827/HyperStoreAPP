using Models;
using Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public sealed class ProductPricingDetailViewModel : ValidatableBindableBase
    {
        public decimal? DiscountAmount
        {
            get { return this.DisplayPrice * (this.DiscountPer) / 100; }

        }
        public decimal? SubTotal
        {
            get { return this.DisplayPrice - this.DiscountAmount; }
        }

        public decimal? SellingPrice
        {
            get { return this.SubTotal + this.TotalGSTAmount; }
        }

        public decimal? TotalGSTPer
        {
            get { return this.CGSTPer + this.SGSTPer; }
        }

        public decimal? TotalGSTAmount
        {
            get { return this.SubTotal * (this.TotalGSTPer) / 100; }
        }

       
        private decimal? _displayPrice;
        [Required(ErrorMessage = "DisplayPrice is Invalid", AllowEmptyStrings = false)]
        [Range(0, float.MaxValue)]
        public decimal? DisplayPrice
        {
            get { return this._displayPrice; }
            set
            {
                SetProperty(ref _displayPrice, value);
                this.OnPropertyChanged(nameof(DiscountAmount));
                this.OnPropertyChanged(nameof(SubTotal));
                this.OnPropertyChanged(nameof(SellingPrice));
            }
        }

        private decimal? _discountPer;
        [DefaultValue(0)]
        [Range(0, 100, ErrorMessage ="Try Discount % in Range (0, 100).")]
        [Required(ErrorMessage = "Discount Percentage is Invalid", AllowEmptyStrings = false)]
        public decimal? DiscountPer
        {
            get { return this._discountPer; }
            set
            {
                SetProperty(ref _discountPer, value);
                decimal f = (decimal)Convert.ToDouble(value);
                this.OnPropertyChanged(nameof(DiscountAmount));
                this.OnPropertyChanged(nameof(SubTotal));
                this.OnPropertyChanged(nameof(TotalGSTAmount));
                this.OnPropertyChanged(nameof(SellingPrice));
            }
        }

        private decimal? _CGSTPer;
        [DefaultValue(0)]
        [Range(0, 100, ErrorMessage = "Try CGST % in Range (0, 100).")]
        [Required(ErrorMessage = "CGST Percentage is Invalid", AllowEmptyStrings = false)]
        public decimal? CGSTPer
        {
            get { return this._CGSTPer; }
            set
            {
                SetProperty(ref _CGSTPer, value);
                this.OnPropertyChanged(nameof(TotalGSTPer));
                this.OnPropertyChanged(nameof(TotalGSTAmount));
                this.OnPropertyChanged(nameof(SellingPrice));
            }
        }

        private decimal? _SGSTPer;
        [DefaultValue(0)]
        [Range(0, 100, ErrorMessage = "Try SGST % in Range (0, 100).")]
        [Required(ErrorMessage = "SGST Percentage is Invalid", AllowEmptyStrings = false)]
        public decimal? SGSTPer
        {
            get { return this._SGSTPer; }
            set
            {
                SetProperty(ref _SGSTPer, value);
                this.OnPropertyChanged(nameof(TotalGSTPer));
                this.OnPropertyChanged(nameof(TotalGSTAmount));
                this.OnPropertyChanged(nameof(SellingPrice));
            }
        }
    }
}
