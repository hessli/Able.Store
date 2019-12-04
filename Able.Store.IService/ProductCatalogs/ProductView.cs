using Able.Store.Model.ProductsDomain;
using System.Collections.Generic;
using System.Linq;
namespace Able.Store.IService.ProductCatalogs
{
    public class BaseProductView
    {
        public int id { get; set; }
        public string title { get; set; }
        public string propertyname { get; set; }
        public IList<ProductPropertyValueView> properties { get; set; }
    }

    public class ProductDetailView:BaseProductView
    {
        public  SkuDetailView  sku { get; set; }
        public static ProductDetailView ToView(Product product)
        {
            ProductDetailView result = new ProductDetailView
            {
                id = product.Id,
                 propertyname=product.PropertyName,
                title = product.Title,
                sku = SkuDetailView.ToView(product.Skus.FirstOrDefault()),
                properties = ProductPropertyValueView.ToViews(product.PropertyValues)
            };
            return result;
        }

    }
}
