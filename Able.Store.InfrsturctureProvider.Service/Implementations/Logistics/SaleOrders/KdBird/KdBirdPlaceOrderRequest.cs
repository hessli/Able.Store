using Able.Store.InfrsturctureProvider.Service.Logistics.SaleOrders;
using System.Collections.Generic;

namespace Able.Store.InfrsturctureProvider.Service.Implementations.Logistics.SaleOrders.KdBird
{

    public class KdBirdPlaceOrderRequest : AbstractKDBirdRequest, IPlaceOrderRequest
    {

        public KdBirdPlaceOrderRequest()
        {
            this.Commdity = new List<ICommdity>();
        }
        public string OrderCode { get; set; }
        public string ShipperCode { get; set; }
        public int ExpType { get; set; }
        public int PayType { get; set; }
        public decimal Cost { get; set; }
        public decimal OtherCost { get; set; }
        public IContact Sender { get; set; }
        public IContact Receiver { get; set; }
        public IList<ICommdity> Commdity { get; set; }
        public double Weight { get; set; }
        public int Quantity { get; set; }
        public double Volume { get; set; }
        public string Remark { get; set; }
        public bool IsReturnPrintTemplate { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public int OrderId { get; set; }
      
    }
}
