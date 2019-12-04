using RabbitMQ.Client;

namespace Able.Store.Infrastructure.Queue.Rabbit.Consumer
{
    public class RabbitConsumerOption : RabbitOption
    {
        public string ConsumerTag { get; set; } = "";
        public string QueueName { get; set; }
        internal override void BindQueue(IModel channel)
        {
            channel.ExchangeBind(this.QueueName
                   , ExchangeOption.ExchangeName,
                   this.RoutingKey);
        }
        internal override void DeclareQueue(IModel channel)
        {
            channel.QueueDeclare(QueueName, Durable, Exclusive, AutoDelete);
        }
    }
}
