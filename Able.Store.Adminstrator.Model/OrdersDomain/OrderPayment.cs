using Able.Store.Adminstrator.Model.Core;
using Able.Store.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Adminstrator.Model.OrdersDomain
{

    [Table("oms_order_payment")]
    public class OrderPayment : ValueOjectBase
    {
        public OrderPayment()
        {

        }
        public OrderPayment(int orderId, DateTime datePaid,
            string transactionId, 
            Merchant merchant, decimal amount)
        {
            DatePaid = datePaid;
            TransacionId = transactionId;
            Merchant = merchant;
            Amount = amount;
            this.OrderId = orderId;
            base.ThrowExceptionIfInvalid();
        }

        [Column("id")]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        [Column("date_paid")]
        public DateTime DatePaid { get; set; }

        [Column("order_id")]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public  virtual Order Order { get; set; }
        /// <summary>
        /// 交易号
        /// </summary>
        [Column("transacion_id")]
        public string TransacionId { get; set; }
        /// <summary>
        /// 商家:支付包，微信
        /// </summary>
        [Column("merchant")]
        public Merchant Merchant { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        /// 
        [Column("amount")]
        public decimal Amount { get; set; }
        protected override void Validate()
        {
            if (string.IsNullOrEmpty(TransacionId))
                base.AddBrokenRule(PaymentBusinessRules.TransactionIdRequired);

            if (Merchant==default(Merchant))
                base.AddBrokenRule(PaymentBusinessRules.MerchantRequired);

            if (Amount < 0)
                base.AddBrokenRule(PaymentBusinessRules.AmountValid);

            if (this.OrderId == default(int))
                base.AddBrokenRule(PaymentBusinessRules.OrderRequired);
        }
    }
}
