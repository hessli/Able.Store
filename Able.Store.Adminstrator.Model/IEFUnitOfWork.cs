using Able.Store.Infrastructure.UniOfWork;
using System.Data.Entity;

namespace Able.Store.Adminstrator.Model.EF
{
    public interface IEFUnitOfWork : IUnitOfWork
    {
        DbSet<T> Souce<T>() where T :class;
    }
}
