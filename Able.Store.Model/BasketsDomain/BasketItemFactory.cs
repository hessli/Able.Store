using Able.Store.Model.SkusDomain;

namespace Able.Store.Model.BasketsDomain
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
