using Able.Store.Infrastructure.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Abl.Store.Administrator.Repository.EF
{
    public interface IEFReadOnlyRepository<T> :
      IReadOnlyRepository<T> where T : IAggregateRoot
    {
        IQueryable<T> GetList(Expression<Func<T, bool>> expression);

        T GetFirstOrDefault(Expression<Func<T, bool>> expression);

        IQueryable<T> GetList(Expression<Func<T, bool>> expression, params string[] includes);

        IQueryable<T> GetList<S>(Expression<Func<T, bool>> expression, Expression<Func<T, S>> orderBy, bool descending);
    }
}
