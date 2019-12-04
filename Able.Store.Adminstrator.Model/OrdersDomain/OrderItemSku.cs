using Able.Store.Adminstrator.Model.BasketsDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Able.Store.Adminstrator.Model.OrdersDomain
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
      
    }
}
