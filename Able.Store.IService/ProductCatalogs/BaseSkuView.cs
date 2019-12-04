using Able.Store.Infrastructure.Querying;
using Able.Store.IService.Comm;
using Able.Store.Model.SkusDomain;
using System.Collections.Generic;

namespace Able.Store.IService.ProductCatalogs
{
    public class BaseSkuView
    {
        public int id { get; set; }
        public int productid { get; set; }
        public string title { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 销量
        /// </summary>
        public int saleqty { get; set; }
    }
    public class SkuDetailView : BaseSkuView
    {
        public IList<ImgDescView> imgs { get; set; }
        public string content { get; set; }
        public IList<SkuAttributeView> skuattributes { get; set; }


        public static IList<SkuDetailView> ToViews(IEnumerable<Sku> models)
        {

            IList<SkuDetailView> results = new List<SkuDetailView>();
            foreach (var item in models)
            {
                results.Add(ToView(item));
            }

            return results;
        }
        public static SkuDetailView ToView(Sku model)
        {

            SkuDetailView view = new SkuDetailView
            {
                id = model.Id,
                content = model.Content,
                price = model.Sale.Price,
                productid = model.ProductId,
                saleqty = model.Sale.SaleQty,
                title = model.Title,
                skuattributes=SkuAttributeView.ToViews(model.Attributes),
                 imgs=ImgDescView.ToViews(model.SkuImgs)
            };

            return view;
        }
    }
    public class SkuPageListView : BaseSkuView
    {
        public string img { get; private set; }
        public static IList<SkuPageListView> ToView(IEnumerable<Sku> models)
        {
            IList<SkuPageListView> results = new List<SkuPageListView>();
            foreach (var item in models)
            {
                var current = item.SkuImgs.GetCurrentItem();
                var vItem = new SkuPageListView()
                {
                    id = item.Id,
                    img = current == null ? "" : current.Img,
                    price = item.Sale.Price,
                    saleqty = item.Sale.SaleQty,
                    title = item.Title,
                    productid = item.ProductId,
                };
                results.Add(vItem);
            }
            return results;
        }

        public static PagingResultView<SkuPageListView> ToPagingResultView(PagingResult<Sku> pagingResult)
        {
            var pageSet = ToView(pagingResult.Result);

            PagingResultView<SkuPageListView> views = new PagingResultView<SkuPageListView>(pagingResult.PageCount, pageSet);

            return views;
        }
    }

}
