using Able.Store.Adminstrator.Model.Core;
using Able.Store.Adminstrator.Model.SkusDomain;
using Able.Store.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Adminstrator.Model.BasketsDomain
{

    [Table("bms_basket_item_sku")]
    
    public class BasketSku
    {
        public BasketSku()
        {
            BasketSkuAttributes = new List<BasketSkuAttribute>();
        }
        public BasketSku(Sku sku,BasketItem item):this()
        {
            Title = sku.Title;
            Price = sku.Sale.Price;
            PicAndDesc = sku.PicAndDesc;
            Tag = sku.Tag;
            Content = sku.Content;
            CreateTime = DateTime.Now;
            PropertyName = sku.Product.PropertyName;
            PropertyValue = sku.PropertyValue;
            BasketSkuAttributes= BasketSkuAttributeFacotry.CrateAttributeFor(sku.Attributes);
        }

        [Column("id")]
        public int Id { get; set; }
      
        [Column("basket_item_id")]
        [ForeignKey("BasketItem")]
        [Key]
        public int BasketItemId { get; set; }

        [Required]
        public virtual BasketItem BasketItem { get; set; }

        [Column("title")]
        
        public string Title { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("property_name")]
        public string PropertyName { get; set; }
         
        [Column("property_value")]
        public string PropertyValue { get; set; }

        [Column("pic_and_desc")]
        public string PicAndDesc { get; set; }

        [Column("tag")]
        public ProductTag Tag { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        [Column("content")]
        public string Content { get; set; }

        private ImgJsonCollection _imgs = null;
        [NotMapped]
        public ImgJsonCollection SkuImgs
        {
            get
            {
                if (_imgs == null)
                {
                    _imgs = new ImgJsonCollection(BasketItemId, this.PicAndDesc);
                }
                return _imgs;
            }
        }
        public virtual ICollection<BasketSkuAttribute> BasketSkuAttributes { get; set; }

        [Column("create_time")]
        public  DateTime? CreateTime { get; set; }
    }
}
