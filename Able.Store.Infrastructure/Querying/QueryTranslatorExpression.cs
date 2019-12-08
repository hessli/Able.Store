using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Able.Store.Infrastructure.Querying
{
    public static class QueryTranslatorExpression
    {
        public static Expression<Func<T, bool>> CreateExpression<T>(this Query query) where T:class
        {
            ParameterExpression parameter= Expression.Parameter(typeof(T), "x");

            Expression<Func<T, bool>> expression = x => true;

            BuildQueryFrom(query,parameter,expression);

            Query query2 = new Query();

            query2.AddCriteria(new Criterion ("name",3,CriteriaOperator.Equal));

            query2.QueryOperator = QueryOperator.Or;
            Query f = new Query();f.AddCriteria(new Criterion("age", 4, CriteriaOperator.Equal));

            query2.AddSubQuery(f);


            return expression;
        }

        private static void BuildQueryFrom<T>
            (Query query,  ParameterExpression parameter, Expression<Func<T, bool>> expression) where T:class
        {

            foreach (Criterion item in query.Criteria)
            {
                MemberExpression member = Expression.Property(parameter, item.PropertyName);

                ConstantExpression constant = Expression.Constant(item.Value);

                switch (item.Operator)
                {
                    case CriteriaOperator.Equal:
                        if (query.QueryOperator == QueryOperator.And)
                            expression = expression.And(Expression.Lambda<Func<T, bool>>(Expression.Equal(member, constant), parameter));

                        else
                            expression = expression.Or(Expression.Lambda<Func<T,bool>>(Expression.Equal(member, constant), parameter));
                        break;

                    case CriteriaOperator.LessThanOrEqual:
                        expression = expression.And(Expression.Lambda<Func<T, bool>>(Expression.LessThanOrEqual(member, constant), parameter));
                        break;
                    case CriteriaOperator.GreaterThan:
                        expression = expression.And(Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(member, constant)));
                        break;
                    case CriteriaOperator.GreaterThanOrEqual:
                        expression = expression.And(Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(member, constant)));
                        break;
                    case CriteriaOperator.NotApplicable:

                        var notContains = Expression.Call(constant, typeof(string).GetMethod("Contains"), member);
                        expression = expression.And(Expression.Lambda<Func<T, bool>>(Expression.Not(notContains)));
                        break;

                    case CriteriaOperator.Applicable:
                        var contains = Expression.Call(constant, typeof(string).GetMethod("Contains"), member);
                        expression = expression.And(Expression.Lambda<Func<T, bool>>(contains));
                        break;
                }
            }

        }

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
