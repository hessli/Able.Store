using Able.Store.Infrastructure.Cache.Redis;
using Able.Store.Infrastructure.Cache.Redis.RabbitTempContainer;
using Able.Store.Infrastructure.Jobs;
using Able.Store.Infrastructure.Serve;

namespace Able.Store.Infrastructure.Cache.RabbitTempContainer
{
    public class RedisConnectionJob : IJob
    {
        public void Excute()
        {
            IConfigurationSource source = new RedisConnectConfigurationSource();

            var datas=source.Load<RedisConnectOptions>();

            foreach (var item in datas)
            {
                RedisConnectionPool.Instance.Add(item,new RedisConnectionFactory(item));
            }
        }
    }
}
