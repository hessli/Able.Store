using Able.Store.Infrastructure.Domain.Events;
using Able.Store.Model.OrdersDomain.Events;
using Able.Store.QueueService.Interface.Orders;

namespace Able.Store.Service.Orders
{
    public class OrderDeliveryHandler : IDomaineventHandler<bool, OrderChangeEvent>
    {

        private IOrderQueueService _queueService;

        public OrderDeliveryHandler(IOrderQueueService queueService)
        {
            _queueService = queueService;
        }
     
        public bool Handler(OrderChangeEvent domainEvent)
        {
            //推送队列就可以了
           //var request=LogisticsRequestFactory.CreateKdBridPlaceOrder(domainEvent.Order);

           // Action<object> action = x => {

           //     var success= ((IOrderQueueService)x).PutLogistics(domainEvent.Order);

           // };
           // Task taks = new Task(action,_queueService);

           // taks.Start();

            return true;
        }
    }
}
