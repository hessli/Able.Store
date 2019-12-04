using Able.Store.IService;
using Able.Store.IService.Shopping;
using Able.Store.Model.BasketsDomain;
using Able.Store.Model.SkusDomain;
using Able.Store.Service.IService;
using System.Collections.Generic;
using System.Linq;
namespace Able.Store.Service.Shopping
{
    public class ShoppingService : BaseService, IShoppingService
    {

        private IBasketRepository _basketRepository;
        private ISkuRepository _skuRepository;
        public ShoppingService(IBasketRepository basketRepository,
            ISkuRepository skuRepository)
        {
            _basketRepository = basketRepository;
            _skuRepository = skuRepository;
        }

        public ResponseView RemoveItem(BasketUserRequestView request)
        {
           var basket=_basketRepository.GetFirstOrDefault(x=>x.UserId== request.userid);

            var list = basket.BasketItems.Where(x => request.skuId.Contains(x.SkuId)).ToList();

            foreach (var item in list)
            {
                basket.RemoveItem(item);
            }
            _basketRepository.Commit(basket);

            return base.OutPutSuccessResponseView();
        }


        public ResponseView<IList<BasketView>> GetBasket(int userId, int[] skuIds)
        {
            var data= _basketRepository.GetBasket(userId,skuIds);
            var view = BasketView.ToViews(data);
            return base.OutPutBrokenResponseView(view);
        }

        public ResponseView<IList<BasketView>> GetBasket(int userid)
        {
             var data= this._basketRepository.GetBasket(userid);
             var view=   BasketView.ToViews(data);
            return base.OutPutBrokenResponseView(view);
        }

        public ResponseView ChangeNumber(ChangeNumRequestView reqeust)
        {
            var basket = _basketRepository
                .GetFirstOrDefault(x => x.UserId == reqeust.userid);

            basket.ChangeItemQty(reqeust.skuid, reqeust.qty);

            _basketRepository.Commit();

            return base.OutPutSuccessResponseView();
        }
        public ResponseView<string> ToBasket(BasketRequestView requestView)
        {
            requestView.CheckRequest();

            var basket = _basketRepository.GetFirstOrDefault(x => x.UserId == requestView.userid);

            var skus = _skuRepository.GetSkus(requestView.skuids);

            if (basket == null)
            {
                 basket = new Basket
                {
                    UserId = requestView.userid
                };
                _basketRepository.Add(basket);
            }
            var results = "";
            foreach (var item in skus)
            {
                basket.AddItem(item);
                results = string.Concat(results,results,item.Id,",");
                foreach (var subItem in requestView.pack)
                {
                    if (item.Id == subItem.skuid)
                    {
                        basket.ChangeItemQty(item.Id, subItem.qty);

                    }
                }
            }
            _basketRepository.Commit();

            var response = base.OutPutBrokenResponseView(results.TrimEnd(','));

            return response;
        }
    }
}
