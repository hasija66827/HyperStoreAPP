using Models;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public enum EAuthentication
    {
        NotAuthenticated,
        OneFactorAuthenticated,
        TwoFactorAuthenticated
    }
    class UserDataSource
    {
        #region Create 
        public static async Task<TUser> CreateNewUserAsync(UserDTO userDTO)
        {
            var user = await Utility.CreateAsync<TUser>(API.Users, userDTO);
            if (user != null)
            {
                var message = String.Format("Welcome {0} {1}!!!\n We are happy to find you here.", user.FirstName, user.LastName);
                SuccessNotification.PopUpSuccessNotification(API.Users, message);
            }
            return user;
        }
        #endregion

        public static async Task<EAuthentication> AuthenticateUserAsync(AuthenticateUserDTO authenticateUserDTO)
        {
            var eauthentication = await Utility.RetrieveAsync<EAuthentication>(BaseURI.LoginSignUpService, API.Users, QueryString.AuthenticateUser, authenticateUserDTO);
            if (eauthentication != null && eauthentication.Count == 1)
                return eauthentication[0];
            return EAuthentication.NotAuthenticated;
        }
    }
}
