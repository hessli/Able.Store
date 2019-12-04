using Able.Store.Infrastructure.Queue.Rabbit.RabbitTempContainer;
using RabbitMQ.Client;
using System;
namespace Able.Store.Infrastructure.Queue.Rabbit.Product
{
    public class PublishSubscribePushed : IRabbitPushed
    {
        public RabbitConnectionFactory Factory { get; set; }
        private Action<ulong, bool> _basicAckCall = null;
        public void Pushed(RabbitRequest request,
                          RabbitOption option,
                          Action<ulong, bool> basicAckCall = null)
        {
            if (option.ExchangeOption == null)
                throw new ArgumentNullException("未指定交换机信息");

            var channel = Factory.CreatChannel();

            _basicAckCall = basicAckCall;

            //声明队列
            option.DeclareQueue(channel);

            //声明交换机
            option.ExchangeDeclare(channel);

            //将交换机和队列绑定
            option.BindQueue(channel);

            var properties = channel.CreateBasicProperties();

            properties.Persistent = option.Durable;

            if (!option.AutoAck && _basicAckCall != null)
            {
                channel.BasicAcks += Channel_BasicAcks;
                channel.BasicNacks += Channel_BasicNacks;
            }
            channel.BasicPublish(exchange: option.ExchangeOption.ExchangeName,
                                 routingKey: option.RoutingKey,
                                 basicProperties: properties,
                                 body: request.GetData());
            if (option.AutoAck)
            {
                channel.Close();
            }
        }
        private void Channel_BasicNacks(object sender, RabbitMQ.Client.Events.BasicNackEventArgs e)
        {
            _basicAckCall(e.DeliveryTag, false);
        }

        private void Channel_BasicAcks(object sender, RabbitMQ.Client.Events.BasicAckEventArgs e)
        {
            _basicAckCall(e.DeliveryTag, true);
        }
    }
}
