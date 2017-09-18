using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class OTPDataSource
    {
        public async static Task<bool> VerifyTransactionByOTP(OTPVerificationDTO OTPVerificationDTO)
        {
            var OTP = await Utility.RetrieveAsync<string>(AuthenticationServiceAPI.OTPVerification, null, OTPVerificationDTO);
            if (OTP == null || OTP.Count != 1)
                return false;
            var OTPDialog = new OTPDialogCC(OTPVerificationDTO.ReceiverMobileNo, OTP[0]);
            await OTPDialog.ShowAsync();
            return OTPDialog.IsVerified;
        }
    }
}
