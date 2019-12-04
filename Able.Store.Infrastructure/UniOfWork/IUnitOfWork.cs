namespace Able.Store.Infrastructure.UniOfWork
{
    public interface IUnitOfWork
    {
        void RegisterAmended<T>(T entity) where T :class;

        void RegisterNew<T>(T entity) where T : class;

        void RegisterRemoved<T>(T entity) where T : class;

        void Commit();
    }
}
