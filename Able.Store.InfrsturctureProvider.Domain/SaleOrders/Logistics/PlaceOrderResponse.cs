using System;

namespace Able.Store.InfrsturctureProvider.Domain.SaleOrders
{
    public class PlaceOrderResponse
    {
         public string Content { get; set; }
         public DateTime CreateTime { get; set; }
         public bool IsSuccess { get; set;}
         public string Reason { get; set; }
    }
}
