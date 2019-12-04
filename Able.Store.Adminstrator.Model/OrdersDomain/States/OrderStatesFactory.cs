
using Able.Store.Adminstrator.Model.Core;
using System.Collections.Generic;

namespace Able.Store.Adminstrator.Model.OrdersDomain.States
{
    public static class OrderStatesFactory
    {
        private static Dictionary<OrderStatus, IOrderState> _dic = new Dictionary<OrderStatus, IOrderState>();

        static OrderStatesFactory()
        {
            _dic.Add(OrderStatus.系统处理, new SystemLockState());

            _dic.Add(OrderStatus.待支付, new OrderPayState());
        }

        public static IOrderState GetOrderState(OrderStatus state)
        {

            IOrderState orderState = null;

            _dic.TryGetValue(state, out orderState);

            return orderState;
        }
    }
}
