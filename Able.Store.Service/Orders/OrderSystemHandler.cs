using Able.Store.Infrastructure.Domain.Events;
using Able.Store.Model.OrdersDomain.Events;
using Able.Store.QueueService.Interface.Orders;

namespace Able.Store.Service.Orders
{
    public class OrderSystemHandler : IDomaineventHandler<bool,OrderChangeEvent>
    {
        private IOrderQueueService _queueService;
        public OrderSystemHandler(IOrderQueueService queueService)
        {
            _queueService = queueService;
        }
        public bool Handler(OrderChangeEvent domainEvent)
        {
            return  _queueService.Lock(domainEvent.Order);
        }
    }
}
