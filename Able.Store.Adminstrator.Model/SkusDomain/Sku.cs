﻿using Able.Store.Adminstrator.Model.Core;
using Able.Store.Adminstrator.Model.ProductsDomain;
using Able.Store.Infrastructure.Domain;
using Able.Store.Infrastructure.Domain.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Adminstrator.Model.SkusDomain
{
    /// <summary>
    /// 商品Sku信息
    /// </summary>
    [Table("pms_sku")]
    public class Sku : EntityBase<int>,IAggregateRoot
    {
        
        [Key]
        [Column("id")]
        public override int Id { get; set; }

        [Column("product_id")]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
      

        [Column("sku_code")]
        public string SkuCode { get; set; }
       
        public virtual SkuSale Sale { get; set; }


        [Column("property_value")]
        public string PropertyValue { get; set; }

        [Column("title")]
        public string Title { get; set; }
        /// <summary>
        /// 商品图片路径和和图片描述
        /// </summary>
        /// 
        [Column("pic_and_desc")]
        public string PicAndDesc { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
          [Column("content")]
        public string Content { get; set; }

        [Column("tag")]
        [EnumDataType(typeof(ProductTag))]
        public ProductTag Tag { get; set; }

       public virtual SkuStock Stock { get; set; }
        
        [Column("publish_time")]
        public DateTime? PublishTime { get; set; }


        [Column("state")]
        public ProductState State { get; set; }

        public virtual ICollection<SkuAttribute> Attributes { get; set; }

        [Column("sort")]
        public int Sort { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }
        private ISkuTag _skuTag;
        [NotMapped]
        public ISkuTag SkuTag
        {
            get
            {
                if (_skuTag == null)
                {
                    _skuTag = SkuTagFactory.GetProductTag(Tag, this);
                }
                return _skuTag;
            }
        }
        private ImgJsonCollection _imgs = null;
        [NotMapped]
        public ImgJsonCollection SkuImgs
        {
            get
            {
                if (_imgs == null)
                {
                    _imgs = new ImgJsonCollection(this.Id,this.PicAndDesc);
                }
                return _imgs;
            }
        }
        /// <summary>
        /// 变更不可用量
        /// </summary>
        public void ChangeCannotQty(int qty)
        {
            var resultQty= this.Stock.CannotQty +qty;

            if (resultQty > this.Stock.Qty)
            {
                base.AddBrokenRule(new BusinessRule("", string.Format("{0}当前数量超过库存总量", this.Title)));
                return;
            }
            if (this.Stock.CannotQty < 0  || resultQty<0)
            {
                base.AddBrokenRule(new BusinessRule("", string.Format("{0}当前数量异常",this.Title)));
                return;
            }
            this.Stock.CannotQty = resultQty;
        }
        /// <summary>
        /// 扣减库存总数
        /// </summary>
        public void Deduction(int qty)
        {
            this.Stock.Qty -= qty;

            if (this.Stock.Qty < 0)
                base.AddBrokenRule(new BusinessRule("", string.Format("{0}当前数量异常", this.Title)));
        }
        protected override void Validate()
        {
            if (this.State == ProductState.上架 &&
                (this.Sale==null || 
                this.Sale.Price==default(decimal)))
            {
                base.AddBrokenRule(new BusinessRule ("Sale","未设置销售价格"));
            }
        }
    }
}
