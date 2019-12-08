using Able.Store.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Adminstrator.Model.SkusDomain
{

    [Table("pms_sku_stock")]
    public class  SkuStock
    {

        [Required]
        public virtual Sku Sku { get; set; }

        [Column("sku_id")]
        [ForeignKey("Sku")]
        [Key]
        public int SkuId { get; set; }

        [Column("create_time")]
        public DateTime? CreateTime { get; set; }

        [Column("qty")]
        /// <summary>
        /// 库存数量
        /// </summary>
        public int Qty { get; set; }
     
        /// <summary>
        /// 可用数量
        /// </summary>
        [NotMapped]
        public int EffectiveQty {

            get {
                  return  Qty - CannotQty;
            }
        }
        /// <summary>
        /// 不可用库存
        /// </summary>
        [Column("cannot_qty")]
        public int CannotQty { get; set; }
    }
}
