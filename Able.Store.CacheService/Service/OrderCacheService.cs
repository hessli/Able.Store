using Able.Store.CommData.Orders;
using Able.Store.Infrastructure.Cache;
using Able.Store.Infrastructure.Cache.Model;
using Able.Store.IService.Orders;
using Able.Store.Model.OrdersDomain;

namespace Able.Store.CacheService.Service
{
    public class OrderCacheService : IOrderCacheService
    {
        private IOrderRepository _orderRepository;
        private ICacheStorage _cacheStorage;
        
        public OrderCacheService(IOrderRepository orderRepository, ICacheStorage cacheStorage)
        {
            _orderRepository = orderRepository;
            _cacheStorage = cacheStorage;
        }
        public string GetGenerateNo()
        {
            CacheUnitModel model = new CacheUnitModel
            {
                CacheStrategy = CacheStrategy.Always,
                DataBaseIndex = OrderStaticResource.DBINDEX,
                Expire = OrderStaticResource.GetTimeSpan(OrderActionType.订购)
            }; 

           var no = _cacheStorage.HashIncrement(model, OrderStaticResource.GENERATENO, OrderStaticResource.GENERATENO_FILED);
            return no.ToString();
        }
    }
}
