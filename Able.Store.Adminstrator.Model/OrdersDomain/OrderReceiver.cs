using Able.Store.Adminstrator.Model.UsersDomain;
using Able.Store.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Adminstrator.Model.OrdersDomain
{

    [Table("oms_order_receiver")]
    public class OrderReceiver:EntityBase<int>
    {
        public OrderReceiver()
        {

        }
        public OrderReceiver(Receiver  receiver)
        {
            this.ReceiverName = receiver.ReceiverName;

            this.Tel = receiver.Tel;

            this.CreateTime = DateTime.Now;

            this.OrderAddress = new OrderAddress(receiver.Address);
        }

        [Column("id")]
        public  override int Id { get; set; }

        [Column("receiver_name")]
        public string ReceiverName { get; set; }

        [Column("tel")]
        public string Tel { get; set; }

        [Column("order_id")]
        [ForeignKey("Order")]
        [Key]
        public int OrderId { get; set; }


        [Required]
        public virtual Order Order { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }
        public virtual OrderAddress OrderAddress { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
