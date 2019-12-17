using Able.Store.Infrastructure.Queue.Product;
using Able.Store.Infrastructure.Queue.Rabbit;
using Able.Store.Infrastructure.Queue.Rabbit.Product;
using Able.Store.Infrastructure.Utils;
using Able.Store.QueueService.Interface.Systems;
using System;

namespace Able.Store.QueueService.Implementations.Systems
{
    public class SystemQueueService :
        ISystemQueueService
    {
        public void PublisSystemException(string actionName,int controllerHashCode,
            string controllerName,Exception exception)
        {
            RabbitProductController controller = AutofacHelper.Resolver<RabbitProductController>();

            var request = new RabbitRequest
            {
                Body = exception,
                Header = new RabbitRequestHeader
                {
                    BusinessId = string.Concat(actionName, controllerHashCode),
                    BusinessName = actionName,
                    Module = controllerName,
                },
               AllowDuplicatePublishing=true
            };
            var productOption = new RabbitProductOption
            {
                AutoDelete = true,
                Durable = false,
                PublishMethod = PublishMethod.发布订阅,
                Exclusive = false,
                ExchangeOption = new RabbitExchangeOption
                {
                    AutoDelete = true,
                    Durable = false,
                    ChangeType = ChangeType.Fanout,
                    ExchangeName = "exception.nodurable.fanout",
                    Exclusive = false
                },
                AutoAck = true,
                QueueNameOption = new RabbitQueueNameOption()
            };
            //通知系统
            productOption.QueueNameOption.Add("exception.nodurable.fanout.notifysys");
            //日志系统
            productOption.QueueNameOption.Add("exception.nodurable.fanout.logsys");
            controller.Push(request, productOption);
        }
    }
}
