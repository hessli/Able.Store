using Able.Store.Infrastructure.Domain;
using System;

namespace Able.Store.InfrsturctureProvider.Domain.SaleOrders
{
    public class PlaceOrderResponse:ValueOjectBase
    {
         public string Content { get; set; }
         public DateTime CreateTime { get; set; }
         public bool IsSuccess { get; set;}
         public string Reason { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
