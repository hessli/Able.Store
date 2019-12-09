using RabbitMQ.Client;
using System.Collections.Generic;
namespace Able.Store.Infrastructure.Queue.Rabbit
{
    public class RabbitQueueNameOption
    {

        private HashSet<string> _hash = new HashSet<string>();
        public RabbitQueueNameOption()
        {
            this.QueueNames = _hash;
        }
        public IEnumerable<string> QueueNames
        {
            get;
            private set;
        }
        public void Add(string queueName)
        {
            if (!this._hash.Contains(queueName))
            {
                this._hash.Add(queueName);
            }
        }
        public int Count {

            get {

                return this._hash.Count;
            }
        }
        public string GetIndexQueueName(int index)
        {

            var enumerator = this.QueueNames.GetEnumerator();
            int count = 0;
            while (enumerator.MoveNext())
            {
                if (count == index)
                {
                    return enumerator.Current;
                }
                count++;
            }
            return string.Empty;
        }

        internal void DeclareQueue(RabbitOption option, IModel channel)
        {
            var enumerator= this.QueueNames.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                channel.QueueDeclare(current, option.Durable, option.Exclusive, option.AutoDelete);
            }
        }
    }
}
