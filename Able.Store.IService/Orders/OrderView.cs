using Able.Store.Model.OrdersDomain;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Able.Store.IService.Orders
{
    public class OrderView
    {
        public OrderView()
        {
            this.product = new List<OrderItemView>();
        }
        public int id { get; set; }
        public string orderNo { get; set; }

        public int state { get; set; }
        public decimal freight { get; set; }
        public DateTime dateAdd { get; set; }
        public DateTime datePay { get; set; }
        public DateTime dateDelivery { get; set; }
        public DateTime dateReceipt { get; set; }
        public DateTime dateClose { get; set; }
        /// <summary>
        /// 商品总额
        /// </summary>
        public decimal commodity { get; set; }

        public string wuliu { get; set; } = "快递";

        public string invoicetitle { get; set; } = "不想开发票";

        public decimal total
        {
            get
            {
                return freight + commodity;
            }
        }
        public string tel { get; set; }
        public string name { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string detailed { get; set; }
        public IList<OrderItemView> product { get; set; }
        public static OrderView ToView(Order order)
        {
            OrderView view = new OrderView
            {

                id = order.Id,
                city = order.Receiver.OrderAddress.City,
                county = order.Receiver.OrderAddress.Area,
                detailed = order.Receiver.OrderAddress.Detail,
                province = order.Receiver.OrderAddress.Province,
                tel = order.Receiver.Tel,
                name = order.Receiver.ReceiverName,
                orderNo = order.OrderNo,
                commodity = order.TotalCost,
                state = (int)order.Status
            };

            var actions=  order.OrderActions.ToList();
            foreach (var item in actions)
            {
                switch (item.Action)
                {
                    case Model.Core.OrderActionEnum.订购:
                        view.dateAdd = item.ActionTime;
                        break;
                    case Model.Core.OrderActionEnum.支付:
                        view.datePay = item.ActionTime;
                        break;
                    case Model.Core.OrderActionEnum.发货:
                        view.dateDelivery = item.ActionTime;
                        break;
                    case Model.Core.OrderActionEnum.签收:
                        view.dateReceipt = item.ActionTime;
                        break;
                    case Model.Core.OrderActionEnum.取消:
                        view.dateClose = item.ActionTime;
                        break;
                }
            }

            var items = order.Items.ToList();

            foreach (var item in items)
            {
               var skuItem= OrderItemView.ToItemView(item);

                view.product.Add(skuItem);
            }
            return view;
        }
    }
    public class OrderItemView
    {
        public int skuId { get; set; }
        public int productId { get; set; }
        public string img { get; set; }
        public string title { get; set; }
        public string property { get; set; }
        public string propertyValue { get; set; }
        public decimal price { get; set; }
        public int qty { get; set; }
        public static OrderItemView ToItemView(OrderItem item)
        {
            OrderItemView view = new OrderItemView
            {
                img = item.GetIndexImg(0),
                price = item.Sku.Price,
                property = item.Sku.PropertyName,
                propertyValue = item.Sku.PropertyValue,
                productId = item.ProductId,
                skuId = item.SkuId,
                qty = item.TotalQty,
                title = item.Sku.Title
            };

            return view;
        }
    }
}
