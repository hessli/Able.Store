using Able.Store.Model.ProductsDomain;
using System.Collections.Generic;

namespace Able.Store.IService.ProductCatalogs
{
    public class ProductPropertyValueView
    {
         public int id { get; set; }
         public string value { get; set; }
        public static IList<ProductPropertyValueView> ToViews(IList<ProductPropertyValue> model)
        {
            IList<ProductPropertyValueView> results = new List<ProductPropertyValueView>();

            foreach (var item in model)
            {
                results.Add(new ProductPropertyValueView {

                     id=item.SkuId,
                     value=item.Value
                });
            }
            return results;
        }
    }
}
