using Able.Store.CommData.Orders;
using Able.Store.Infrastructure.Domain.Events;
using Able.Store.Infrastructure.Utils;
using Able.Store.Model.OrdersDomain.Events;
using System;
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

        public override bool Delivery(Order order)
        {
            throw new NotImplementedException();
        }

        public override bool SignForState(Order order)
        {
            throw new NotImplementedException();
        }

        public override bool Submit(Order order)
        {
            throw new NotImplementedException("当前订单不可以提交");
        }
        public override bool SystemLocker(Order order)
        {
            var orderState = AutofacHelper
                .ResolverNamed<IOrderState>("OrderPayState");  //OrderStatesFactory.GetOrderState(OrderStatus.待支付);

            order.SetStateTo(orderState);
            var submmitted = new OrderChangeEvent() { Order = order };
            //触发锁定库存，暂时屏蔽
            var result = DomainEvent.Raise<bool, OrderChangeEvent>
                 (submmitted, "OrderSystemHandler");

            //var result=DomainEvent.Raise<bool, OrderChangeEvent>(submmitted, "OrderDeliveryHandler");

            return true;
        }
    }
}
