using Able.Store.Adminstrator.Model.BasketsDomain;
using Able.Store.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Adminstrator.Model.OrdersDomain
{

    [Table("oms_order_item")]
   public class OrderItem:EntityBase<int>
    {
        public OrderItem()
        {

        }
        public OrderItem(BasketItem item)
        {
            this.SkuId = item.SkuId;
            this.ProductId = item.ProductId;
            this.TotalCost = item.TotalCost;
            this.TotalQty = item.TotalQty;
            this.Sku = new OrderItemSku(item.BasketSku);
        }

        [Column("id")]
        [Key]
        public override int Id { get; set; }

        [ForeignKey("Order")]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Required]
        public virtual Order Order { get; set; }

        [Column("sku_id")]
        public int SkuId { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }

        [Column("total_cost")]
        public decimal TotalCost { get; set; }

        [Column("total_qty")]
        public int TotalQty { get; set; }
        public virtual OrderItemSku Sku { get; set; }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
