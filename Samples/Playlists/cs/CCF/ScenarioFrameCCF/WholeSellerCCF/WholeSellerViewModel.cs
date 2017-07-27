using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class WholeSellerViewModel
    {
        private Guid _wholeSellerId;
        public virtual Guid WholeSellerId { get { return this._wholeSellerId; } set { this._wholeSellerId = value; } }

        private string _address;
        public virtual string Address { get { return this._address; } set { this._address = value; } }

        private string _gstin;
        public string GSTIN { get { return this._gstin; } set { this._gstin = value; } }

        private string _mobileNo;
        public virtual string MobileNo { get { return this._mobileNo; } set { this._mobileNo = value; } }

        private string _name;
        public virtual string Name { get { return this._name; } set { this._name = value; } }

        private float _walletBalance;
        public virtual float WalletBalance { get { return this._walletBalance; } set { this._walletBalance = value; } }

        public WholeSellerViewModel()
        {
            this._wholeSellerId = Guid.NewGuid();
            this._address = "";
            this._mobileNo = "";
            this._name = "";
            this._walletBalance = 0;
        }

        public WholeSellerViewModel(DatabaseModel.WholeSeller wholeSeller)
        {
            this._wholeSellerId = wholeSeller.WholeSellerId;
            this._address = wholeSeller.Address;
            this._gstin = wholeSeller.GSTIN;
            this._mobileNo = wholeSeller.MobileNo;
            this._name = wholeSeller.Name;
            this._walletBalance = wholeSeller.WalletBalance;
        }

        public string WholeSeller_MobileNo_Address
        {
            get { return string.Format("{0}({1})", this.MobileNo, this.Address); }
        }

        public string WholeSeller_Name_MobileNo
        {
            get { return string.Format("{0}\n{1}", this.Name, this.MobileNo); }
        }
    }
}
