using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed class ProductTagViewModel : BindableBases, ITag
    {
        public Guid? TagId { get; set; }
        public string TagName { get; set; }

        public bool IsChecked
        {
            get; set;
        }

        public ProductTagViewModel()
        {
        }
    }
}
