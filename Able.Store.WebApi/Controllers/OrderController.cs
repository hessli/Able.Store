using Able.Store.IService;
using Able.Store.IService.Orders;
using System.Collections.Generic;

namespace Able.Store.WebApi.Controllers
{
    public class OrderController : BaseController
    {
        public IOrderService OrderService { get; set; }
        public ResponseView<OrderView> GetOrder(int orderId)
        {
            var data= OrderService.GetOrderDetail(1,orderId);

            return data;
        }

        public ResponseView<IList<MerchantView>> GetPayWay()
        {
           var data=  OrderService.GetPayWay();

            return data;
        }

        public ResponseView<int> PostCreateOrder(CreateOrderRequest request)
        {

            request.userid = 1;
            var data=  OrderService.Create(request);
            return data;
        }
    }
}
