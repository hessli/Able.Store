using Able.Store.Model.Core;
using System;

namespace Able.Store.Model.OrdersDomain.States
{
    public class OrderPayState : OrderState
    {
        public override OrderStatus Status
        {
            get
            {
                return OrderStatus.待支付;
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
            throw new NotImplementedException();
        }


        public override bool SystemLocker(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
