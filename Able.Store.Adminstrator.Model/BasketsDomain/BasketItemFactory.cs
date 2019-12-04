using Able.Store.Adminstrator.Model.SkusDomain;

namespace Able.Store.Adminstrator.Model.BasketsDomain
{
    public class BasketItemFactory
    {
        public static BasketItem CrateItemFor(Sku sku, Basket basket)
        {  

            var basketItem = new BasketItem (sku,basket);
            return basketItem;
        }
    }
}
