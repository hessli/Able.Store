using Able.Store.Infrastructure.Domain;
using Able.Store.Model.BasketsDomain;
using Able.Store.Model.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Model.OrdersDomain
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

        public ImgJsonCollection GetImgCollections()
        {
            return  this.Sku.GetImgCollections(this.SkuId);
        }

        public string GetIndexImg(int index)
        {
            return this.Sku.GetIndexImg(this.SkuId,index);
        }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
