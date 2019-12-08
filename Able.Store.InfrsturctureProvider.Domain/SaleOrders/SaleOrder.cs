using System;
using Able.Store.Infrastructure.Domain;

namespace Able.Store.InfrsturctureProvider.Domain.SaleOrders
{
    public class SaleOrder : EntityBase<int>, IAggregateRoot
    {
        public SaleOrder()
        {

        }

        public SaleOrder(int orderId, string orderNo, 
            string tagName, string requestData)
        {
            this.OrderId = orderId;

            this.OrderNo = OrderNo;

            this.PlaceOrder = new PlaceOrder(tagName, requestData);
        }
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public virtual PlaceOrder PlaceOrder { get; set; }
        public override int Id { get; set; }
        public override DateTime? CreateTime { get; set; }

        public void SetPlaceOrderResult(string content, bool isSuccess,string reason)
        { 
            PlaceOrder.SetResult(content, isSuccess,reason);
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
