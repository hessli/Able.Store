using Able.Store.Infrastructure.Domain.Business;

namespace Able.Store.Model.OrdersDomain
{
    public class PaymentBusinessRules
    {
        public static readonly BusinessRule TransactionIdRequired =
           new BusinessRule("TransactionId", "交易单号不可为空");

        public static readonly BusinessRule MerchantRequired =
            new BusinessRule("Merchant", "支付商不可为空");
        public static readonly BusinessRule AmountValid =
            new BusinessRule("Amount", "不是有效的交易金额");

        public static readonly BusinessRule OrderRequired = new BusinessRule("Order","订单不可以为空");

         
    }
}
