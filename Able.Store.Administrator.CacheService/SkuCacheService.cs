using Able.Store.Administrator.IService;
using Able.Store.Administrator.IService.Skus;
using Able.Store.Adminstrator.Model.SkusDomain;
using Able.Store.CommData.Orders;
using Able.Store.Infrastructure.Cache;

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
        public void NotifyChangeQtyNumber(string requestCorrelationId, bool isSuccess,
            string message, int moduleId, int errorCode = 0)
        {
            var args = new ResponseView<string>(message, isSuccess, requestCorrelationId);

            args.errcode = errorCode;

            //发布到Redis消息队列
            _cacheStorage.Publish(OrderCacheKey.DBINDEX,
               OrderCacheKey.CREATE_ORDER_CHANGEQTY_CHANNELNAME, args);
        }
        public bool ChangeQtyNumberExsist(string requestCorrelationId)
        {
            var key = string.Concat(requestCorrelationId, ".", OrderCacheKey.CREATE_ORDER_CHANGEQTY_RESULT_STUFF);

            bool isExists = _cacheStorage.KeyExists(OrderCacheKey.DBINDEX, key);

            return isExists;
        }
    }
}
