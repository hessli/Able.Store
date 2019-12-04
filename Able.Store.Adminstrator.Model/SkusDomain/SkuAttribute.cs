using Able.Store.Adminstrator.Model.ProductsDomain;
using Able.Store.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Adminstrator.Model.SkusDomain
{

    [Table("pms_sku_atrribute_value")]
    public class SkuAttribute:ValueOjectBase
    {
        [Column("id")]
        [Key]
         public  int Id { get; set; }

         [Column("sku_id")]
        [ForeignKey("Sku")]
        public int SkuId { get; set; }
        public Sku Sku { get; set; }
         public virtual ProductAttribute  Attribute { get; set; }
         public virtual ProductAttributeValue AttributeValue { get; set; }

        [Column("attribute_id")]
        public int AttributeId { get; set; }

        [Column("attribute_value_id")]
        public int AttributeValueId { get; set; }

        [Column("create_time")]
        public DateTime? CreateTime { get; set; }
        protected override void Validate()
        {
            
        }
    }
}
