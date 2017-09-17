using Models;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public enum EAuthenticationFactor
    {
        NotAuthenticated,
        OneFactorAuthenticated,
        TwoFactorAuthenticated
    }

    public class AuthenticationToken
    {
        public EAuthenticationFactor AuthenticationFactor { get; set; }
        public TUser User { get; set; }
    }

    class UserDataSource
    {
        #region Create 
        public static async Task<AuthenticationToken> CreateNewUserAsync(UserDTO userDTO)
        {
            var authenticationToken = await Utility.CreateAsync<AuthenticationToken>(BaseURI.LoginSignUpService + API.Users, userDTO);
            if (authenticationToken != null)
            {
                var message = String.Format("Welcome {0} {1}!!!\n We are happy to find you here.", authenticationToken.User.FirstName, authenticationToken.User.LastName);
                SuccessNotification.PopUpSuccessNotification(API.Users, message);
            }
            return authenticationToken;
        }
        #endregion

        public static async Task<AuthenticationToken> AuthenticateUserAsync(AuthenticateUserDTO authenticateUserDTO)
        {
            var authenticationToken = await Utility.RetrieveAsync<AuthenticationToken>(BaseURI.LoginSignUpService + API.Users, QueryString.AuthenticateUser, authenticateUserDTO);
            if (authenticationToken != null && authenticationToken.Count == 1)
                return authenticationToken[0];
            return new AuthenticationToken(){AuthenticationFactor= EAuthenticationFactor.NotAuthenticated};
        }
    }
}
