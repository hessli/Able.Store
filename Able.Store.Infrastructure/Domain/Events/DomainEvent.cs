using Able.Store.Infrastructure.Utils;

namespace Able.Store.Infrastructure.Domain.Events
{
    public class DomainEvent
    {
        static DomainEvent()
        {
            DomainEventHandlerFactory= AutofacHelper.Resolver<IDomainEventHandlerFactory>();
        }
        public static IDomainEventHandlerFactory DomainEventHandlerFactory
        {
            get; private set;
        }

        public static R Raise<R, T>(T domainEvent,string name) where T:IDomainEvent
        {
             var eventHandler=  DomainEventHandlerFactory.GetDomainEventHandlersFor<R,T>(domainEvent,name);
             var r= eventHandler.Handler(domainEvent);
             return r;
        }

        public static void Raise<T>(T domainEvent, params string[] names) where T : IDomainEvent
        {
            DomainEventHandlerFactory
            .GetDomainEventHandlersFor(domainEvent, names)
            .ForEach(x =>x.Handler(domainEvent));
        }
    }
}
