using Able.Store.Infrastructure.Domain;
using System;

namespace Able.Store.InfrsturctureProvider.Domain.SaleOrders
{
    public class PlaceOrderRequest : ValueOjectBase
    {
         public string Conent { get; set; }

         public DateTime Time { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
