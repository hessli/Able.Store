using Able.Store.Infrastructure.Utils;
using System.Collections.Generic;

namespace Able.Store.Infrastructure.Domain.Events
{
    public class DomainEventHandlerFactory : IDomainEventHandlerFactory
    {
        public IEnumerable<IDomaineventHandler<T>>
            GetDomainEventHandlersFor<T>(T domainEvent, params string[] names) where T : IDomainEvent
        {
            IList<IDomaineventHandler<T>> handlers = new List<IDomaineventHandler<T>>();

            foreach (var item in names)
            {
                var handler = AutofacHelper.ResolverNamed<IDomaineventHandler<T>>(item);

                handlers.Add(handler);
            }
            return handlers;
        }

        public IDomaineventHandler<R, T> GetDomainEventHandlersFor<R, T>(T domainEvent, string name) where T : IDomainEvent
        {
            var handler = AutofacHelper.ResolverNamed<IDomaineventHandler<R, T>>(name);

            return handler;
        }
    }
}
