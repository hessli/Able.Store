using Able.Store.Infrastructure.Cache.Redis;
using Able.Store.IService;
using Able.Store.IService.ProductCatalogs;
using Able.Store.Model.ProductsDomain;
using Able.Store.Model.SkusDomain;
using System;
using System.Collections.Generic;
namespace Able.Store.CacheService.Service
{
    public class ProductCacheService : IProductCacheService
       
    {
        private IProductRepository _productRepository;
        private ISkuRepository _skuRepository;
        Lazy<CacheController> _controller = new Lazy<CacheController>(); 
        public ProductCacheService(IProductRepository productRepository,
            ISkuRepository skuRepository)
        {
            _productRepository = productRepository;
            _skuRepository = skuRepository;
            CacheKeys = new List<string>();
            CacheKeys.Add(Recommend_KEY);
            CacheKeys.Add(New_KEY);
        }
        public string PREFIX
        {
            get
            {
                return "product_";
            }
        }
        public string Recommend_KEY
        {
            get
            {
                return string.Concat(PREFIX, "recommend");
            }
        }
        public string New_KEY
        {
            get
            {
                return string.Concat(PREFIX, "New");
            }
        }
        public IList<string> CacheKeys {
            get;private set;
        }
        private int _dbIndex = (int)RedisDbZone.Pms;
        public   IList<RecommendProductView> GetRecommendProduct(int size, string baseUrl)
        {
           var data=  _controller.Value.HashValues<RecommendProductView>(Recommend_KEY,_dbIndex);

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
        public  IList<NewProductView> GetNewProducts(int size)
        {
            var data = _controller.Value.HashValues<NewProductView>(New_KEY, _dbIndex);
            if (data == null || data.Count == 0)
            {
                var model = _skuRepository.GetNewProducts(size);

                data = NewProductView.ToView(model);
            }
           
             return data;
        }
    }
}
