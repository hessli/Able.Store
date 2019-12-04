using Able.Store.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Adminstrator.Model.ProductsDomain
{
    [Table("pms_product_attribute")]
    public class ProductAttribute : EntityBase<int>
    {
        [Column("id")]
        [Key]
        public override int Id { get; set; }
        [Column("product_id")]

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Column("name")]
        public string Name { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }
        protected override void Validate()
        {
        }
    }
}
