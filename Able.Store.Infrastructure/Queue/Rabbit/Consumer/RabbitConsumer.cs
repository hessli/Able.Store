using Able.Store.Infrastructure.Queue.RabbitTempContainer;

namespace Able.Store.Infrastructure.Queue.Rabbit.Consumer
{
    public class RabbitConsumer : IConsumer
    {
        public RabbitConnectionFactory Factory { get; set; }

        public event ConsumeHandle OnConsumeHandler;
        public void Consume(RabbitConsumerOption option)
        {
            var consumer = RabbitObjFactory.CreateConsumer(option.PublishMethod);

            consumer.OnConsumeHandler += OnConsumeHandler;

            consumer.Factory = Factory;

            consumer.Consume(option);
        }
    }
}
