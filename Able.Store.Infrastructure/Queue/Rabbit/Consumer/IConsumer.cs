

using Able.Store.Infrastructure.Queue.RabbitTempContainer;

namespace Able.Store.Infrastructure.Queue.Rabbit.Consumer
{
    public interface IConsumer
    {
        RabbitConnectionFactory Factory { get; set; }

        event ConsumeHandle  OnConsumeHandler;

        void Consume(RabbitConsumerOption option);
    }
}
