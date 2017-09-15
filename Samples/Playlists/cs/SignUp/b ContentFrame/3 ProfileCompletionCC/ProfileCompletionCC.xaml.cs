using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.SignUp
{
    public delegate void ProfileCompletionFormNavigated(ProfileCompletionViewModel PCV);
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfileCompletionCC : Page
    {
        private ProfileCompletionViewModel _PCV;
        private event ProfileCompletionFormNavigated _PCFNavigatedEvent;
        public ProfileCompletionCC()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._PCV = DataContext as ProfileCompletionViewModel;
            this._PCV.DateOfBirth = DateTime.Now.AddYears(-25);
            this._PCV.ErrorsChanged += _PCV_ErrorsChanged;
            this._PCFNavigatedEvent += CompleteUserInformationCC.Current.Current_PCFNavigatedEvent;
            base.OnNavigatedTo(e);
        }

        private void _PCV_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_PCV.ValidateProperties())
            {
                this._PCFNavigatedEvent?.Invoke(_PCV);
            }
        }
    }
}
