using Able.Store.Adminstrator.Model.BasketsDomain;
using Able.Store.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Adminstrator.Model.OrdersDomain
{

    [Table("oms_order_item_sku_attribute")]
    public  class OrderItemSkuAttribute : EntityBase<int>
    {
        public OrderItemSkuAttribute(BasketSkuAttribute skuAttribute)
        {
            this.CreateTime = DateTime.Now;
            this.Name = skuAttribute.Name;
            this.Value = skuAttribute.Value;
        }

        [Column("id")]
        [Key]
        public override int Id { get; set; }
        [Column("order_item_id")]
        [ForeignKey("OrderItemSku")]
        public int? OrderItemId { get; set; }
        [Required]
        public virtual OrderItemSku OrderItemSku { get; set; }

        [Column("value")]
        public string Value { get; set; }

        [Column("name")]
        public string Name { get; set; }
        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }

        protected override void Validate()
        {

        }
    }
}
