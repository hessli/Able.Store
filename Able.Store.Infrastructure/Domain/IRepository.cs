namespace Able.Store.Infrastructure.Domain
{
    public interface IRepository<T>:IReadOnlyRepository<T>,IBaseRepository<T> where T:IAggregateRoot
    {
        void Save(T entity);
        void Commit();
    }
}
