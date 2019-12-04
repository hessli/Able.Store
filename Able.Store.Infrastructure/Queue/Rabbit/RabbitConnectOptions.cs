

using Able.Store.Infrastructure.Serve;

namespace Able.Store.Infrastructure.Queue.Rabbit
{
    public class RabbitConnectOptions : IConnectOptions
    {
        public string TagName
        {
            get;set;
        }
        public string Host { get; set; }
        public int Port { get; set; }
        public string VirtualHost
        {
            get; set;
        }
        public string PassWord { get; set; }
        public string Account { get; set; }

        /// <summary>
        /// 连接断开是否自动重启
        /// </summary>
        public bool AutomaticRecoveryEnabled { get; set; }

        public int CompareTo(object obj)
        {
            var other= (RabbitConnectOptions)obj;

            if (other != null &&  other.TagName.Equals(this.TagName))
            {
                if ((other == this))
                     return 1;

                if (this.Host == other.Host &&
                  this.Port == other.Port &&
                  this.VirtualHost == VirtualHost)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
