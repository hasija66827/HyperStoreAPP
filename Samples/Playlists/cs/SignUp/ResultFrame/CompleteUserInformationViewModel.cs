using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.SignUp
{
    public class CompleteUserInformationViewModel
    {
        public BusinessInformationViewModel BIV { get; set; }
        public HyperStoreAccountViewModel HSAV { get; set; }
        public ProfileCompletionViewModel PCV { get; set; }
        public CompleteUserInformationViewModel() { }
    }
}
