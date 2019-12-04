using Able.Store.Adminstrator.Model.Core;

namespace Able.Store.Adminstrator.Model.SkusDomain
{
    public class SkuTagFactory
    {
        public static ISkuTag GetProductTag(ProductTag tag,Sku sku)
        {
            ISkuTag productTag=null;

            switch (tag)
            {
                case ProductTag.推荐:
                    productTag = new RecommendTag(sku);
                    break;
                case ProductTag.新品:
                    productTag = new NewTag(sku);
                    break;
            }
            return productTag;
        }
    }
}
