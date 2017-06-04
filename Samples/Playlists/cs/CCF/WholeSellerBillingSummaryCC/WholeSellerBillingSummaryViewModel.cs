﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class WholeSellerBillingSummaryViewModel : INotifyPropertyChanged
    {
        private float _billAmount;
        public float BillAmount
        {
            get { return this._billAmount; }
            set
            {
                this._billAmount = value;
                this.OnPropertyChanged(nameof(BillAmount));
            }
        }
        private Int32 _totalQuantity;
        public Int32 TotalQuantity
        {
            get { return this._totalQuantity; }
            set
            {
                this._totalQuantity = value;
                this.OnPropertyChanged(nameof(TotalQuantity));
            }
        }
        public WholeSellerBillingSummaryViewModel()
        {
            _billAmount = 0;
            _totalQuantity = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
