using Able.Store.RedisConsumer.DAO;

namespace Able.Store.RedisConsumer.Business.Orders
{
    public class SaleOrderBusiness
    {
        private SaleOrderDAO saleOrderDAO;
        public SaleOrderBusiness()
        {
            saleOrderDAO = new SaleOrderDAO();
        }
        public void CreateOrder(CreateOrderRequest request)
        {
            var order = saleOrderDAO.GetOrder(request.orderId);

            order.SetStateTo(CommData.Orders.OrderStatus.待支付);

            saleOrderDAO.UpdateState(order);
        }
    }
}
