using System;
using System.Linq;
using System.Linq.Expressions;

namespace Able.Store.Infrastructure.Domain
{
    public interface IReadOnlyRepository<T> where T:IAggregateRoot
    {
        IQueryable<T> GetList(Expression<Func<T, bool>> expression);

        T GetFirstOrDefault(Expression<Func<T, bool>> expression);

        T GetFirstById(int id);

        IQueryable<T> GetList(Expression<Func<T, bool>> expression, params string[] includes);

        IQueryable<T> GetList<S>(Expression<Func<T, bool>> expression, Expression<Func<T, S>> orderBy, bool descending);
    }
}
