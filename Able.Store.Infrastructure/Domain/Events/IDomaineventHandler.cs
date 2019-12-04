namespace Able.Store.Infrastructure.Domain.Events
{


    public interface IDomaineventHandler<R, T> where T : IDomainEvent
    {
        R Handler(T domainEvent);
    }
    public interface IDomaineventHandler<T> where T:IDomainEvent
    {
        void  Handler(T domainEvent);
    }
}
