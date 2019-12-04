using Able.Store.Infrastructure.Domain.Events;
using Able.Store.Model.Core;
using Able.Store.Model.OrdersDomain.Events;

namespace Able.Store.Model.OrdersDomain.States
{
    public class OpenOrderState : OrderState
    {
        public OpenOrderState()
        {
        }

        public override void Submit(Order order)
        {
            //if (order.OrderHasBeenPaidFor())
            //     //order.SetStateTo(OrderStatesFactory.Submitted);

            //OnDomainEventHandlerFactory.Raise(new OrderSubmittedEvent
            //{
            //    Order = order
            //});
        }

        public override bool SystemLocker(Order order)
        {
            throw new System.NotImplementedException();
        }
    }
}
