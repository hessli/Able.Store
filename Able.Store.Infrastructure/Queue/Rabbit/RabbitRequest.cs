using Able.Store.Infrastructure.Utils;
using System.Text;

namespace Able.Store.Infrastructure.Queue.Rabbit
{
    public class RabbitRequest
    { 

        public RabbitRequestHeader Header
        {
            get; set;
        }
        public string GetCorrelationId()
        {
            if (Header != null)
                return Header.GetCorrelationId();

            return "";
        }
        /// <summary>
        /// 缓存数据库
        /// </summary>
        public int CacheDbIndex { get; set; }
        /// <summary>
        /// 是否允许重复发布
        /// </summary>
        public bool AllowDuplicatePublishing { get; set; } = false;
        public object Body { get; set; }

        internal byte[] GetData()
        {

            var dataStr = JsonPase.Serialize(this);

            var dataBytes = Encoding.UTF8.GetBytes(dataStr);

            return dataBytes;
        }

        public T PaseBody<T>()
        {
            var dataStr = JsonPase.Serialize(this.Body);
            var data = JsonPase.Deserialize<T>(dataStr);

            return data;
        }

    }
}
