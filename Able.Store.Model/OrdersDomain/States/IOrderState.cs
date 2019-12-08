using Able.Store.Model.Core;
namespace Able.Store.Model.OrdersDomain.States
{
    public interface IOrderState
    {
        OrderStatus Status { get;  }

        /// <summary>
        /// 支付后提交
        /// </summary>
        /// <param name="order"></param>
        bool Submit(Order order);
         /// <summary>
         /// 锁定
         /// </summary>
         /// <param name="order"></param>
         /// <returns></returns>
        bool SystemLocker(Order order);

        /// <summary>
        /// 推配送
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        bool Delivery(Order order);

        /// <summary>
        /// 签收
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        bool SignForState(Order order);
    }
}
