using System;
using System.Linq.Expressions;

namespace Able.Store.Infrastructure.Querying
{
    public  class Criterion
    {
        public Criterion(string propertyName,object value)
        {
            this.PropertyName = propertyName;
            this.Value = value;

        }
         public string PropertyName { get;private set; }
         public object Value { get; private set; }
   
     
    }
}
