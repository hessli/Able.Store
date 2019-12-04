
using Able.Store.Model.ProductsDomain;
using Able.Store.Model.SkusDomain;
using AutoMapper;
using System.Collections.Generic;

namespace Able.Store.IService.ProductCatalogs
{
    public class RecommendProductView
    {
        public string link { get; set; }
        public int productid { get; set; }
        public string img { get; set; }
        public static IList<RecommendProductView> ToView(IList<Sku> model)
        {
            IList<RecommendProductView> results = new List<RecommendProductView>();

            foreach (var item in model)
            {
                var currentImg = item.SkuImgs.GetCurrentItem();
                var vItem = new RecommendProductView
                {
                    img = currentImg == null ? string.Empty : currentImg.Img,
                    link = ((RecommendTag)item.SkuTag).Link
                };
                results.Add(vItem);

            }

            return results;
        }

    }
}
