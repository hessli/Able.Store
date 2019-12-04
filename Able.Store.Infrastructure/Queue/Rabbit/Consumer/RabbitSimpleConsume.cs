using Able.Store.Infrastructure.Queue.Rabbit.RabbitTempContainer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Able.Store.Infrastructure.Queue.Rabbit.Consumer
{
    public class RabbitSimpleConsume: IConsumer
    {
        public RabbitConnectionFactory Factory { get; set; }

        public event ConsumeHandle  OnConsumeHandler;

        private IModel _channel;
        public RabbitSimpleConsume( )
        {
            
        }
        public void KeepAlive()
        {
            Task task = new Task(()=> {

                 while (true)
                 {  
                    if (_channel.IsClosed)
                    {
                        _channel = Factory.CreatChannel();
                    }
                    Thread.Sleep(300000);
                }
            });
            task.Start();
        }
        public void Consume(RabbitConsumerOption option)
        {
             _channel = Factory.CreatChannel();

            option.DeclareQueue(_channel);

            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(_channel);

            var properties = _channel.CreateBasicProperties();

            properties.Persistent = option.Durable;

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;

                OnConsumeHandler?.Invoke(new ConsumeArgs(body));

                if (!option.AutoAck)
                {
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            };
            _channel.BasicConsume(
                queue: option.QueueName,
                autoAck: option.AutoAck,
                 exclusive: option.Exclusive,
                consumer: consumer);

            KeepAlive();
        }
    }
}
