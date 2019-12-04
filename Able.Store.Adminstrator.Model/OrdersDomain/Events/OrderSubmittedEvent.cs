using Able.Store.Infrastructure.Domain.Events;

namespace Able.Store.Adminstrator.Model.OrdersDomain.Events
{
    public class OrderSubmittedEvent:IDomainEvent
    {
          public Order Order { get; set; }
    }
}
