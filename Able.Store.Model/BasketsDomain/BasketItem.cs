using Able.Store.Infrastructure.Domain;
using Able.Store.Model.SkusDomain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Model.BasketsDomain
{

    [Table("bms_basket_item")]
    public class BasketItem : EntityBase<int>, IEntityState
    {
        public BasketItem()
        {

        }
        public BasketItem(Sku sku,Basket basket)
        {
            SkuId = sku.Id;

            ProductId = sku.Product.Id;

            CreateTime = DateTime.Now;

            BasketSku = new BasketSku(sku,this);

            TotalQty += TotalQty;

            TotalCost = TotalCost * TotalQty;
        }

        [Column("id")]
        [Key]
        public override int Id { get; set; }

        [Column("basket_id")]
        [ForeignKey("Basket")]
        public virtual int? BasketId { get; set; }

        [Required]
        public virtual Basket Basket { get; set; }

        [Column("sku_id")]
        public int SkuId { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }


        public virtual BasketSku BasketSku { get; set; }

        [Column("total_qty")]
        public int TotalQty { get; set; }

        [Column("total_cost")]
        public decimal TotalCost { get; set; }

        [NotMapped]
        public EntityState? EntityState { get; set; }
      
        internal void ChangeQtyTo(int qty)
        {
          this.TotalQty = qty;
          this.TotalCost = this.TotalQty * BasketSku.Price;
        }
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
