using Able.Store.Infrastructure.Cache.Redis;

namespace Able.Store.RedisConsumer.Start
{
    public class Boot
    {
        public static void Start()
        {
            var redisConnection= new RedisConnectionJob();
            redisConnection.Excute();
            ColumnMapper.SetMapper();
        }
    }
}
