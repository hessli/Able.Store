using Able.Store.QueueService.Interface.Orders;

namespace Able.Store.QueueService.Implementations.Order
{
    public static class LogisticsRequestFactory
    {
        public static LogisticsRequestBody CreatePlaceOrder(this Able.Store.Model.OrdersDomain.Order order)
        {

            LogisticsRequestBody data = new LogisticsRequestBody
            {
                Cost = order.TotalCost,
                OrderCode = order.OrderNo,
                Quantity = order.TotalQty,
                Weight = 1.00,
                Volume = 2.00,
                Remark = order.Message,
                OtherCost = 0,
                Receiver = new LogisticsContactBody
                {
                    Address = order.Receiver.OrderAddress.Detail,
                    CityName = order.Receiver.OrderAddress.City,
                    Company = order.Receiver.ReceiverName,
                    ExpAreaName = order.Receiver.OrderAddress.Area,
                    Mobile = order.Receiver.Tel,
                    Name = order.Receiver.ReceiverName,
                    ProvinceName = order.Receiver.OrderAddress.Province

                },
                 OrderId=order.Id,
                Sender = new LogisticsContactBody
                {

                    Address = SenderTemplete.Address,
                    CityName = SenderTemplete.CityName,
                    Company = SenderTemplete.Company,
                    ExpAreaName = SenderTemplete.ExpAreaName,
                    Mobile = SenderTemplete.Mobile,
                    Name = SenderTemplete.Name,
                    ProvinceName = SenderTemplete.ProvinceName
                }
            };
            foreach (var item in order.Items)
            {

                LogisticsCommdityBody commdity = new LogisticsCommdityBody
                {
                    GoodsName = item.Sku.Title,
                    Goodsquantity = item.TotalQty,
                    GoodsWeight = 2.00
                };
                data.AddItem(commdity);
            }
            return data;
        }
    }
}
