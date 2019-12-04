using Able.Store.Adminstrator.Model.BasketsDomain;
using Able.Store.Adminstrator.Model.UsersDomain;

namespace Able.Store.Adminstrator.Model.OrdersDomain
{
    internal static class CreateOrderFactory
    {
        internal static OrderReceiver CreateReceiver(Receiver receiver)
        {
            OrderReceiver orderReceiver = new OrderReceiver(receiver);

            return orderReceiver;
        }
        internal static OrderItem CreateOrderItems(BasketItem basketItem)
        {
             
            OrderItem item = new OrderItem(basketItem);

            return item;
      
        }
    }
}
