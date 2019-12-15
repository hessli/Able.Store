using Able.Store.Administrator.IService;
using Able.Store.Administrator.IService.Skus;
using Able.Store.Adminstrator.Model.SkusDomain;
using Able.Store.CommData.Orders;
using Able.Store.Infrastructure.Cache;
using Able.Store.Infrastructure.Cache.Model;
using Able.Store.Infrastructure.Queue.Rabbit;
using System.Threading.Tasks;

namespace Able.Store.Administrator.CacheService
{
    public class SkuCacheService : ISkuCacheService
    {
        private ISkuRepository _skuRepository;
        private ICacheStorage _cacheStorage;

        public SkuCacheService(ISkuRepository skuRepository,
            ICacheStorage cacheStorage)
        {
            _skuRepository = skuRepository;
            _cacheStorage = cacheStorage;
        }
        public void NotifyChangeQtyNumber(RabbitRequestHeader requestHeader,
            bool isSuccess,
            string message, 
            int errorCode = 0)
        {
            var args = new ResponseView<RabbitRequestHeader>(message, isSuccess, requestHeader);

            args.errcode = errorCode;

            Task _task = new Task(() =>
            {   //标记处理结果
                var model = new CacheUnitModel
                {
                    DataBaseIndex = OrderStaticResource.DBINDEX,
                    Expire = OrderStaticResource.GetTimeSpan(OrderActionType.订购)
                };
                var correlationId = requestHeader.GetCorrelationId();

                var key = OrderStaticResource.GetCreateOrderCompleteCorrelationId(correlationId);
                _cacheStorage.SetAdd(model, key, args);
            });

            _task.Start();

            //发布redis消息队列
            _cacheStorage.Publish(OrderStaticResource.DBINDEX,
               OrderStaticResource.CREATE_ORDER_CHANGEQTY_CHANNELNAME, args);
        }

        public bool ChangeQtyNumberExsist(string requestCorrelationId)
        {
            var key = OrderStaticResource.GetCreateOrderCompleteCorrelationId(requestCorrelationId);

            bool isExists = _cacheStorage.KeyExists(OrderStaticResource.DBINDEX, key);

            return isExists;
        }
    }
}
