using System;

namespace Able.Store.Infrasturcture.Service.Domain.logistics.KdBird.PlaceOrders
{
    public class KdBirdResult
    {
        public string EBusinessID { get; set; }

         public bool Success { get; set; }

        public string SignWaybillCode { get; set; }

        public string ResultCode { get; set; }

        public string Reason { get; set; }

        public string UniquerRequestNumber { get; set; }

        public string PrintTemplate { get; set; }

        public DateTime EstimatedDeliveryTime { get; set; }

        public int SubCount { get; set; }

        public string SubOrders { get; set; }

        public string SubPrintTemplates { get; set; }

        public string SignBillPrintTemplate { get; set; }

        public string ReceiverSafePhone { get; set; }

        public string SenderSafePhone { get; set; }

        public string DialPage { get; set; }

        public KdBirdOrder KdBirdOrder { get; set; }
    }
} 