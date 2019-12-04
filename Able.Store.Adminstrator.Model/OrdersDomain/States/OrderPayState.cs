using Able.Store.Adminstrator.Model.Core;
using System;

namespace Able.Store.Adminstrator.Model.OrdersDomain.States
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

        public override void Submit(Order order)
        {
            throw new NotImplementedException();
        }


        public override bool SystemLocker(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
