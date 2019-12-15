using Able.Store.CommData.Orders;
using Able.Store.Infrastructure.Cache;
using Able.Store.Infrastructure.Queue.Product;
using Able.Store.Infrastructure.Queue.Rabbit;
using Able.Store.Infrastructure.Queue.Rabbit.Product;
using Able.Store.QueueService.Interface.Orders;
using System;

namespace Able.Store.QueueService.Implementations.Order
{
    public class OrderQueueService : IOrderQueueService
    {
        private RabbitProductController _controller;
 
        private readonly string MODULE = "order";
        private readonly string LOCK = "create.change.qty";
        private readonly string PUTLOGISTICS = "saleorder.putlogistics.placeorder";
        public OrderQueueService(ICacheStorage cacheStorage, 
            RabbitProductController controller)
        {
            _controller = controller;
        }
        public bool Lock(Able.Store.Model.OrdersDomain.Order order)
        {
            var option = new RabbitProductOption
            {
                AutoDelete = true,
                Exclusive = false,
                Durable = false,
                PublishMethod = PublishMethod.简单工作队列,
                AutoAck = true,
                RoutingKey = "queue.direct.undurable.changecannotqty",
                QueueNameOption = new RabbitQueueNameOption()
            };
            option.QueueNameOption.Add("queue.direct.undurable.changecannotqty");

            RabbitRequest request = new RabbitRequest();
            request.Header =
                new RabbitRequestHeader
                {
                    BusinessId = order.Id.ToString(),
                    BusinessName = LOCK,
                    Module = MODULE,
                    PublishMethod = PublishMethod.简单工作队列,
                };
            request.Body = LockInventoryItemBody.ToBodys(order.Items);

            request.CacheDbIndex = OrderStaticResource.DBINDEX;

            _controller.Push(request, option);

            return true;
      
        }

        public bool Notify()
        {
            throw new NotImplementedException();
        }

        public bool PutLogistics(Able.Store.Model.OrdersDomain.Order order)
        {

            var option = new RabbitProductOption
            {
                AutoDelete = true,
                Exclusive = false,
                Durable = false,
                PublishMethod = PublishMethod.简单工作队列,
                AutoAck = true,
                RoutingKey = "queue.direct.undurable.placeorder",
                QueueNameOption = new RabbitQueueNameOption()
            };
            option.QueueNameOption.Add("queue.direct.undurable.placeorder");

            var body = LogisticsRequestFactory.CreatePlaceOrder(order);

            RabbitRequest request = new RabbitRequest();
            request.Header =
                new RabbitRequestHeader
                {
                    BusinessId = order.Id.ToString(),
                    BusinessName = PUTLOGISTICS,
                    Module = MODULE,
                    PublishMethod = PublishMethod.简单工作队列,
                };
            request.Body = LockInventoryItemBody.ToBodys(order.Items);

            request.CacheDbIndex = OrderStaticResource.DBINDEX;

            _controller.Push(request, option);

            return true;
        }
    }
}
