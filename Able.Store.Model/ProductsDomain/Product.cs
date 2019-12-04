using Able.Store.Infrastructure.Domain;
using Able.Store.Model.CategoriesDomain;
using Able.Store.Model.Core;
using Able.Store.Model.SkusDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Able.Store.Model.ProductsDomain
{
    [Table("pms_product")]
    public class Product : EntityBase<int>, IAggregateRoot
    {
        [Key]
        [Column("id")]
        public override int Id { get; set; }

        [ForeignKey("Category")]
        [Column("category_id")]
        public  int? CategoryId { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [NotMapped]
        public Brand Brand { get; set; }

        [Description("关键属性名")]
        [Column("property_name")]
        public string PropertyName { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("text")]
        public string Text { get; set; }
        /// <summary>
        /// 商品状态
        /// </summary>
        [Column("state")]
        public ProductState State { get; set; }
        /// <summary>
        /// 商品图片
        /// </summary>
        [Column("img")]
        public string Img { get; set; }
        /// <summary>
        /// sku
        /// </summary>
        public virtual ICollection<Sku> Skus { get; set; }

        [Column("publish_time")]
        public DateTime? PublishTime { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }

        [NotMapped]
        public IList<ProductPropertyValue> PropertyValues { get; set; }

     
        protected override void Validate()
        {
            
        }
    }
}
