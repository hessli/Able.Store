using Able.Store.IService;
using Able.Store.IService.Shopping;
using System.Collections.Generic;

namespace Able.Store.WebApi.Controllers
{
    public class ShoppingController : BaseController
    {
        public IShoppingService ShoppingService { get; set; }

        public ResponseView<string> PostToBasket(BasketRequestView request)
        {
            request.userid = 1;

            var data=ShoppingService.ToBasket(request);

            return data;
        }

        public ResponseView  PostRemove(BasketUserRequestView request)
        {
            request.userid = 1;
            var data=  ShoppingService.RemoveItem(request);

            return data;
        }

        public ResponseView<IList<BasketView>> PostBaskBySku(BasketUserRequestView request)
        {
            request.userid = 1;

           var data= ShoppingService.GetBasket(request.userid,request.skuId);

            return data;
        }
        public ResponseView<IList<BasketView>> GetBasket()
        {
            var data=  ShoppingService.GetBasket(1);

            return data;
        }


        public ResponseView PostChangeNumber(ChangeNumRequestView request)
        {
            request.userid = 1;
           var data=   ShoppingService.ChangeNumber(request);

           return data;
        }
    }
}
