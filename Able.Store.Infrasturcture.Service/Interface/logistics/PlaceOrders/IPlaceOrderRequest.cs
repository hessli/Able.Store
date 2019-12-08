using System.Collections.Generic;

namespace Able.Store.Infrasturcture.Service.Interface.logistics
{
    public interface IPlaceOrderRequest
    {
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
