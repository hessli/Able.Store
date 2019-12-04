using Able.Store.Infrastructure.Querying;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static IQueryable<T> Order<T>(this IQueryable<T> query, IList<OrderParamter> paramters)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            for (var i = 0; i < paramters.Count; i++)
            {
                var peroperty = typeof(T).GetProperty(paramters[i].FiledName);

                MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, peroperty);

                var orderByExp = Expression.Lambda(propertyAccess, parameter);

                var orderByMethod = string.Empty;
                if (i > 0)
                    orderByMethod = paramters[i].IsDesc ? "ThenByDescending" : "ThenBy";
                else
                    orderByMethod = paramters[i].IsDesc ? "OrderByDescending" : "OrderBy";

                MethodCallExpression p = Expression.Call(typeof(Queryable), orderByMethod,
                        new Type[] { typeof(T), peroperty.PropertyType }, query.Expression,
                        Expression.Quote(orderByExp));

                query = query.Provider.CreateQuery<T>(p);
            }

            return query;
        }

        public static PagingResult<T> Pagination<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            int count = 0;
            IEnumerable<T> pageSet = new List<T>();

            if (query != null)
            {
                pageSet = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                count = query.Count();
            }
            return new PagingResult<T>(count,pageSet);
        }

        public static PagingResult<T> Pagination<T>(this IList<T> query, int pageIndex, int pageSize)
        {
            int count = 0;
            IEnumerable<T> pageSet = new List<T>();

            if (query != null)
            {
                pageSet = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                count = query.Count();
            }
            return new PagingResult<T>(count, pageSet);
        }
    }
}
