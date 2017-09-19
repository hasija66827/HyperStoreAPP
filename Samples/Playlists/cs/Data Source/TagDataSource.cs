using Models;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public delegate void CreatedNewTag(TTag tag);
    class TagDataSource
    {
        public static event CreatedNewTag CreatedNewTagEvent;
        #region Create
        public static async Task<TTag> CreateNewTagAsync(TagDTO tagDTO)
        {
            var tag = await Utility.CreateAsync<TTag>(BaseURI.HyperStoreService + API.Tags, tagDTO);
            if (tag != null)
            {
                var message = String.Format("You can associate {0} with any product in your store.\nThis can help you to filter out the products in meaningful way.", tag.TagName);
                CreatedNewTagEvent?.Invoke(tag);
                SuccessNotification.PopUpSuccessNotification(API.Tags, message);
            }
            return tag;
        }
        #endregion

        #region Read
        public static async Task<List<TTag>> RetreiveTagsAsync()
        {
            List<TTag> tags = await Utility.RetrieveAsync<List<TTag>>(BaseURI.HyperStoreService + API.Tags, null, null);
            return tags;
        }
        #endregion
    }
}
