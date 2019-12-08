using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Able.Store.Infrastructure.Querying
{
    public static class QueryTranslatorExpression
    {
        public static IQueryable<T> Order<T>(this IQueryable<T> query, IList<OrderByClause> paramters) where T:class
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            for (var i = 0; i < paramters.Count; i++)
            {
                var peroperty = typeof(T).GetProperty(paramters[i].PropertyName);

                MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, peroperty);

                var orderByExp = Expression.Lambda(propertyAccess, parameter);

                var orderByMethod = string.Empty;
                if (i > 0)
                    orderByMethod = paramters[i].Desc ? "ThenByDescending" : "ThenBy";
                else
                    orderByMethod = paramters[i].Desc ? "OrderByDescending" : "OrderBy";

                MethodCallExpression p = Expression.Call(typeof(Queryable), orderByMethod,
                        new Type[] { typeof(T), peroperty.PropertyType }, query.Expression,
                        Expression.Quote(orderByExp));

                query = query.Provider.CreateQuery<T>(p);
            }
            return query;
        }
    }
}
