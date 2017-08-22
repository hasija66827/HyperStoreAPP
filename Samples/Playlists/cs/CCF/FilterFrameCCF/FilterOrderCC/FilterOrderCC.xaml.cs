using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public delegate Task DateChangedDelegate();
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
            startDateCP.Closed += DateCP_Closed;
            endDateCP.Closed += DateCP_Closed;
        }
   
        private void DateCP_Closed(object sender, object e)
        {
            DateChangedEvent?.Invoke();
        }
    }
}