using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemp.ViewModel
{
    public class WholeSellerViewModel
    {
        private Guid _wholeSellerId;
        public virtual Guid WholeSellerId { get { return this._wholeSellerId; } set { this._wholeSellerId = value; } }
        private string _mobileNo;
        public virtual string MobileNo { get { return this._mobileNo; } set { this._mobileNo = value; } }
        private string _name;
        public virtual string Name { get { return this._name; } set { this._name = value; } }
        private bool _isVerifiedWholeSeller;
        public virtual bool IsVerifiedWholeSeller { get { return this._isVerifiedWholeSeller; } set { this._isVerifiedWholeSeller= value; } }
        private string _address;
        public virtual string Address { get { return this._address; } set { this._address = value; } }
        private float _walletBalance;
        public virtual float WalletBalance { get { return this._walletBalance; } set { this._walletBalance = value; } }

        public WholeSellerViewModel()
        {
            this._wholeSellerId = Guid.NewGuid();
            this._mobileNo = "";
            this._name = "";
            this._isVerifiedWholeSeller = false;
            this._address = "";
            this._walletBalance = 0;
        }

        public WholeSellerViewModel(Guid WholeSellerId, string name,
            string mobileNo, string address, float walletBalance, bool isVerifiedWholeSeller)
        {
            this.WholeSellerId = WholeSellerId;
            this.Name = name;
            this.MobileNo = mobileNo;
            this.Address = address;
            this.WalletBalance = walletBalance;
            this.IsVerifiedWholeSeller = isVerifiedWholeSeller;
        }
        public string WholeSeller_MobileNo_Address
        {
            get { return string.Format("{0}({1})", this.MobileNo, this.Address); }
        }

    }
}
