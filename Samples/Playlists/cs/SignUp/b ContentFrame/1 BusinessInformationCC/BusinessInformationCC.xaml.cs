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
    public delegate void BusinessInformationFormNavigated(BusinessInformationViewModel BIV);
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BusinessInformationCC : Page
    {
        private BusinessInformationViewModel _BIV { get; set; }
        private event BusinessInformationFormNavigated _BIFNavigatedEvent;
        public BusinessInformationCC()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _BIV = DataContext as BusinessInformationViewModel;
            _BIV.ErrorsChanged += _BIV_ErrorsChanged;
            this._BIFNavigatedEvent += CompleteUserInformationCC.Current.Current_BIFNavigatedEvent;
            base.OnNavigatedTo(e);
        }

        private void _BIV_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_BIV.ValidateProperties())
            {
                _BIV.SelectedBusinessTypeIndex = CategoryCMB.SelectedIndex;
                _BIV.SelectedStateIndex = StateCMB.SelectedIndex;
                _BIFNavigatedEvent?.Invoke(_BIV);
                this.Frame.Navigate(typeof(HyperStoreAccountCC));
            }
        }
    }
}
