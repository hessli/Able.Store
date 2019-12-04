using Able.Store.Model.SkusDomain;
using AutoMapper;
using System.Collections.Generic;
namespace Able.Store.IService.ProductCatalogs
{
    public class NewProductView
    {
      
        public int id { get; set; }
        public int productid { get; set; }
        public decimal price { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string img { get; set; }
        public int saleqty { get; set; }
        public static IList<NewProductView> ToView(IList<Sku> models)
        {
            IList<NewProductView> results = new List<NewProductView>();
            foreach (var item in models)
            {
                var newTag = (NewTag)item.SkuTag;

               var currentImg=  item.SkuImgs.GetCurrentItem();

                var vItem = new NewProductView
                {
                    id = item.Id,
                    productid=item.ProductId,
                    text = item.Content,
                    title = item.Title,
                    price = newTag.Price,
                    saleqty = newTag.SaleQty,
                    img = currentImg==null?string.Empty:currentImg.Img
                };
                results.Add(vItem);
            }

            return results;
        }
    }
}
