using Able.Store.Infrastructure.Cache.RabbitTempContainer;
using Able.Store.Infrastructure.Jobs;
using Able.Store.Infrastructure.Queue.Rabbit.RabbitTempContainer;

namespace Able.Store.WebApi.App_Start
{
    public class Boot
    {
        public static void Init()
        {
            JobController.AddJob(new RabbitConnectionJob(), 1300000, 600000);

            JobController.AddJob(new RedisConnectionJob(), 3600000, 600000);

            JobController.Start();

            var rabbitJob= new RabbitConnectionJob();

            rabbitJob.Excute();

            var redisJob= new RedisConnectionJob();

            redisJob.Excute();
        }
    }
}