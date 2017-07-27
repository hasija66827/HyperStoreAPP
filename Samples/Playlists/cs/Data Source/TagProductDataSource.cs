using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class TagProductDataSource
    {
        public static bool CreateTagProduct(Guid productId, List<Guid?> tagIds)
        {
            var db = new DatabaseModel.RetailerContext();
            List<DatabaseModel.ProductTag> productTags = new List<DatabaseModel.ProductTag>();
            foreach (var tagId in tagIds)
                productTags.Add(new DatabaseModel.ProductTag(productId, tagId));

            db.ProductTags.AddRange(productTags);
            db.SaveChanges();
            return true;
        }

        /// <summary>
        /// Retrieves list of distinct product Id having atleast one tag in tagIds.
        /// </summary>
        /// <param name="tagIds"></param>
        /// <returns></returns>
        public static List<Guid?> RetrieveProductId(List<Guid?> tagIds)
        {
            var db = new DatabaseModel.RetailerContext();
            var result = db.ProductTags
                            .Where(pt => tagIds.Contains(pt.TagId))
                            .Select(pt => pt.ProductId)
                             .Distinct();
            return result.ToList();

        }
    }
}
