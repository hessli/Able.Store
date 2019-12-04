using Able.Store.Adminstrator.Model.Core;
using Able.Store.Adminstrator.Model.OrdersDomain.States;
using Able.Store.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace Able.Store.Adminstrator.Model.OrdersDomain
{
    /// <summary>
    /// 本来不该直接调用其他子系统的但是为了少写代码就这样吧
    /// </summary>
    [Table("oms_order")]
    public class Order : EntityBase<int>, IAggregateRoot
    {
        private IOrderState _orderState;
        public Order()
        {
            Items = new List<OrderItem>();
        }

        [Column("id")]
        [Key]
        public override int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("total_cost")]
        public decimal TotalCost { get; set; }

        [Column("total_qty")]
        public int TotalQty { get; set; }

        [Column("order_no")]
        public string OrderNo { get; set; }

        [Column("create_time")]
        public override DateTime? CreateTime { get; set; }

        [Column("order_status")]
        public OrderStatus Status { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; }
        /// <summary>
        /// 收货信息
        /// </summary>
        public virtual OrderReceiver Receiver { get; set; }
        /// <summary>
        /// 货运信息
        /// </summary>
        [NotMapped]
        public virtual OrderShipping OrderShipping { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("message")]
        public string Message { get; set; }

        /// <summary>
        /// 支付信息
        /// </summary>
        [NotMapped]
        public virtual ICollection<OrderPayment> Payment { get; set; }

        public string EmailMessage()
        {
            StringBuilder stb = new StringBuilder();
            stb.AppendLine("您在爱购购买了：");
            foreach (var item in Items)
            {
                stb.AppendLine(string.Format("{0} {1}", item.TotalQty, item.Sku.Title));
            }
            stb.AppendLine(string.Format("总价值:{0}元", this.TotalCost));

            return stb.ToString();
        }
        private bool OrderTotalMatches(OrderPayment payment)
        {
            return true;
        }
        internal void SetStateTo(IOrderState state)
        {
            this._orderState = state;

            this.Status = this._orderState.Status;
        }
        /// <summary>
        /// 邮件信息
        /// </summary>
        /// <returns></returns>
        protected override void Validate()
        {

        }
    }
}
