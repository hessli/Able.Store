using Able.Store.Administrator.IService;
using Able.Store.Administrator.IService.Skus;
using Able.Store.Adminstrator.Model.SkusDomain;
using Able.Store.Infrastructure.Queue.Rabbit;
using System.Collections.Generic;
using System.Linq;

namespace Able.Store.Administrator.Service.Skus
{
    public class SkuService : BaseService, ISkuService
    {
        ISkuRepository _skuRepository;
        ISkuCacheService _cacheService;
        public SkuService(ISkuRepository skuRepository, ISkuCacheService skuCacheService)
        {
            _skuRepository = skuRepository;
            _cacheService = skuCacheService;
        }
        public void CreateOrderChangeCannotQty(RabbitRequest request)
        {
            var correlationId = request.GetCorrelationId();

            if (_cacheService.ChangeQtyNumberExsist(correlationId, request.Header.ModuleId))
            {
                return;
            }
            ChangeCannotQtyRequest requestBody = new ChangeCannotQtyRequest();
            requestBody.items =
                request.PaseBody<IList<ChangeCannotQtyItemRequest>>();

            var response = ChangeCannotQty(requestBody);
          
             _cacheService.NotifyChangeQtyNumber(correlationId, response.issuccess, response.message, request.Header.ModuleId, response.errcode);
        }

        public ResponseView ChangeCannotQty(ChangeCannotQtyRequest request)
        {
            try
            {
                request.ThrowExceptionIfInvalid();
            }
            catch (ServiceInvalidException ex)
            {
                return base.OutPutErrorResponseView(ex.Message);
            }

            var kvs = request.GetDic();

            var skus =  _skuRepository.GetList(x => request.Ids.Contains(x.Id), "Stock").ToList();

            foreach (var item in skus)
            {
                var subItem = kvs[item.Id];
                item.ChangeCannotQty(subItem.qty);
                if (item.IsBroker())
                {
                    var result= base.OutPutErrorResponseView(item.GetBrokenRuleMessage());
                    return result;
                }
            }
           _skuRepository.Commit();

            return base.OutPutSuccessResponseView();
            

        }
    }
}
