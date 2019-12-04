using System.Configuration;

namespace Able.Store.Infrastructure.Queue.Rabbit.SourceConfig
{
    public class RabbitOptionElement : ConfigurationElement
    {
        [ConfigurationProperty("tagName", IsKey = true, IsRequired = true)]
        public string TagName
        {
            get { return base["tagName"] as string; }
            set { base["tagName"] = value; }
        }
        [ConfigurationProperty("host")]
        public string Host
        {
            get { return base["host"] as string; }
            set { base["host"] = value; }
        }
        public bool AutomaticRecoveryEnabled
        {
            get
            {
                return bool.Parse((base["automaticrecoveryenabled"] as string));
            }
            set
            {
                base["automaticrecoveryenabled"] = value;
            }
        }
        [ConfigurationProperty("port")]
        public int Port
        {
            get { return int.Parse(base["port"].ToString()); }
            set { base["port"] = value; }
        }

        [ConfigurationProperty("virtualHost")]
        public string VirtualHost
        {
            get { return base["virtualHost"] == null ? null : base["virtualHost"] as string; }
            set { base["virtualHost"] = value; }
        }
        [ConfigurationProperty("passWord")]
        public string PassWord
        {
            get { return base["passWord"] as string; }
            set { base["passWord"] = value; }
        }

        [ConfigurationProperty("account")]
        public string Account
        {
            get { return base["account"] as string; }
            set { base["account"] = value; }
        }
    }
}
