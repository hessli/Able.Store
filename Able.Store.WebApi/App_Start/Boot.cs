using Able.Store.Infrastructure.Cache.Redis;
using Able.Store.Infrastructure.Jobs;
using Able.Store.Infrastructure.Queue.RabbitTempContainer;
using Able.Store.Infrastructure.Utils;
using Able.Store.IService.Administration;
using Autofac;
namespace Able.Store.WebApi.App_Start
{
    public class Boot
    {
        public static void Init()
        {
            (new RedisConnectionJob()).Excute();
            (new RabbitConnectionJob()).Excute();

            using (var scope = AutofacHelper.Container.BeginLifetimeScope())
            {

                var service= scope.Resolve<IAdministrationCacheService>();
                service.BootStartInitAdministrationCache();
            }
              
        }
    }
}