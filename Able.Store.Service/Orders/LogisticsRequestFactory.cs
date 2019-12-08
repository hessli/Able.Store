using Able.Store.Infrasturcture.Service.Implementations.logistics;
using Able.Store.Infrasturcture.Service.Interface.logistics;
using Able.Store.Model.OrdersDomain;

namespace Able.Store.Service.Orders
{
    public static class LogisticsRequestFactory
    {
        public static IPlaceOrderRequest CreateKdBridPlaceOrder(this Order order)
        {

            KdBirdPlaceOrderRequest data = new KdBirdPlaceOrderRequest
            {
                Cost = order.TotalCost,
                OrderCode = order.OrderNo,
                Quantity = order.TotalQty,
                Weight = 1.00,
                Volume = 2.00,
                Remark = "这只是测试订单",
                ShipperCode = "x",
                OtherCost = 0
            };

            foreach (var item in order.Items)
            {
                ICommdity commdity = new KdBirdCommdity
                {
                    GoodsName = item.Sku.Title,
                    Goodsquantity = item.TotalQty,
                    GoodsWeight = 2.00
                };
                data.Commdity.Add(commdity);
            }
            return data;
        }
    }
}
