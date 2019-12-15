using RabbitMQ.Client;

namespace Able.Store.Infrastructure.Queue.Rabbit
{
    public abstract class RabbitOption
    {
        public RabbitOption()
        {
        }
        public RabbitExchangeOption ExchangeOption { get; set; }

        public PublishMethod PublishMethod { get; set; }


        /// <summary>
        /// 是否持久化
        /// </summary>
        public bool Durable { get; set; } = false; 
        /// <summary>
        /// 是否独占
        /// </summary>
        public bool Exclusive { get; set; }

        /// <summary>
        /// 是否自动删除
        /// </summary>
        public bool AutoDelete { get; set; }

        /// <summary>
        /// 是否自动应答
        /// </summary>
        public bool AutoAck { get; set; }
        /// <summary>
        /// 等待时间
        /// </summary>
        public System.TimeSpan WaitTime { get; set; }
        public string RoutingKey
        {
            get; set;
        } = "";
        /// <summary>
        /// 绑定交换机
        /// </summary>
        internal abstract void BindQueue(IModel channel);
        
        /// <summary>
        /// 声明队列
        /// </summary>
        /// <param name="channel"></param>
        internal abstract void DeclareQueue(IModel channel);

        /// <summary>
        /// 声明交换机
        /// </summary>
        /// <param name="channel"></param>
        internal void ExchangeDeclare(IModel channel)
        {
            ExchangeOption.ExchangeDeclare(channel);
        }
    }

}
