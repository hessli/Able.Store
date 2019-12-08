using System.Collections.Generic;

namespace Able.Store.InfrsturctureProvider.Service.Logistics.SaleOrders
{
    public interface IPlaceOrderRequest
    { 
        
        int OrderId { get; set; }
        string OrderCode { get; set; }
        string ShipperCode { get; set; }
        int ExpType { get; set; }
        int PayType { get; set; }
        decimal Cost { get; set; }
        decimal OtherCost { get; set; }
        IContact Sender { get; set; }
        IContact Receiver { get; set; }
        IList<ICommdity> Commdity { get; set; }
        double Weight { get; set; }

        int Quantity { get; set; }

        double Volume { get; set; }

        string Remark { get; set; }
        bool IsReturnPrintTemplate { get; set; }

      
    }
}
