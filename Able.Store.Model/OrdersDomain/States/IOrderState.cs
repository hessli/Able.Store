using Able.Store.Model.Core;
namespace Able.Store.Model.OrdersDomain.States
{
    public interface IOrderState
    {
        OrderStatus Status { get;  }

        void Submit(Order order);
         
        bool SystemLocker(Order order);
    }
}
