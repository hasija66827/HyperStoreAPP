using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate;
using Models;
using SDKTemplate.DTO;
using Newtonsoft.Json;

namespace SDKTemp.Data
{
    public class CustomerOrderDataSource
    {
      
        #region SendNotification
        private static void _SendOrderCreationNotification(CustomerPageNavigationParameter PNP, decimal? deductedWalletAmount)
        {
            if (deductedWalletAmount != null)
            {
                var formattedDeductedWalletAmount = Utility.ConvertToRupee(Math.Abs((decimal)deductedWalletAmount));
                string firstMessage = "";
                if (deductedWalletAmount > 0)
                    firstMessage = String.Format("{0} has been Deducted from Wallet.", formattedDeductedWalletAmount);
                else if (deductedWalletAmount < 0)
                    firstMessage = String.Format("{0} has been Added to Wallet.", formattedDeductedWalletAmount);

                var updateWalletBalance = PNP.SelectedCustomer.WalletBalance - deductedWalletAmount;
                var formattedWalletBalance = Utility.ConvertToRupee(Math.Abs((decimal)updateWalletBalance));
                string secondMessage = "";
                if (updateWalletBalance > 0)
                    secondMessage = String.Format("You owe {0} to {1}.", formattedWalletBalance, PNP.SelectedCustomer.Name);
                else
                    secondMessage = String.Format("{0} owes you {1}.", PNP.SelectedCustomer.Name, formattedWalletBalance);

                SuccessNotification.PopUpHttpPostSuccessNotification(API.CustomerOrders, firstMessage + "\n" + secondMessage);
            }
        }
        #endregion
    }
}
