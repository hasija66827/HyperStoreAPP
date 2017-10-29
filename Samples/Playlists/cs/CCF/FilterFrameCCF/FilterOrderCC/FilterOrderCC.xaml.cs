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
    public delegate Task FilterSupplierOrderCriteriaChangedDelegate();
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FilterOrderCC : Page
    {
        public static FilterOrderCC Current;
        public FilterSupplierOrderViewModel FilterSupplierOrderCriteria { get { return this._FilterSupplierOrderViewModel; } }
        public FilterSupplierOrderCriteriaChangedDelegate FilterOrderCriteriaChangedEvent;
        private FilterSupplierOrderViewModel _FilterSupplierOrderViewModel;
        public FilterOrderCC()
        {
            Current = this;
            this.InitializeComponent();
            this._FilterSupplierOrderViewModel = new FilterSupplierOrderViewModel();
            OrderDateLBCP.Closed += FilterCriteriaChanged;
            OrderDateUBCP.Closed += FilterCriteriaChanged;
            DueDateLBCP.Closed += FilterCriteriaChanged;
            DueDateUBCP.Closed += FilterCriteriaChanged;
            PartialOrderOnlyCB.Click += FilterCriteriaChanged;
        }

        private void FilterCriteriaChanged(object sender, object e)
        {
            FilterOrderCriteriaChangedEvent?.Invoke();
        }
    }
}
