using System.Collections.Generic;

namespace Able.Store.IService.ProductCatalogs
{
    public interface IProductCacheService:IBaseCacheService
    {
        /// <summary>
        /// 推荐产品
        /// </summary>
        /// <param name="size"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        IList<RecommendProductView> GetRecommendProduct(int size, string baseUrl);
        /// <summary>
        /// 新品
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        IList<NewProductView> GetNewProducts(int size);
    }
}
