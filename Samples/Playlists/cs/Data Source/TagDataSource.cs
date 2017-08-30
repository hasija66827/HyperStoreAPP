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
        private static List<TagViewModel> _tags;
        public static List<TagViewModel> Tags { get { return _tags; } }

        #region Create
        public static async Task<TTag> CreateNewTagAsync(TagDTO tagDTO)
        {
            string actionURI = "tags";
            var tag = await Utility.CreateAsync<TTag>(actionURI, tagDTO);
            return tag;
        }
        #endregion

        #region Read
        public static void RetreiveTags()
        {
            var db = new DatabaseModel.RetailerContext();
            _tags= db.Tags.Select(t => new TagViewModel(t.TagId, t.TagName, false)).ToList();
        }
        #endregion
    }
}
