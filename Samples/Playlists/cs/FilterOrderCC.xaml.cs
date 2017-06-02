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
        public FilterOrderViewModel SelectedDateRange { get { return this._selectedDate; } }
        private FilterOrderViewModel _selectedDate;
        public static FilterOrderCC Current;
        public event DateChangedDelegate DateChangedEvent;
        public FilterOrderCC()
        {
            Current = this;
            this._selectedDate = new FilterOrderViewModel();
            
            this.InitializeComponent();

            startDateCP.DateChanged += StartDate_DateChanged;
            endDateCP.DateChanged += EndDate_DateChanged;
        }

        private void EndDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            Current._selectedDate.EndDate = endDateCP.Date.Value.Date;
            DateChangedEvent?.Invoke(this);
        }

        private void StartDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            this._selectedDate.StartDate = startDateCP.Date.Value.Date;
            DateChangedEvent?.Invoke(this);
        }
    }
}
