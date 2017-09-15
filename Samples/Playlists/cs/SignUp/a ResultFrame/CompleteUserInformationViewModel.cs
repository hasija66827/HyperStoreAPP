using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.SignUp
{
    public class CompleteUserInformationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public BusinessInformationViewModel BIV { get; set; }
        public HyperStoreAccountViewModel HSAV { get; set; }
        public ProfileCompletionViewModel PCV { get; set; }
        public CompleteUserInformationViewModel() { }
        public void OnALLPropertyChanged()
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            foreach (PropertyInfo prop in typeof(CompleteUserInformationViewModel).GetProperties())
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop.Name));
        }
    }
}
