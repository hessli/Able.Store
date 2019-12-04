
using Able.Store.IService.Adverts;
using Able.Store.IService.ProductCatalogs;
using Able.Store.Model.AdvertDomain;
using Able.Store.Model.CategoriesDomain;
using Able.Store.Model.SkusDomain;
using Able.Store.Service.IService.Categories;
using AutoMapper;
using System.Linq;

namespace Able.Store.IService
{
    public class AutoMapperBootStrapper
    {
        public static  MapperConfiguration Configuration
        {
            get;
            private set;
        }
        public  static void ConfigureAutoMapper()
        {
            Configuration = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;

                #region 产品
              //  cfg.CreateMap<Sku, NewProductView>();

                cfg.CreateMap<Sku, BaseSkuView>();


               // cfg.CreateMap<Sku, RecommendProductView>();

                #endregion

                #region 通用
                cfg.CreateMap<SearchPageOrderRequest,OrderParamter>();
                #endregion
                cfg.CreateMap<Advert, BannerView>();

                cfg.CreateMap<Category, CategoryView>();

                #region
                // cfg.CreateMap<ProductTitle, ProductSummaryView>().ForMember(x => x.Price, opt => opt.ConvertUsing(new MoneyFormatter()));
                //cfg.CreateMap<Product, IndexProductView>();

                //cfg.CreateMap<Advert, BannerView>();

                //cfg.CreateMap<Category, IndexCategoryView>();
                #endregion

            });
        }
    }
}
