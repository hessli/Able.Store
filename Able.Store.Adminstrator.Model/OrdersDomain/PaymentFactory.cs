using Able.Store.Adminstrator.Model.Core;
using System;

namespace Able.Store.Adminstrator.Model.OrdersDomain
{
    public class PaymentFactory
    {
        public static OrderPayment CreatePayment(int orderId, Merchant merchant,
            DateTime datePaid,
            string transctionId,
            decimal amount
            )
        {
            return new OrderPayment(orderId, datePaid, transctionId, merchant,amount);
        }
    }
}
