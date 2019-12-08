using Able.Store.IService;
using Able.Store.IService.Adverts;
using Able.Store.IService.Categories;
using Able.Store.IService.ProductCatalogs;
using Able.Store.Service.IService.Categories;
using System.Collections.Generic;

namespace Able.Store.WebApi.Controllers
{
    public class IndexController : BaseController
    {
        public IProductCatalogService ProductCatalogService { get; set; }
        public IAdvertService AdvertService { get; set; }
        public ICategoryService CategoryService { get; set; }
        public ResponseView<IList<BannerView>> GetBanner(int size)
        {
            var data = AdvertService.GetBanners(size);

       

            return data;
        }

        public ResponseView<IList<CategoryView>> GetCategoris(int size)
        {
            var data = CategoryService.GetCategories(size);

            return data;

        }

        public ResponseView<IList<RecommendProductView>> GetRecommendProduct(int size)
        {
            var data = ProductCatalogService.GetRecommendProduct(size, "www.baidu.com");

            return data;


        }
        public ResponseView<IList<NewProductView>> GetNewProducts(int size)
        {

            var data = ProductCatalogService.GetNewProducts(size);

            return data;

        }

    }
}
