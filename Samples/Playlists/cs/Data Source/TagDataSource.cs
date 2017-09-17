using Models;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class TagDataSource
    {
        private static List<FilterTagViewModel> _tags;
        public static List<FilterTagViewModel> Tags { get { return _tags; } }

        #region Create
        public static async Task<TTag> CreateNewTagAsync(TagDTO tagDTO)
        {
            var tag = await Utility.CreateAsync<TTag>(BaseURI.HyperStoreService + API.Tags, tagDTO);
            if (tag != null)
            {
                var message = String.Format("You can associate {0} with any product in your store.\nThis can help you to filter out the products in meaningful way.", tag.TagName);
                SuccessNotification.PopUpSuccessNotification(API.Tags, message);
            }
            return tag;
        }
        #endregion

        #region Read
        public static async Task<List<TTag>> RetreiveTagsAsync()
        {
            List<TTag> tags = await Utility.RetrieveAsync<TTag>(BaseURI.HyperStoreService + API.Tags, null, null);
            return tags;
        }
        #endregion
    }
}
