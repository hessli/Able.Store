using Able.Store.Model.BasketsDomain;
using Able.Store.Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Model.OrdersDomain
{

    [Table("oms_order_item_sku")]
    public class OrderItemSku
    {
        public OrderItemSku()
        {

        }
        public OrderItemSku(BasketSku  sku)
        {
            this.Content = sku.Content;
            this.Title = sku.Title;
            this.Price = sku.Price;
            this.PicAndDesc = sku.PicAndDesc;
            this.Content = sku.Content;
            this.PropertyName = sku.PropertyName;
            this.PropertyValue = sku.PropertyValue;
            this.CreateTime = DateTime.Now;
            OrderItemSkuAttributes = new List<OrderItemSkuAttribute>();
            foreach (var item in sku.BasketSkuAttributes)
            {
                OrderItemSkuAttributes.Add(new OrderItemSkuAttribute (item));
            }
        }

        [Column("id")]
        public int Id { get; set; }

        [Column("order_item_id")]
        [ForeignKey("OrderItem")]
        [Key]
        public int OrderItemId { get; set; }

        [Required]
        public virtual OrderItem OrderItem { get; set; }

        [Column("title")]

        public virtual ICollection<OrderItemSkuAttribute> OrderItemSkuAttributes { get; set; }

        public string Title { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("property_name")]
        public string PropertyName { get; set; }

        [Column("property_value")]
        public string PropertyValue { get; set; }

        [Column("pic_and_desc")]
        public string PicAndDesc { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        [Column("content")]
        public string Content { get; set; }
        [Column("create_time")]
        public DateTime CreateTime { get; set; }

        private ImgJsonCollection _imgJsons=null;
        public ImgJsonCollection GetImgCollections(int skuId)
        {
             _imgJsons =  new ImgJsonCollection(skuId, this.PicAndDesc);

            return _imgJsons;
        }

        public string GetIndexImg(int skuId, int index)
        {
            if (_imgJsons == null)
            {
                GetImgCollections(skuId);
            }
            if (_imgJsons != null && index < _imgJsons.Count)
            {
                return _imgJsons.SkuImgs[index].Img;
            }
            return "";
        }
    }
}
