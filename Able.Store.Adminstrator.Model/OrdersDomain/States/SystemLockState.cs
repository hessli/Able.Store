using System;
using Able.Store.Adminstrator.Model.Core;
using Able.Store.Adminstrator.Model.OrdersDomain.Events;
using Able.Store.Infrastructure.Domain.Events;

namespace Able.Store.Adminstrator.Model.OrdersDomain.States
{
    public class SystemLockState : OrderState
    {
        public override OrderStatus Status
        {
            get
            {
                return OrderStatus.系统处理;
            }
        }
        public override void Submit(Order order)
        {
            throw new NotImplementedException("当前订单不可以提交");
        }
        public override bool SystemLocker(Order order)
        {
            var orderState = OrderStatesFactory.GetOrderState(OrderStatus.待支付);

            order.Status = orderState.Status;

            order.SetStateTo(orderState);

            var submmitted = new OrderSubmittedEvent() { Order = order };
            //触发事件
             DomainEvent.Raise(submmitted, "OrderSystemHandler");

            return true;
        }
    }
}
