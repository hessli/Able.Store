using StackExchange.Redis;
namespace Able.Store.Infrastructure.Cache.Redis
{

    public class RedisContext
    {
        protected RedisConnectionFactory _redisConnectinFactory;
        public RedisContext()
        {
            RedisConnectionFactory redisConnectinFactory = null;
            if (RedisConnectionPool.Instance.TryGet(out redisConnectinFactory))
            {
                _redisConnectinFactory = redisConnectinFactory;
            }
        }
        public IDatabase GetDatabase(int index)
        {
            return _redisConnectinFactory.GetDatabase(index);
        }
        public ISubscriber GetSubscriber()
        {
           var  connection = _redisConnectinFactory.Connection;

           var suber= connection.GetSubscriber();

           return suber;
        }
    }
}
