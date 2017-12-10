using HyperStoreServiceAPP.DTO;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginSignUpService.DTO;

namespace SDKTemplate
{
    class OTPDataSource
    {
        public async static Task<bool> VerifyTransactionByOTPAsync(OTPVerificationDTO OTPVerificationDTO)
        {
            var OTP = await Utility.RetrieveAsync<string>(AuthenticationServiceAPI.OTPVerification, null, OTPVerificationDTO);
            if (OTP == null)
                return false;
            var OTPDialog = new OTPDialogCC(OTPVerificationDTO.ReceiverMobileNo, OTP);
            await OTPDialog.ShowAsync();
            return OTPDialog.IsVerified;
        }
    }
}
