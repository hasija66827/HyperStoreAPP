using Models;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class UserDataSource
    {
        #region Create 
        public static async Task<TUser> CreateNewUserAsync(UserDTO userDTO)
        {
            var user = await Utility.CreateAsync<TUser>(API.Users, userDTO);
            if (user != null)
            {
                var message = String.Format("Welcome {0}!!!\n We are happy to find you here.", user);
                SuccessNotification.PopUpSuccessNotification(API.Users, message);
            }
            return user;
        }
        #endregion
    }
}
