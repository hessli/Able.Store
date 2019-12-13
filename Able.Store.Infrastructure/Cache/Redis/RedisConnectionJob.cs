using Able.Store.Infrastructure.ConfigCenter;
using Able.Store.Infrastructure.Jobs;
using Able.Store.Infrastructure.Queue.RabbitTempContainer;


namespace Able.Store.Infrastructure.Cache.Redis
{
    public class RedisConnectionJob : IJob
    {
        public void Excute()
        {
            IConfigurationSource source = new RabbitConnectFromDataBase();
            var data = source.Load<RedisConnectOptions>();

            foreach (var item in data)
            {
                RedisConnectionPool.Instance.Add(item,new RedisConnectionFactory(item));
            }
        }
    }
}
