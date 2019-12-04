using Able.Store.Infrastructure.Querying;
using Able.Store.IService.Comm;
using Able.Store.Model.SkusDomain;
using System.Collections.Generic;
using System.Linq;

namespace Able.Store.IService.Categories
{
    public  class CategroyProductView
    {
        public int skudId { get; set; }
        public string title { get; set; }
        public string img { get; set; }

        public static PagingResultView<CategroyProductView> ToViews(PagingResult<Sku> entity)
        {
            IList<CategroyProductView> productViews = new List<CategroyProductView>();

            foreach (var item in entity.Result)
            {
                productViews.Add(new CategroyProductView
                {
                    img = ImgDescView.GetImg(ImgDescView.ToView(item.SkuImgs.FirstOrDefault())),
                    skudId = item.Id,
                    title = item.Title
                });
            }

            PagingResultView<CategroyProductView> paging = new PagingResultView<CategroyProductView>(entity.PageCount, productViews);
            return paging;
        }
    }
}
