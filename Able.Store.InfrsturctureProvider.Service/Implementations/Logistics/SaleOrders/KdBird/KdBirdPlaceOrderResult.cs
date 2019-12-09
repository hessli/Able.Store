using Able.Store.InfrsturctureProvider.Service.Logistics.SaleOrders;
using Newtonsoft.Json;
using System;

namespace Able.Store.InfrsturctureProvider.Service.Implementations.Logistics.SaleOrders.KdBird
{
    public class KdBirdPlaceOrderResult : AbstractResponseResult, IPlaceOrderResult
    {
        public string EBusinessID { get; set; }

        [JsonConverter(typeof(LogisticsOrderConvert))]
        public ILogisticsOrder Order { get; set; }
        public string SignWaybillCode { get; set; }
        public string UniquerRequestNumber { get; set; }
        public string PrintTemplate { get; set; }
        public string EstimatedDeliveryTime { get; set; }
        public int SubCount { get; set; }
        public string SubOrders { get; set; }
        public string SubPrintTemplates { get; set; }
        public string SignBillPrintTemplate { get; set; }
        public string ReceiverSafePhone { get; set; }
        public string SenderSafePhone { get; set; }
        public string DialPage { get; set; }

        public string GetLogisticCode()
        {
            if (Success)
            {
                return Order.LogisticCode;
            }
            else return "";
        }
    }

    public class LogisticsOrderConvert : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
        public override object ReadJson(JsonReader reader, Type objectType,  object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<KdBirdLogisticsOrder>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
