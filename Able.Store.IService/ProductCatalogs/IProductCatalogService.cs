using System.Collections.Generic;

namespace Able.Store.IService.ProductCatalogs
{
    public interface IProductCatalogService
    {
        ResponseView<PagingResultView<SkuPageListView>> SearchProductPaging(SearchProductRequest request);

        ResponseView<ProductDetailView> GetProduct(int productId,int skuId);

        ResponseView<IList<NewProductView>> GetNewProducts(int size);

        ResponseView<IList<RecommendProductView>>  GetRecommendProduct(int size,string url);
    }
}
