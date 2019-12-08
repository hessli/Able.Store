namespace Able.Store.Infrastructure.Domain
{
    public interface IBaseRepository<T>
    {
        void Add(T entity);

        void Remove(T entity);

    }
}
