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
        private decimal? _MRPDec { get { return Utility.TryToConvertToDecimal(this._MRP); } }
        private string _MRP;
        [Required(ErrorMessage = "MRP/Unit is required.", AllowEmptyStrings = false)]
        [Range(0, float.MaxValue, ErrorMessage = "Try MRP in numeric range.")]
        public string MRP
        {
            get { return _MRP; }
            set
            {
                SetProperty(ref _MRP, value);
                this.OnALLPropertyChanged();
            }
        }

        private decimal? _CGSTPerDec { get { return Utility.TryToConvertToDecimal(this._CGSTPer); } }
        private string _CGSTPer;
        [Required(ErrorMessage = "CGST Percentage is required.", AllowEmptyStrings = false)]
        [Range(0d, 100, ErrorMessage = "Try CGST % in Range (0, 100).")]
        public string CGSTPer
        {
            get { return _CGSTPer; }
            set
            {
                SetProperty(ref _CGSTPer, value);
                this.OnALLPropertyChanged();
            }
        }

        private decimal? _SGSTPerDec { get { return Utility.TryToConvertToDecimal(this._SGSTPer); } }
        private string _SGSTPer;
        [Required(ErrorMessage = "SGST Percentage is required.", AllowEmptyStrings = false)]
        [Range(0d, 100, ErrorMessage = "Try SGST % in Range (0, 100).")]
        public string SGSTPer
        {
            get { return this._SGSTPer; }
            set
            {
                SetProperty(ref _SGSTPer, value);
                this.OnALLPropertyChanged();
            }
        }

        private decimal? _DisPerDec { get { return Utility.TryToConvertToDecimal(this._DisPer); } }
        private string _DisPer;
        [Required(ErrorMessage = "Discount Percentage is required.", AllowEmptyStrings = false)]
        [Range(0d, 100, ErrorMessage = "Try Discount % in Range (0, 100).")]
        public string DiscPer
        {
            get { return this._DisPer; }
            set
            {
                SetProperty(ref _DisPer, value);
                this.OnALLPropertyChanged();
            }
        }

        public decimal? MRPPerUnit { get { return this._MRPDec; } }
        public decimal? DiscountAmount { get { return _MRPDec * _DisPerDec / 100; } }
        public decimal? ValueIncTax { get { return _MRPDec - DiscountAmount; } }
        public decimal? ValueExcTax { get { return ValueIncTax * 100 / (100 + _CGSTPerDec + _SGSTPerDec); } }
        public decimal? TotalGSTAmount { get { return ValueIncTax - ValueExcTax; } }
        public decimal? TotalGSTPer { get { return this._CGSTPerDec + this._SGSTPerDec; } }
        public decimal? CGSTAmount
        {
            get
            {
                if (TotalGSTPer == 0) return 0;
                else return (_CGSTPerDec * TotalGSTAmount) / TotalGSTPer;
            }
        }
        public decimal? SGSTAmount { get { return TotalGSTAmount - CGSTAmount; } }

        public void OnALLPropertyChanged()
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.OnPropertyChanged(nameof(MRPPerUnit));
            this.OnPropertyChanged(nameof(DiscountAmount));
            this.OnPropertyChanged(nameof(ValueIncTax));
            this.OnPropertyChanged(nameof(ValueExcTax));
            this.OnPropertyChanged(nameof(TotalGSTAmount));
            this.OnPropertyChanged(nameof(CGSTAmount));
            this.OnPropertyChanged(nameof(SGSTAmount));
        }

    }
}
