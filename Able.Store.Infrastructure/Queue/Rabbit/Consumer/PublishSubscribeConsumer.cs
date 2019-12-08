using Able.Store.Infrastructure.Queue.RabbitTempContainer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Able.Store.Infrastructure.Queue.Rabbit.Consumer
{
   public class PublishSubscribeConsumer : IConsumer
    {
        public RabbitConnectionFactory Factory { get; set; }

        private IModel _channel;

        public event ConsumeHandle OnConsumeHandler;

        public void KeepAlive()
        {
            Task task = new Task(() => {

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
            var channel = Factory.CreatChannel();

            option.ExchangeDeclare(channel);

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;

                var message = Encoding.UTF8.GetString(body);

                OnConsumeHandler?.Invoke(new ConsumeArgs(body));

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: option.QueueName,
                       autoAck: option.AutoAck,
                       exclusive: option.Exclusive,
                       consumerTag: option.ConsumerTag,
                       consumer: consumer);

            KeepAlive();
        }
    }
}
