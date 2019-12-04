

using Able.Store.Adminstrator.Model.Core;

namespace Able.Store.Adminstrator.Model.OrdersDomain.States
{

    public abstract class OrderState :IOrderState
    {
        public virtual OrderStatus Status { get; }
        public abstract void Submit(Order order);
        public abstract bool SystemLocker(Order order);
     
    }
}
