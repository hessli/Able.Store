namespace Able.Store.Infrastructure.Querying
{
    public  class Criterion
    {
         public string PropertyName { get; set; }
         public object Value { get; set; }

         public QueryOperator Operator { get; set; }
    }
}
