
using RabbitMQ.Client;

namespace Able.Store.Infrastructure.Queue.Rabbit.Product
{
    public class RabbitProductOption : RabbitOption
    {
        public static int RabbitErrorCode = 1001;

        public static int BussinessErrorCode = 1002;
        public RabbitQueueNameOption QueueNameOption { get; set; }
        internal override void BindQueue(IModel channel)
        {
           var enumerator=QueueNameOption.QueueNames.GetEnumerator();

            while (enumerator.MoveNext())
            {
                channel.ExchangeBind(enumerator.Current
                    , ExchangeOption.ExchangeName, 
                    this.RoutingKey);
            }
        }
        internal override void DeclareQueue(IModel channel)
        {
            QueueNameOption.DeclareQueue(this,channel);
        }
    }
}
