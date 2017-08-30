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

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TagFormCC : Page
    {
        private TagFormViewModel _Tag { get; set; }
        public TagFormCC()
        {
            this.InitializeComponent();
            Loaded += CreateTag_Loaded;
        }

        private void CreateTag_Loaded(object sender, RoutedEventArgs e)
        {
            _Tag = DataContext as TagFormViewModel;
            _Tag.ErrorsChanged += _Tag_ErrorsChanged;
        }

        private void _Tag_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage));
        }
    }
}
