using Able.Store.Model.OrdersDomain;


namespace Able.Store.QueueService.Interface.Orders
{
  public  interface IOrderQueueService
    {
        //锁定库存
        bool Lock(Order order);
        //推配送
        bool PutShipping();
        //支付完成后需要发送电子邮件和短信
        bool Notify();
    }
}
