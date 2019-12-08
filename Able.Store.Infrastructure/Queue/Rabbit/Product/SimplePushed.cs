using Able.Store.Infrastructure.Queue.RabbitTempContainer;
using RabbitMQ.Client;
using System;

namespace Able.Store.Infrastructure.Queue.Rabbit.Product
{
    public class SimplePushed : IRabbitPushed
    {
        public RabbitConnectionFactory Factory { get; set; }

        private Action<ulong, bool> _basicAckCall;
        public void Pushed(RabbitRequest request, RabbitOption option,
            Action<ulong, bool> basicAckCall = null)
        {
                _basicAckCall = basicAckCall;
            
                var channel = Factory.CreatChannel();

                option.DeclareQueue(channel);

                var properties = channel.CreateBasicProperties();

                properties.Persistent = option.Durable;

                if (!option.AutoAck)
                {
                    channel.ConfirmSelect();
                    channel.BasicAcks += Channel_BasicAcks;
                    channel.BasicNacks += Channel_BasicNacks;
                }
                channel.BasicPublish(exchange: "",
                                    routingKey: option.RoutingKey,
                                    basicProperties: properties,
                                    body: request.GetData());
            if (option.AutoAck)
            {
                channel.Close();
            }

        }
        private void Channel_BasicNacks(object sender,
            RabbitMQ.Client.Events.BasicNackEventArgs e)
        {
            _basicAckCall?.Invoke(e.DeliveryTag, false);
        }
        private void Channel_BasicAcks(object sender, RabbitMQ.Client.Events.BasicAckEventArgs e)
        {
            _basicAckCall?.Invoke(e.DeliveryTag, true);
        }
    }
}
