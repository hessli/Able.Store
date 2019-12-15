using Able.Store.CommData.Products;
using Able.Store.Infrastructure.Cache;
using Able.Store.IService.ProductCatalogs;
using Able.Store.Model.ProductsDomain;
using Able.Store.Model.SkusDomain;
using System.Collections.Generic;
namespace Able.Store.CacheService.Service
{
    public class ProductCacheService : IProductCacheService

    {
        private IProductRepository _productRepository;
        private ISkuRepository _skuRepository;
        private ICacheStorage _cacheStorage;

        public ProductCacheService(IProductRepository productRepository,
            ISkuRepository skuRepository, ICacheStorage cacheStorage)
        {
            _productRepository = productRepository;
            _skuRepository = skuRepository;
            _cacheStorage = cacheStorage;
        }

        public IList<RecommendProductView> GetRecommendProduct(int size, string baseUrl)
        {

            var data = _cacheStorage.SortedSetRangeByRank<string, RecommendProductView>(ProductStaticResource.DBINDEX, ProductStaticResource.RECOMMENDKEY, stop: size);

            if (data == null || data.Count == 0)
            {
                var model = _skuRepository.GetRecommendProducts(size);
                foreach (var item in model)
                {
                    ((RecommendTag)item.SkuTag).SetLink(baseUrl);
                }
                data = RecommendProductView.ToView(model);
            }

            return data;

        }
        public IList<NewProductView> GetNewProducts(int size)
        {
            var data = _cacheStorage.SortedSetRangeByRank<string, NewProductView>
                (ProductStaticResource.DBINDEX, ProductStaticResource.NEWKEY, stop: size);

            if (data == null || data.Count == 0)
            {
                var model = _skuRepository.GetNewProducts(size);

                data = NewProductView.ToView(model);
            }
            return data;
        }
    }
}
