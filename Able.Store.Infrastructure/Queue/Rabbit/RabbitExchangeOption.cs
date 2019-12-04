using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Able.Store.Infrastructure.Queue.Rabbit
{
   public class RabbitExchangeOption
    {
        public string ExchangeName
        {
            get; set;
        }
        /// <summary>
        /// 是否持久化
        /// </summary>
        public bool Durable { get; set; }
        /// <summary>
        /// 是否独占
        /// </summary>
        public bool Exclusive { get; set; }

        /// <summary>
        /// 是否自动删除
        /// </summary>
        public bool AutoDelete { get; set; }

        public ChangeType ChangeType { get; set; }

        public override string ToString()
        {
            if (ChangeType == default(ChangeType))
                return "";

           return    ChangeType.ToString();
        }
        internal void ExchangeDeclare(IModel channel)
        {
            channel.ExchangeDeclare(
                 exchange: ExchangeName,
                 type: this.ToString(),
                 durable: Durable,
                 autoDelete:AutoDelete);
        }
    }
}
