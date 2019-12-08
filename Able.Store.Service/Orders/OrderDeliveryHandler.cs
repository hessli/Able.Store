using Able.Store.Infrastructure.Domain.Events;
using Able.Store.Infrasturcture.Service.Implementations.Logistics;
using Able.Store.Infrasturcture.Service.Interface.logistics;
using Able.Store.Model.OrdersDomain.Events;

namespace Able.Store.Service.Orders
{
    public class OrderDeliveryHandler : IDomaineventHandler<bool, OrderChangeEvent>
    {
        ILogisticsProviderService logisticsProviderService; 
        public OrderDeliveryHandler()
        {
            logisticsProviderService = new KdBirdProvider();
        }
        public bool Handler(OrderChangeEvent domainEvent)
        {
               logisticsProviderService
                     .PlaceOrder(domainEvent.Order.CreateKdBridPlaceOrder());

            return true;
        }
    }
}
