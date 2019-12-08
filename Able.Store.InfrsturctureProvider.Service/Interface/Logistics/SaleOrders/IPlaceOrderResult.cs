namespace Able.Store.InfrsturctureProvider.Service.Logistics.SaleOrders
{ 
    public  interface IPlaceOrderResult
    {
        string EBusinessID { get; set; }

        ILogisticsOrder  Order { get; set; }

        bool Success { get; set; }

        string SignWaybillCode { get; set; }

        string ResultCode { get; set; }

        string Reason { get; set; }

        string UniquerRequestNumber { get; set; }


        string PrintTemplate { get; set; }


        string EstimatedDeliveryTime { get; set; }


        int SubCount { get; set; }

        string SubOrders { get; set; }


        string SubPrintTemplates { get; set; }

        string SignBillPrintTemplate { get; set; }


        string ReceiverSafePhone { get; set; }

        string SenderSafePhone { get; set; }

        string DialPage { get; set; }
    }
}
