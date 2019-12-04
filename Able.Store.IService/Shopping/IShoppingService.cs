
using System.Collections.Generic;

namespace Able.Store.IService.Shopping
{
    public interface IShoppingService
    {
        ResponseView<IList<BasketView>> GetBasket(int userId);

        ResponseView<IList<BasketView>> GetBasket(int userId, int[] skuIds);

        ResponseView<string> ToBasket(BasketRequestView requestView);

        ResponseView ChangeNumber(ChangeNumRequestView reqeust);

        ResponseView RemoveItem(BasketUserRequestView request);
    }
}
