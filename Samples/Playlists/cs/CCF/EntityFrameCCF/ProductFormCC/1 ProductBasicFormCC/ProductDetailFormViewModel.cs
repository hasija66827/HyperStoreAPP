using Models;
using Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public sealed class ProductDetailFormViewModel : ValidatableBindableBase
    {
        private string _code;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [RegularExpression(@"[1-9]\d{3,12}", ErrorMessage = "Try code with atleast 4 and atmost 13 digits.")]
        public string Code { get { return this._code; } set { SetProperty(ref _code, value); } }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [MaxLength(20, ErrorMessage = "Try name with atmost 20 charecters.")]
        [RegularExpression(@"[a-zA-Z\s]{1,20}", ErrorMessage = "Name is Invalid")]
        public string Name { get; set; }

        //TODO: add error text block.
        private Int32? _threshold;
        [Range(0, Int32.MaxValue)]
        [Required(ErrorMessage = "Try Threshold with positive integar.", AllowEmptyStrings = false)]
        public Int32? Threshold { get { return this._threshold; } set { SetProperty(ref _threshold, value); } }
    }
}
