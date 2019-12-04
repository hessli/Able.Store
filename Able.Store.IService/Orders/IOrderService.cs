using System.Collections.Generic;

namespace Able.Store.IService.Orders
{
    public interface IOrderService
    {
        ResponseView<int> Create(CreateOrderRequest request);
        ResponseView<IList<MerchantView>> GetPayWay();
        ResponseView<OrderView> GetOrderDetail(int userId,int orderId);
    }
}
