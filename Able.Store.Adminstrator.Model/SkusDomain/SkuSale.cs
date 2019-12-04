using Able.Store.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Able.Store.Adminstrator.Model.SkusDomain
{
    /// <summary>
    /// 产品销售情况
    /// </summary>
    [Table("pms_sku_sale")]
    public class SkuSale: IEntityBase<int>,ISkuSale
    {
        [Column("sku_id")]
        [Key]
        public virtual int SkuId { get; set; }

        [InverseProperty("Sale")]
        [Required]
        public Sku Sku { get; set; }
        /// <summary>
        /// 实际购买量
        /// </summary>
        [Column("sale_qty")]
        public int SaleQty
        {
            get;
            set;
        }
        /// <summary>
        /// 意向购买量 加入购物车的量
        /// </summary>
        [Column("intention_qty")]
        public int IntentionQty { get; set; }
        /// <summary>
        /// 销售价格
        /// </summary>
        /// 
        [Column("price")]
        public decimal Price { get; set; }

        [Column("create_time")]
        public  DateTime? CreateTime { get; set; }
        public void TotalIntentionQty(int qty)
        {
            this.SaleQty += qty;
        }
        public void TotalRealQty(int qty)
        {
            this.IntentionQty += qty;
        }
    }
}
