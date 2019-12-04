
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Able.Store.Infrastructure.Domain
{
    public interface IReadOnlyRepository<T> where T:IAggregateRoot
    {
        T GetSingle();

        /// <summary>
        /// 显示加载导航属性
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        T GetExplicitFirstOrDefault(Expression<Func<T, bool>> expression);

        IQueryable<T> GetList(Expression<Func<T,bool>> expression);

        T GetFirstOrDefault(Expression<Func<T, bool>> expression);

        IQueryable<T> GetList(Expression<Func<T, bool>> expression, params string[] includes);

        IQueryable<T> GetList<S>(Expression<Func<T, bool>> expression, Expression<Func<T, S>> orderBy, bool descending);


    }
}
