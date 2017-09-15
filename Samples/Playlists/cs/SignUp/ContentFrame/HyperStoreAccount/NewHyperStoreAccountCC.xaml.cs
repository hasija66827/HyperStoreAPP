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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewHyperStoreAccountCC : Page
    {
        private HyperStoreAccountViewModel _HSAV { get; set; }
        public NewHyperStoreAccountCC()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _HSAV = DataContext as HyperStoreAccountViewModel;
            _HSAV.ErrorsChanged += _HyperStoreAccountViewModel_ErrorsChanged;
            base.OnNavigatedTo(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = this._HSAV.ValidateProperties();
            if (IsValid)
                this.Frame.Navigate(typeof(ProfileCompletionCC));
        }

        private void _HyperStoreAccountViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
        }
    }
}
