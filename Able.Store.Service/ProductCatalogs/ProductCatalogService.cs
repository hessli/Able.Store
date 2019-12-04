
using Able.Store.IService;
using Able.Store.IService.ProductCatalogs;
using Able.Store.Model.ProductsDomain;
using Able.Store.Model.SkusDomain;
using Able.Store.Service.IService;
using System.Collections.Generic;

namespace Able.Store.Service.Implements.ProductCatalogs
{
    public class ProductCatalogService : BaseService,IProductCatalogService
    {
        IProductRepository _productRepository;
        IProductCacheService _productCacheService;
        ISkuRepository _skuRepository;
        public ProductCatalogService(IProductRepository productRepository,
            ISkuRepository skuRepository, IProductCacheService productCacheService)
        {
            _productRepository = productRepository;
            _skuRepository = skuRepository;
            _productCacheService = productCacheService;
        }
        /// <summary>
        /// 获取推荐产品
        /// </summary>
        public ResponseView<IList<RecommendProductView>> GetRecommendProduct(int size, string baseUrl)
        {
            var data = _productCacheService.GetRecommendProduct(size, baseUrl);

            return base.OutPutResponseView(data);
        }
        /// <summary>
        /// 获取新品
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public ResponseView<IList<NewProductView>> GetNewProducts(int size)
        {
            var data = _productCacheService.GetNewProducts(size);

            return base.OutPutResponseView(data);
        }

        public ResponseView<ProductDetailView> GetProduct(int productId,int skuId)
        {
             var  model=  _productRepository.GetProduct(productId, skuId);
             var data= ProductDetailView.ToView(model);
            return base.OutPutBrokenResponseView(data);
        }
        /// <summary>
        /// 查询产品
        /// </summary>
        /// <param name="title"></param>
        /// <param name="orderParamters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseView<PagingResultView<SkuPageListView>> SearchProductPaging(SearchProductRequest request)
        {
            var pagingModel = _skuRepository.PagingResult(request.keyword,
                request.GetOrderParamter(), 
                request.page_index, 
                request.page_size);

           

            var data = SkuPageListView.ToPagingResultView(pagingModel);

            var result= base.OutPutBrokenResponseView(data);

            return result;
        }
    }
}
