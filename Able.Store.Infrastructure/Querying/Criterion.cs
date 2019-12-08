using System;
using System.Linq.Expressions;

namespace Able.Store.Infrastructure.Querying
{
    public  class Criterion
    {
        public Criterion(string propertyName,object value, CriteriaOperator criteriaOperator)
        {
            this.PropertyName = propertyName;
            this.Value = value;
            this.Operator = criteriaOperator;
        }
         public string PropertyName { get;private set; }
         public object Value { get; private set; }
         public CriteriaOperator Operator { get; private set; }

        public static Criterion Create<T>(Expression<Func<T, object>> expression, object value, CriteriaOperator criteriaOperator)
        {
            string propertyName = PropertyNameHelper.ResolvePropertyName<T>(expression);
            Criterion myCriterion = new Criterion(propertyName, value, criteriaOperator);
            return myCriterion;
        }
    }
}
