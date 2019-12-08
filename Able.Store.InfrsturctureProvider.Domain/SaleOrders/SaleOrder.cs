namespace Able.Store.InfrsturctureProvider.Domain.SaleOrders
{
    public class SaleOrder
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
        public PlaceOrder PlaceOrder { get; set; }

        public void SetPlaceOrderResult(string content, bool isSuccess,string reason)
        { 
            PlaceOrder.SetResult(content, isSuccess,reason);
        }
    }
}
