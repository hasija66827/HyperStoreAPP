using Models;
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
    public sealed partial class PersonSummaryCC : Page
    {
        private PersonSummaryViewModel _PSV { get; set; }

        public PersonSummaryCC()
        {
            this.InitializeComponent();
            this._PSV = new PersonSummaryViewModel();
            SupplierCCF.Current.SupplierListUpdatedEvent += Current_SupplierListUpdatedEvent;
        }

        private void Current_SupplierListUpdatedEvent(List<Person> suppliers)
        {
            this._PSV.TotalWalletBalance = (decimal)suppliers.Sum(c => c.WalletBalance);
            this._PSV.OnALLPropertyChanged();
        }
    }
}
