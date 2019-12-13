using Able.Store.CommData.Orders;
using Able.Store.Infrastructure.Dappers;
using Able.Store.Infrastructure.Domain;
using System;
namespace Able.Store.RedisConsumer.Domain.OrdersDomain
{
    public class Order : EntityBase<int>, IAggregateRoot
    {
        public Order()
        {
           
        }
        [Column("id")]
       
        public override int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("total_cost")]
        public decimal TotalCost { get; set; }

        [Column("total_qty")]
        public int TotalQty { get; set; }

        [Column("order_no")]
        public string OrderNo { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }

        [Column("order_status")]
        public short Status { get; set; }
 
        [Column("user_name")]
        public string UserName { get; set; }

        [Column("message")]
        public string Message { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
        internal void SetStateTo(OrderStatus orderStatus)
        {
            this.Status = (short)orderStatus;
        }
    }
}
