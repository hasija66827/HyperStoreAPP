using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class ProViewModel : INotifyPropertyChanged
    {
        private decimal? _MRP;
        public decimal? MRP { get { return _MRP; } set { _MRP = value; this.OnALLPropertyChanged(); } }

        private decimal? _CGSTPer;
        public decimal? CGSTPer { get { return _CGSTPer; } set { _CGSTPer = value; this.OnALLPropertyChanged(); } }

        private decimal? _SGSTPer;
        public decimal? SGSTPer { get { return this._SGSTPer; } set { this._SGSTPer = value; this.OnALLPropertyChanged(); } }

        private decimal? _DisPer;
        public decimal? DiscPer { get { return this._DisPer; } set { this._DisPer = value; this.OnALLPropertyChanged(); } }

        public decimal? DiscountAmount { get { return MRP * DiscPer / 100; } }
        public decimal? ValueIncTax { get { return MRP - DiscountAmount; } }
        public decimal? ValueExcTax { get { return ValueIncTax * 100 / (100 + CGSTPer + SGSTPer); } }
        public decimal? TotalGSTAmount { get { return ValueIncTax - ValueExcTax; } }
        public decimal? CGSTAmount { get { return (CGSTPer * TotalGSTAmount) / (CGSTPer + SGSTPer); } }
        public decimal? SGSTAmount { get { return TotalGSTAmount - CGSTAmount; } }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnALLPropertyChanged()
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(DiscountAmount)));
            this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(ValueIncTax)));
            this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(ValueExcTax)));
            this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(TotalGSTAmount)));
            this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(CGSTAmount)));
            this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(SGSTAmount)));
        }
    }
}
