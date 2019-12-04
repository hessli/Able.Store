using Able.Store.Adminstrator.Model.Core;
namespace Able.Store.Adminstrator.Model.OrdersDomain.States
{
    public interface IOrderState
    {
        OrderStatus Status { get;  }

        void Submit(Order order);
         
        bool SystemLocker(Order order);
    }
}
