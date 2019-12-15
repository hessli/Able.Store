using Able.Store.Infrastructure.Domain.Events;
using Able.Store.IService.Shopping;
using Able.Store.Model.OrdersDomain.Events;
using Able.Store.QueueService.Interface.Orders;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Able.Store.Service.Orders
{
    public class OrderSystemHandler : IDomaineventHandler<bool,OrderChangeEvent>
    {
        private IOrderQueueService _queueService;
        private IShoppingService _shoppingService;
        public OrderSystemHandler(IOrderQueueService queueService, 
            IShoppingService shoppingService)
        {
            _queueService = queueService;
            _shoppingService = shoppingService;
        }
        public bool Handler(OrderChangeEvent domainEvent)
        {
            var order = domainEvent.Order;
            var  isSuccess=  _queueService.Lock(order);
            //if (isSuccess)
            //{
            //   //var request=  new BasketUserRequestView();
            //   // request.skuId = order.Items.Select(x=>x.SkuId).ToArray();
            //   // request.userid = order.UserId;
            //    //Action<object> ac = x =>
            //    //  {
            //    //      var service = (IShoppingService)x;
            //    //      service.RemoveItem(request);
            //    //  };
            //    //Task task = new Task(ac, _shoppingService);
            //    //task.Start();
            //}
            return isSuccess;
        }
    }
}
