using Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class ProViewModel : ValidatableBindableBase
    {
        private decimal? _MRP;
        [Required(ErrorMessage = "MRP/Unit is required.", AllowEmptyStrings = false)]
        public decimal? MRP { get { return _MRP; } set { SetProperty(ref _MRP, value); this.OnALLPropertyChanged(); } }

        private decimal? _CGSTPer;
        [Range(0, 100, ErrorMessage = "Try CGST % in Range (0, 100).")]
        [Required(ErrorMessage = "CGST Percentage is required.", AllowEmptyStrings = false)]
        public decimal? CGSTPer { get { return _CGSTPer; } set { SetProperty(ref _CGSTPer, value); this.OnALLPropertyChanged(); } }

        private decimal? _SGSTPer;
        [Range(0, 100, ErrorMessage = "Try SGST % in Range (0, 100).")]
        [Required(ErrorMessage = "SGST Percentage is required.", AllowEmptyStrings = false)]
        public decimal? SGSTPer { get { return this._SGSTPer; } set { SetProperty(ref _SGSTPer, value); this.OnALLPropertyChanged(); } }

        private decimal? _DisPer;
        [Range(0, 100, ErrorMessage = "Try Discount % in Range (0, 100).")]
        [Required(ErrorMessage = "Discount Percentage is required.", AllowEmptyStrings = false)]
        public decimal? DiscPer { get { return this._DisPer; } set { SetProperty(ref _DisPer, value); this.OnALLPropertyChanged(); } }

        public decimal? DiscountAmount { get { return MRP * DiscPer / 100; } }
        public decimal? ValueIncTax { get { return MRP - DiscountAmount; } }
        public decimal? ValueExcTax { get { return ValueIncTax * 100 / (100 + CGSTPer + SGSTPer); } }
        public decimal? TotalGSTAmount { get { return ValueIncTax - ValueExcTax; } }
        public decimal? CGSTAmount { get { return (CGSTPer * TotalGSTAmount) / (CGSTPer + SGSTPer); } }
        public decimal? SGSTAmount { get { return TotalGSTAmount - CGSTAmount; } }

        public void OnALLPropertyChanged()
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.OnPropertyChanged(nameof(MRP));
            this.OnPropertyChanged(nameof(DiscountAmount));
            this.OnPropertyChanged(nameof(ValueIncTax));
            this.OnPropertyChanged(nameof(ValueExcTax));
            this.OnPropertyChanged(nameof(TotalGSTAmount));
            this.OnPropertyChanged(nameof(CGSTAmount));
            this.OnPropertyChanged(nameof(SGSTAmount));
        }
    }
}
