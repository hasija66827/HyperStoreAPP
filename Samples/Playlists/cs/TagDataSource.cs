using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class TagDataSource
    {
        #region create
        public static bool CreateTag(TagViewModel tag)
        {
            var db = new DatabaseModel.RetailerContext();
            db.Tags.Add(new DatabaseModel.Tag(tag));
            db.SaveChanges();
            return true;
        }
        #endregion

        #region Read
        public static List<TagViewModel> RetrieveTags()
        {
            var db = new DatabaseModel.RetailerContext();
            return db.Tags.Select(t => new TagViewModel(t.TagName, false)).ToList();
        }
        #endregion
    }
}
