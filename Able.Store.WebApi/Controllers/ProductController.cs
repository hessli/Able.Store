using Able.Store.IService;
using Able.Store.IService.ProductCatalogs;

namespace Able.Store.WebApi.Controllers
{
    public class ProductController : BaseController
    {
        public IProductCatalogService ProductCatalogService { get; set; }
        public ResponseView<PagingResultView<SkuPageListView>> PostPaging(SearchProductRequest request)
        {
             var r=  ProductCatalogService.SearchProductPaging(request);

             return  r;
        }

        public ResponseView<ProductDetailView> GetDetail(int productid,int skuid)
        {

             var r=  ProductCatalogService.GetProduct(productid, skuid);

            return r;


        }

    }
}