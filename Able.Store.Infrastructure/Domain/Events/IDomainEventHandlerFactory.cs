using System.Collections.Generic;

namespace Able.Store.Infrastructure.Domain.Events
{
    public interface IDomainEventHandlerFactory
    {
        IDomaineventHandler<R, T> GetDomainEventHandlersFor<R,T>(T domainEvent,string name) where T:IDomainEvent;
        IEnumerable<IDomaineventHandler<T>> GetDomainEventHandlersFor<T>(T domainEvent,params string[] names) where T:IDomainEvent;
    }
}
