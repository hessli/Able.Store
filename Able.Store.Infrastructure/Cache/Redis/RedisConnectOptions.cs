
using Able.Store.Infrastructure.Serve;

namespace Able.Store.Infrastructure.Cache.Redis
{
    public class RedisConnectOptions : IConnectOptions
    {
        public string TagName
        {

            get;set;
        }
        public bool AbortOnConnectFail { get; internal set; }

        public string PassWord { get; internal set; }

        public int Port { get; internal set; }

        public int SyncTimeout { get; set; }
        public int ConnectRetry { get; set; } = 5;

        public string Account { get; internal set; }
        public string Host { get; internal set; }
        public bool AllowAdmin { get; internal set; }
        public string ConnctionString
        {
            get
            {
                return string.Format("{0}:{1}", Host, Port);
            }
        }
        public int CompareTo(object obj)
        {
            var other = (RedisConnectOptions)obj;

            if (other != null && other.TagName.Equals(this.TagName))
            {
                if ((other == this))
                    return 1;

                if (this.Host == other.Host &&
                  this.Port == other.Port && 
                  this.PassWord==other.PassWord &&
                  this.ConnectRetry==this.ConnectRetry )
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
