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
    public delegate void FilterWholeSalerOrderCriteriaChangedDelegate();
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FilterWholeSalerOrderCC : Page
    {
        public FilterWholeSalerOrderCriteriaChangedDelegate FilterWholeSalerOrderCriteriaChangedEvent;
        public FilterWholeSalerOrderCriteria FilterWholeSalerOrderCriteria { get; set; }
        public  static FilterWholeSalerOrderCC Current;
        public FilterWholeSalerOrderCC()
        {
            Current = this;
            this.InitializeComponent();
            this.FilterWholeSalerOrderCriteria = new FilterWholeSalerOrderCriteria();
            startDateCP.Closed += FilterCriteriaChanged;
            endDateCP.Closed += FilterCriteriaChanged;
            dueDateCP.Closed += FilterCriteriaChanged;
            PartialOrderOnlyCB.Click += FilterCriteriaChanged;
        }

        private void FilterCriteriaChanged(object sender, object e)
        {
            FilterWholeSalerOrderCriteriaChangedEvent?.Invoke();
        }
    }
}
