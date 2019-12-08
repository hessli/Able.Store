using Abl.Store.Administrator.Repository.EF;
using Abl.Store.Administrator.Repository.Skus;
using Able.Store.Administrator.CacheService;
using Able.Store.Administrator.IService.Skus;
using Able.Store.Administrator.Service.Skus;
using Able.Store.Adminstrator.Model.SkusDomain;
using Able.Store.Infrastructure.Cache.RedisTempContainer;
using Able.Store.Infrastructure.Jobs;
using Able.Store.Infrastructure.Queue.Rabbit;
using Able.Store.Infrastructure.Queue.Rabbit.Consumer;
using Able.Store.Infrastructure.Queue.Rabbit.Product;
using Able.Store.Infrastructure.Queue.RabbitTempContainer;
using Able.Store.Infrastructure.UniOfWork;
using Able.Store.Infrastructure.Utils;
using Autofac;

namespace Able.Store.Administrator.OrderConsumer
{
    public class ObjectManager
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SimplePushed>().Keyed<IRabbitPushed>(PublishMethod.简单工作队列);
            builder.RegisterType<PublishSubscribePushed>().Keyed<IRabbitPushed>(PublishMethod.发布订阅);

            builder.RegisterType<RabbitSimpleConsume>()
                .Keyed<IConsumer>(PublishMethod.简单工作队列);
            builder.RegisterType<PublishSubscribeConsumer>()
                .Keyed<IConsumer>(PublishMethod.发布订阅);
            builder.RegisterType<SkuRepository>()
                .As<ISkuRepository>();
            builder.RegisterType<SkuService>().As<ISkuService>();
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<SkuCacheService>().As<ISkuCacheService>();
           
            AutofacHelper.Container = builder.Build();

        

        }
        public static void Start()
        {

            var job= new RabbitConnectionJob();

            job.Excute();

            var redis = new RedisConnectionJob();
            redis.Excute();
            JobController.AddJob(job, 1300000, 300000);
            JobController.AddJob(redis, 1200000, 150000);
            JobController.Start();
        }
        public ObjectManager()
        {

        }

      

    }
}
