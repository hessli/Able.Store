namespace Able.Store.Model.Core
{
    public enum OrderStatus : int
    {
        系统处理 = 1,
        待支付 = 2,
        待出货 = 3,
        配送中=4,
        待签收 = 5,
        已签收 = 6,
        已取消=7
    }
    public enum OrderActionEnum:int
    {
        订购 = 1,
        支付 = 2,
        发货 = 3,
        签收 = 4,
        取消=5
    }
    public enum Merchant
    {
        微信 = 1,
        支付宝 = 2,
        钱包 = 3
    }
}
