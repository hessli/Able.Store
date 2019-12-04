using Able.Store.Infrastructure.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Model.ProductsDomain
{
    [Table("pms_product_attribute_value")]
    public  class ProductAttributeValue:EntityBase<int>
    {
        [Column("id")]
        [Key]
        public  override int Id { get; set; }

        [Column("attributeid")]

        [ForeignKey("Attribute")]
        public int AttributeId { get; set; }

        [Column("value")]
        public string Value { get; set; }
        public virtual ProductAttribute Attribute { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }
        protected override void Validate()
        {
            
        }
    }
}
