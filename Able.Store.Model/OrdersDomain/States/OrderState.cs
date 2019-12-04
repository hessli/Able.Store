using Able.Store.Model.Core;

namespace Able.Store.Model.OrdersDomain.States
{

    public abstract class OrderState :IOrderState
    {
        public virtual OrderStatus Status { get; }
        public abstract void Submit(Order order);
        public abstract bool SystemLocker(Order order);
     
    }
}
