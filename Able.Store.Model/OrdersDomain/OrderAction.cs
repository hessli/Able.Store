using Able.Store.Infrastructure.Domain;
using Able.Store.Model.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Model.OrdersDomain
{

    [Table("oms_order_action")]
   public class OrderAction:EntityBase<int>
    {

        [Column("id")]
        [Key]
        public override int Id { get; set; }
        [Column("order_id")]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

         [Required]
         public Order Order { get; set; }
       
        [Column("action_id")]
        public OrderActionEnum Action { get; set; }
        [Column("action_time")]
        public DateTime ActionTime { get; set; }
        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
