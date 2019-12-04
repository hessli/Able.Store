using Able.Store.Infrastructure.Serve;
using System.Collections.Generic;
using System.Configuration;

namespace Able.Store.Infrastructure.Queue.Rabbit.SourceConfig
{
  public  class RabbitConnectConfigurationSource : IConfigurationSource
    {   
        public IEnumerable<T> Load<T>() where T: class, IConnectOptions
        {
            IList<T> datas = new List<T>();

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
                    datas.Add(data as T);
                }
            }
            return datas;
        }
    }
}
