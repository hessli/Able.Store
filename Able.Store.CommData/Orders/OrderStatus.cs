
namespace Able.Store.CommData.Orders
{
    public enum OrderStatus:short
    {
        系统处理 = 1,
        待支付 = 2,
        待出货 = 3,
        配送中=4,
        待签收 = 5,
        已签收 = 6,
        已取消=7
    }
}
