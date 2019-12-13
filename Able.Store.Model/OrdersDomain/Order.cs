using Able.Store.CommData.Orders;
using Able.Store.Infrastructure.Domain;
using Able.Store.Infrastructure.Domain.Business;
using Able.Store.Model.BasketsDomain;
using Able.Store.Model.OrdersDomain.States;
using Able.Store.Model.UsersDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace Able.Store.Model.OrdersDomain
{
    /// <summary>
    /// 本来不该直接调用其他子系统的但是为了少写代码就这样吧
    /// </summary>
    [Table("oms_order")]
    public class Order:EntityBase<int>,IAggregateRoot
    {
        private IOrderState _orderState;
        private IGenerateNo _generalNo;
        public Order()
        {
            Items = new List<OrderItem>();
        }
        public Order(User userInfo,
            Basket  basket,
            IGenerateNo  generalNo,
            string message=""):this()
        {
            if (userInfo == null)
            {
                base.AddBrokenRule(new BusinessRule("用户信息", "无效的用户"));
                return;
            }
            var receiver= userInfo.GetPointReceiver();

            if (receiver == null)
            {
                base.AddBrokenRule(new BusinessRule ("","指定的收货地址无效"));
            }

            if (basket == null || basket.BasketItems.Count==0)
            {
                base.AddBrokenRule(new BusinessRule("", "未指定商品信息"));
                return;
            }
            
            _orderState = new SystemLockState();

            _generalNo = generalNo;

            this.OrderNo = _generalNo.Generate();

            this.TotalCost = basket.Cost;

            this.UserId = userInfo.Id;

            this.UserName = userInfo.Nick;

            this.TotalQty = basket.Qty;

            this.Status = _orderState.Status;

            this.CreateTime = DateTime.Now;

            this.Message = message;

            this.OrderActions = new List<OrderAction>();

            this.OrderActions.Add(CreateOrderFactory.CreateOrderAction(OrderActionType.订购));

            foreach (var item in basket.BasketItems)
            {
                var r= CreateOrderFactory.CreateOrderItems(item);

                this.Items.Add(r);
            }
           this.Receiver= CreateOrderFactory.CreateReceiver(receiver);
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
        public virtual ICollection<OrderAction> OrderActions { get; set; }
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
        public virtual ICollection<OrderPayment> Payment { get;  set; }

        //public void SetPayment(Payment payment)
        //{
        //    if (OrderHasBeenPaidFor())
        //    {
        //        throw new Exception("已支付");
        //    }
        //    if (OrderTotalMatches(payment))
        //    {
        //        Payment = payment;
        //    }
        //    else
        //    {
        //        throw new Exception("支付金额错误");
        //    }
        //    _orderState.Submit(this);
        //}

        //public bool OrderHasBeenPaidFor()
        //{
        //    return Payment !=null && OrderTotalMatches(Payment);
        //}

        public bool SystemLocker()
        {
            var lockered=  _orderState.SystemLocker(this);
            return lockered;
        }
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
