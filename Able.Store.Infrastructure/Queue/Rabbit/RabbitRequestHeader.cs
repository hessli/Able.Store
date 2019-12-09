using Able.Store.Infrastructure.Queue.Rabbit.Notify;
using Able.Store.Infrastructure.Utils;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Able.Store.Infrastructure.Queue.Rabbit
{
    public class RabbitRequestHeader
    {
        /// <summary>
        /// 模块名
        /// </summary>
        public string Module { get; set; }

        public int ModuleId { get; set; }
        /// <summary>
        /// 业务名
        /// </summary>
        public string BusinessName { get; set; }

        public string ExtentionTag { get; set; }

        public PublishMethod PublishMethod { get; set; }

        /// <summary>
        /// 业务主键
        /// </summary>
        public string BusinessId { get; set; }

        /// <summary>
        /// 是否异步通知
        /// </summary>
        public bool IsSynch { get; set; }

        /// <summary>
        /// 等待通知时长
        /// </summary>
        public int WaitTime { get; set; } = 5000;

        /// <summary>
        /// 是否需要获取发布处理结果
        /// </summary>
        public bool IsGetNotify { get; set; }
        public string GetCorrelationId()
        {
            var key = string.Concat(this.Module, ".", this.BusinessName);
            if (!string.IsNullOrWhiteSpace(this.BusinessId))
            {
                key = string.Concat(key, ".", BusinessId);
            }
            if (!string.IsNullOrWhiteSpace(this.ExtentionTag))
            {
                key = string.Concat(key, ".", this.ExtentionTag);
            }
            return key;
        }
    }

    public class RabbitRequest
    {
        public RabbitRequestHeader Header {
            get;set;
        }

        public string GetCorrelationId()
        {
            if (Header != null)
                return Header.GetCorrelationId();

            return "";
        }

        [JsonIgnore]
        public Action<RabbitResponseResult> SynchCallback { get; set; }
        public object Body { get; set; }

        public T PaseBody<T>()
        {
            var dataStr= JsonPase.Serialize(this.Body);
            var data = JsonPase.Deserialize<T>(dataStr);

            return data;
        }
        internal byte[] GetData() {

            var dataStr = JsonPase.Serialize(this);

            var dataBytes = Encoding.UTF8.GetBytes(dataStr);

            return dataBytes;
        }
    }
}
