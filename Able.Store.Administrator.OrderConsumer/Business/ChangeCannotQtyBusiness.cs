using Able.Store.Administrator.IService.Skus;
using Able.Store.Infrastructure.Queue.Rabbit.Consumer;
using Able.Store.Infrastructure.Utils;
using Autofac;

namespace Able.Store.Administrator.OrderConsumer.Business
{
    public class ChangeCannotQtyBusiness
    {
        RabbitConsumerController _controller;
        public ChangeCannotQtyBusiness()
        {
            _controller = new RabbitConsumerController();
        }
        public void ChangeCannotQtyConsumer()
        {
            _controller.OnConsumeHandler += Instance_OnChangeCannotQty;
            _controller.RabbitConsume(new RabbitConsumerOption
            {
                AutoAck = true,
                AutoDelete = true,
                Durable = false,
                Exclusive = false,
                PublishMethod = Infrastructure.Queue.Rabbit.PublishMethod.简单工作队列,
                QueueName = "queue.direct.undurable.changecannotqty",
                RoutingKey = "queue.direct.undurable.changecannotqty"
            });
        }
        private void Instance_OnChangeCannotQty(ConsumeArgs data)
        {
            var request = data.GetReqeust();
            using (ILifetimeScope inner = AutofacHelper.Container.BeginLifetimeScope())
            {
                var skuService = inner.Resolve<ISkuService>();

                skuService.CreateOrderChangeCannotQty(request);
            }
        }
    }
}
