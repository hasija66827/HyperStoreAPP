using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.Settings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsCC : Page
    {
        private SettingsViewModel _SV { get; set; }
        public SettingsCC()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _SV = DataContext as SettingsViewModel;
            _SV.Passcode = "";
            _SV.ConfirmPasscode = "";
            _SV.ErrorsChanged += _SV_ErrorsChanged;
            base.OnNavigatedTo(e);
        }

        private void _SV_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {

        }

        private void RequestForPlan_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void UpdatePasscodeBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = this._SV.ValidateProperties();
            var str = Passcode.Password;
            if (IsValid)
            {
                if (_SV.Passcode != BaseURI.User.Passcode)
                {
                    var updateUserDTO = new UpdateUserDTO() { Passcode = _SV.Passcode };
                    var user = await UserDataSource.UpdatePasscodeAsync(BaseURI.User.UserId, updateUserDTO);
                    if (user != null)
                        BaseURI.User = user;
                    //TODO:Verify the trans with free SMS service.

                }
            }
        }
    }
}
