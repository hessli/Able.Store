using Able.Store.Infrastructure.Domain.Events;

namespace Able.Store.Model.OrdersDomain.Events
{
    public class OrderChangeEvent:IDomainEvent
    {
          public Order Order { get; set; }
    }
}
