

using Able.Store.Model.BasketsDomain;
using System.Collections.Generic;

namespace Able.Store.IService.Shopping
{
    public class BasketView
    {
        public int skuid { get; set; }

        public string title { get; set; }

        public decimal totalcost { get; set; }

        public string img { get; set; }

        public decimal price { get; set; }

        public int qty { get; set; }

        public string propertyname { get; set; }

        public string propertyvalue { get; set; }
        public static IList<BasketView> ToViews(Basket basket)
        {
            IList<BasketView> views = new List<BasketView>();

            if (basket != null)
            {
                foreach (var item in basket.BasketItems)
                {
                    var img= item.BasketSku.SkuImgs
                                         .GetCurrentItem();
                    views.Add(new BasketView
                    {
                        title = item.BasketSku.Title,
                        qty = item.TotalQty,
                        propertyname = item.BasketSku.PropertyName,
                        propertyvalue = item.BasketSku.PropertyValue,
                        skuid = item.SkuId,
                        price = item.BasketSku.Price,
                        img = img==null?string.Empty:img.Img,
                         totalcost=item.TotalCost
                    });
                }
            }
            return views;
        }
    }
}
