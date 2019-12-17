namespace Able.Store.Infrastructure.Queue.Rabbit
{
    public class RabbitRequestHeader
    {
        /// <summary>
        /// 模块名
        /// </summary>
        public string Module { get; set; }
        /// <summary>
        /// 业务名
        /// </summary>
        public string BusinessName { get; set; }
        /// <summary>
        /// 业务主键
        /// </summary>
        public string BusinessId { get; set; }

        public string ExtentionTag { get; set; }

        public PublishMethod PublishMethod { get; set; }

      
        /// <summary>
        /// 等待通知时长
        /// </summary>
        public int WaitTime { get; set; } = 5000;
  
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

}
