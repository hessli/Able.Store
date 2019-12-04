using Able.Store.Infrastructure.Queue.Rabbit.Consumer;
using Able.Store.Infrastructure.Queue.Rabbit.Product;
using System.Collections.Generic;

namespace Able.Store.Infrastructure.Queue.Rabbit
{
    public class RabbitObjFactory
    {
        public RabbitObjFactory(int index)
        {
            if (index == 0)
                this.RegisterPushed();
            else if (index == 1)
                this.RegisterConsumer();
        }
        private Dictionary<PublishMethod, IRabbitPushed> _dicPubshed;
        private Dictionary<PublishMethod, IConsumer> _dicConsumer;

        internal IRabbitPushed GetPushed(PublishMethod method)
        {
            return _dicPubshed[method];
        }
        internal IConsumer GetConsumer(PublishMethod method)
        {
            return _dicConsumer[method];
        }
        private void RegisterConsumer()
        {
            if(_dicConsumer==null)
            _dicConsumer = new Dictionary<PublishMethod, IConsumer>();

            _dicConsumer.Add(PublishMethod.简单工作队列, new RabbitSimpleConsume());
            _dicConsumer.Add(PublishMethod.发布订阅,new PublishSubscribeConsumer());
        }
        private void RegisterPushed()
        {
            if (_dicPubshed == null)
            {
                _dicPubshed = new Dictionary<PublishMethod, IRabbitPushed>();
            }
            _dicPubshed.Add(PublishMethod.简单工作队列, new SimplePushed());
            _dicPubshed.Add(PublishMethod.发布订阅, new PublishSubscribePushed());
        }
        public static IConsumer CreateConsumer(PublishMethod method)
        {
            RabbitObjFactory rabbitObjFactory = new RabbitObjFactory(1);

            var consumer = rabbitObjFactory.GetConsumer(method);
            return consumer;
        }

        public static IRabbitPushed CreatePushed(PublishMethod method)
        {
            RabbitObjFactory rabbitObjFactory = new RabbitObjFactory(0);
            var pushed = rabbitObjFactory.GetPushed(method);
            return pushed;
        }
    }
}
