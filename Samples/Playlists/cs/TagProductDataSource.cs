using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class TagProductDataSource
    {
        public static bool CreateTagProduct(Guid productId, List<Guid> tagIds )
        {
            var db = new DatabaseModel.RetailerContext();
            List<DatabaseModel.ProductTag> productTags= new List<DatabaseModel.ProductTag>();
            foreach (var tagId in tagIds)    
                    productTags.Add(new DatabaseModel.ProductTag(productId, tagId));
            
            db.ProductTags.AddRange(productTags);
            db.SaveChanges();
            return true;
        }
    }
}
