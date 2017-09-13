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
    public sealed class ProductFormViewModel : ValidatableBindableBase
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

        private string _code;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [RegularExpression(@"[1-9]\d{3,12}", ErrorMessage = "Try code with atleast 4 and atmost 13 digits.")]
        public string Code { get { return this._code; } set { SetProperty(ref _code, value); } }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [MaxLength(20, ErrorMessage = "Try name with atmost 20 charecters.")]
        [RegularExpression(@"[a-zA-Z]{1,20}", ErrorMessage = "Name is Invalid")]
        public string Name { get; set; }

        //TODO: add error text block.
        private Int32 _threshold;
        [Range(0, float.MaxValue)]
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        public Int32 Threshold { get { return this._threshold; } set { SetProperty(ref _threshold, value); } }

        private decimal? _displayPrice;
        [DefaultValue(0)]
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
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
        [Range(0, 100, ErrorMessage ="Discount Percentage is Invalid")]
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
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
        [Range(0, 100, ErrorMessage = "CGST Percentage is Invalid")]
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
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
        [Range(0, 100, ErrorMessage = "SGST Percentage is Invalid")]
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
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
