using Models;
using Mvvm;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public class TagFormViewModel : ValidatableBindableBase, ITag
    {
        private DelegateCommand _validateCommand;

        private string _tagName;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        public string TagName { get { return this._tagName; } set { SetProperty(ref _tagName, value); } }

        public TagFormViewModel()
        {
            _validateCommand = new DelegateCommand(ValidateAndSave_Executed);
        }

        public ICommand ValidateCommand
        {
            get { return _validateCommand; }
        }

        private async void ValidateAndSave_Executed()
        {
            var IsValid = ValidateProperties();
            if (IsValid)
            {
                var tagDTO = new TagDTO()
                {
                    TagName = this.TagName,
                };
                await TagDataSource.CreateNewTagAsync(tagDTO);
                MainPage.Current.NotifyUser("New Tag was created succesfully ", NotifyType.StatusMessage);
            }
        }
    }
}
