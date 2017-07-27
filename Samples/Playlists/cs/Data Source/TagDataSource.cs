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
        public static bool CreateTag(TagViewModel tag)
        {
            var db = new DatabaseModel.RetailerContext();
            db.Tags.Add(new DatabaseModel.Tag(tag));
            db.SaveChanges();
            _tags.Add(tag);
            return true;
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
