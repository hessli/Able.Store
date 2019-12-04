using System;
using Able.Store.Infrastructure.Domain.Events;
using Able.Store.Infrastructure.Utils;
using Able.Store.Model.Core;
using Able.Store.Model.OrdersDomain.Events;
namespace Able.Store.Model.OrdersDomain.States
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
            var orderState = AutofacHelper
                .ResolverNamed<IOrderState>("OrderPayState");  //OrderStatesFactory.GetOrderState(OrderStatus.待支付);

            order.SetStateTo(orderState);

            var submmitted = new OrderChangeEvent() { Order = order };
            //触发事件
           var result=  DomainEvent.Raise<bool, OrderChangeEvent>
                (submmitted, "OrderSystemHandler");

            return result;
        }
    }
}
