using Able.Store.CommData.Orders;
using Able.Store.Model.BasketsDomain;
using Able.Store.Model.Core;
using Able.Store.Model.UsersDomain;
using System;

namespace Able.Store.Model.OrdersDomain
{
    internal static class CreateOrderFactory
    {
        internal static OrderReceiver CreateReceiver(Receiver receiver)
        {
            OrderReceiver orderReceiver = new OrderReceiver(receiver);

            return orderReceiver;
        }

        internal static OrderAction CreateOrderAction(OrderActionType  action)
        {
            return new OrderAction
            {
                Action = action,
                ActionTime = DateTime.Now,
                CreateTime = DateTime.Now,
            };
        }
        internal static OrderItem CreateOrderItems(BasketItem basketItem)
        {

            OrderItem item = new OrderItem(basketItem);

            return item;

        }
    }
}
