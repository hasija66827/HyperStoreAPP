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
            string actionURI = API.Tags;
            var tag = await Utility.CreateAsync<TTag>(actionURI, tagDTO);
            return tag;
        }
        #endregion

        #region Read
        public static async Task<List<TTag>> RetreiveTagsAsync()
        {
            List<TTag> tags = await Utility.RetrieveAsync<TTag>(API.Tags, null, null);
            return tags;
        }
        #endregion
    }
}
