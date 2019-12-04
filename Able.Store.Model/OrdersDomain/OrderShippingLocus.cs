using System;
using Able.Store.Infrastructure.Domain;

namespace Able.Store.Model.OrdersDomain
{
    public class OrderShippingLocus:ValueOjectBase,IEntityBase<int>
    {
         public int Id { get; set; }
         public string Desc { get; set; }
         public DateTime? CreateTime { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
