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
        public OrderQueueService()
        {
            _controller = new RabbitProductController();
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
                    BusinessName = "create.change.qty",
                    IsGetNotify = true,
                    IsSynch = false,
                    Module = "order",
                    PublishMethod = PublishMethod.简单工作队列,
                };
            request.Body = LockInventoryItemBody.ToBodys(order.Items);

            var result = _controller.Push(request, option);

            return result == null ? false : result.IsSuccess;
        }

        public bool Notify()
        {
            throw new NotImplementedException();
        }

        public bool PutShipping()
        {
            throw new NotImplementedException();
        }
    }
}
