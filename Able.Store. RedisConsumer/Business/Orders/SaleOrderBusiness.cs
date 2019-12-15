using Able.Store.Infrastructure.Cache;
using Able.Store.Infrastructure.Cache.Redis;
using Able.Store.Infrastructure.Queue.Rabbit;
using Able.Store.RedisConsumer.Comm;
using Able.Store.RedisConsumer.DAO;

namespace Able.Store.RedisConsumer.Business.Orders
{
    public class SaleOrderBusiness
    {
        private SaleOrderDAO saleOrderDAO;

        private ICacheStorage _cacheStorage;
        public SaleOrderBusiness()
        {
            saleOrderDAO = new SaleOrderDAO();
            _cacheStorage = new RedisStorage();
        }
        public void CreateOrder(RequestView<RabbitRequestHeader> request)
        {
            var orderId = 0;
            if (request.issuccess && request.result != null &&
               int.TryParse(request.result.BusinessId, out orderId))
            {
                var order = saleOrderDAO.GetOrder(orderId);

                order.SetStateTo(CommData.Orders.OrderStatus.待支付);

                saleOrderDAO.UpdateState(order);

                //下面到时候需要记录日志先不处理吧
               // CacheUnitModel model = new CacheUnitModel
               // {
               //     DataBaseIndex = OrderStaticResource.DBINDEX,
               //     Expire = OrderStaticResource.GetTimeSpan(OrderActionType.订购)
               // };
               //var correlationId= request.result.GetCorrelationId();

               //var key=  OrderStaticResource.
               //                   GetCreateOrderCompleteCorrelationId(correlationId);

               //var result= new RequestView<int>
               // {
               //     issuccess = true,
               //     result = order.Id
               // };
               // _cacheStorage.SetAdd(model, key, result);
            }
        }
    }
}
