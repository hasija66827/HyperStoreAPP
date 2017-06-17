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
    public delegate void DateChangedDelegate(object sender);
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FilterOrderCC : Page
    {
        public FilterOrderViewModel SelectedDateRange;
        public static FilterOrderCC Current;
        public event DateChangedDelegate DateChangedEvent;
        public FilterOrderCC()
        {
            Current = this;
            this.SelectedDateRange = new FilterOrderViewModel();
            this.InitializeComponent();
            startDateCP.Closed += EndDateCP_Closed;
            endDateCP.Closed += EndDateCP_Closed;
        }
   
        private void EndDateCP_Closed(object sender, object e)
        {
            DateChangedEvent?.Invoke(FilterOrderCC.Current);
        }
    }
}