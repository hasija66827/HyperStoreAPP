using SDKTemplate.DTO;
using SDKTemplate.SignUp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.Login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginCC : Page
    {
        private LoginViewModel _LoginViewModel { get; set; }
        public LoginCC()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._LoginViewModel = DataContext as LoginViewModel;
            this._LoginViewModel.ErrorsChanged += _LoginViewModel_ErrorsChanged;
            base.OnNavigatedTo(e);
        }



        private void _LoginViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {

        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_LoginViewModel.ValidateProperties())
            {
                var authenticationDTO = new AuthenticateUserDTO()
                {
                    MobileNo = _LoginViewModel.MobileNo,
                    Password = _LoginViewModel.Password,
                    DeviceId = Utility.GetHardwareId()
                };

                var authenticationToken = await UserDataSource.AuthenticateUserAsync(authenticationDTO);
                if (authenticationToken == null || authenticationToken.AuthenticationFactor == EAuthenticationFactor.NotAuthenticated)
                {
                    //MSG DIalogue: INvalid username or password.
                }
                else
                {
                    BaseURI.UserId = authenticationToken.User.UserId;
                    if (authenticationToken.AuthenticationFactor == EAuthenticationFactor.TwoFactorAuthenticated)
                        this.Frame.Navigate(typeof(MainPage));
                    else if (authenticationToken.AuthenticationFactor == EAuthenticationFactor.OneFactorAuthenticated)
                    {
                        //MSG Diaologue OTP Verification starts.
                    }
                }
            }
        }

        private void SignUpClick_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SignUpPage));
        }
    }
}
