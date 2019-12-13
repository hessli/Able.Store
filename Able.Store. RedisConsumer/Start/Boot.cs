using Able.Store.Infrastructure.Cache.Redis;
using Able.Store.Infrastructure.Jobs;

namespace Able.Store.RedisConsumer.Start
{
    public class Boot
    {
        public static void Start()
        {
            JobController.AddJob(new RedisConnectionJob(), 3600000, 600000);

            JobController.Start();
        }
    }
}
