using Able.Store.CommData.Orders;

namespace Able.Store.Model.OrdersDomain.States
{

    public abstract class OrderState :IOrderState
    {
        public virtual OrderStatus Status { get; }
        public abstract bool Delivery(Order order);
        public abstract bool SignForState(Order order);
        public abstract bool Submit(Order order);
        public abstract bool SystemLocker(Order order);

       
    }
}
