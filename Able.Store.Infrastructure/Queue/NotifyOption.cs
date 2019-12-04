using System;

namespace Able.Store.Infrastructure.Queue
{
    public class NotifyOption
    {
        /// <summary>
        /// 模块名
        /// </summary>
        public string Module { get; set; }
        /// <summary>
        /// 业务名
        /// </summary>
        public string BusinessName { get; set; }

        public string ExtentionTag { get; set; }

        /// <summary>
        /// 业务主键
        /// </summary>
        public string BusinessKey { get; set; }

        /// <summary>
        /// 是否异步通知
        /// </summary>
        public bool IsSynch { get; set; }

        /// <summary>
        /// 等待通知时长
        /// </summary>
        public int WaitTime { get; set; } = 5000;
        internal string NotifyKey
        {
            get
            {
                var key = string.Concat(this.Module, ".", this.BusinessName);
                if (!string.IsNullOrWhiteSpace(this.BusinessKey))
                {
                    key = string.Concat(key, ".", BusinessKey);
                }
                if (!string.IsNullOrWhiteSpace(this.ExtentionTag))
                {
                    key = string.Concat(key, ".", this.ExtentionTag);
                }
                return key;
            }
        }
    }
 
}
