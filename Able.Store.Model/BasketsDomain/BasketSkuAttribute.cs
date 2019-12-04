using Able.Store.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Model.BasketsDomain
{

    [Table("bms_basket_item_sku_attribute")]
    public  class BasketSkuAttribute: EntityBase<int>
    {

        [Column("id")]
        [Key]
        public override int Id { get; set; }

        [Required]
        public virtual BasketSku BasketSku { get; set; }

        [Column("basket_item_id")]
        [ForeignKey("BasketSku")]
        public int? BasketItemId { get; set; }

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
