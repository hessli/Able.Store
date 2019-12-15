using Able.Store.Infrastructure.ConfigCenter;
using Able.Store.Infrastructure.Queue.Rabbit;
using Able.Store.Infrastructure.Queue.Rabbit.XmlConfig;
using System.Collections.Generic;
using System.Configuration;

namespace Able.Store.Infrastructure.Queue.RabbitTempContainer
{
    public  class RabbitConnectFromXml : IConfigurationSource
    {   
        public IEnumerable<IConnectOptions> Load()
        {
            IList<IConnectOptions> datas = new List<IConnectOptions>();

            var cfg = (RabbitOptionConfig)ConfigurationManager.GetSection("rabbitOption");

            if (cfg != null)
            {
                foreach (RabbitOptionElement item in cfg.Elements)
                {
                    IConnectOptions data = new RabbitConnectOptions
                    {
                        Account = item.Account,
                        Host = item.Host,
                        Port = item.Port,
                        TagName = item.TagName,
                        PassWord = item.PassWord,
                        VirtualHost = item.VirtualHost
                    };
                    datas.Add(data);
                }
            }
            return datas;
        }
    }
}
