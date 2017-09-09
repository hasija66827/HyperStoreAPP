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
    public sealed partial class SupplierSummaryCC : Page
    {
        private SupplierSummaryViewModel _SupplierSummaryViewModel { get; set; }

        public SupplierSummaryCC()
        {
            this.InitializeComponent();
            this._SupplierSummaryViewModel = new SupplierSummaryViewModel();
            SupplierCCF.Current.SupplierListUpdatedEvent += Current_SupplierListUpdatedEvent;
        }

        private void Current_SupplierListUpdatedEvent(List<Models.TSupplier> suppliers)
        {
            this._SupplierSummaryViewModel.TotalWalletBalance = (decimal)suppliers.Sum(c => c.WalletBalance);
            this._SupplierSummaryViewModel.OnALLPropertyChanged();
        }
    }
}
