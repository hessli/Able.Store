using Able.Store.Infrastructure.Cache.Redis;
using Able.Store.Infrastructure.Cache.Redis.RedisTempContainer;
using Able.Store.Infrastructure.ConfigCenter;
using Able.Store.Infrastructure.Jobs;

namespace Able.Store.Infrastructure.Cache.RedisTempContainer
{
    public class RedisConnectionJob : IJob
    {
        public void Excute()
        {
            IConfigurationSource source = new RedisConnectFromDataBase();

            var datas=source.Load<RedisConnectOptions>();

            foreach (var item in datas)
            {
                RedisConnectionPool.Instance.Add(item,new RedisConnectionFactory(item));
            }
        }
    }
}
