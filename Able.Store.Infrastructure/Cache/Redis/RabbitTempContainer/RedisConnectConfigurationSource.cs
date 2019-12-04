using Able.Store.Infrastructure.Serve;
using System.Collections.Generic;

namespace Able.Store.Infrastructure.Cache.Redis.RabbitTempContainer
{
    public class RedisConnectConfigurationSource :IConfigurationSource
    {
        public IEnumerable<T> Load<T>() where T : class, IConnectOptions
        {
            IList<T> datas = new List<T>();

            IConnectOptions connection = new RedisConnectOptions
            {
                AbortOnConnectFail = true,
                Host = "192.168.229.131",
                PassWord = "1234",
                Port = 6379,
                SyncTimeout = 15000,
                 TagName="redis131",
                Account = "root",
                AllowAdmin = true
            };

            datas.Add(connection as T);

            return datas;
        }
    }
}
