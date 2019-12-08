using Able.Store.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Model.SkusDomain
{

    [Table("pms_sku_stock")]
    public class  SkuStock
    {
    
        [InverseProperty("Stock")]
        [Required]
        public virtual Sku Sku { get; set; }

        [Column("sku_id")]
        [Key]
        public int SkuId { get; set; }

        [Column("create_time")]
        public DateTime? CreateTime { get; set; }

        [Column("qty")]
        /// <summary>
        /// 库存数量
        /// </summary>
        public int Qty { get; set; }
         
        [NotMapped]
        public int EffectiveQty {

            get {
                  return  Qty - this.CannotQty;
            }
        }
        /// <summary>
        /// 不可用库存
        /// </summary>
         [Column("cannot_qty")]
         public int CannotQty { get; set; }
    
    }
}
