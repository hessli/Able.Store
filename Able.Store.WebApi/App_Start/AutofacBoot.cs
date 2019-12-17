using Able.Store.Infrastructure.Cache;
using Able.Store.Infrastructure.Cache.Redis;
using Able.Store.Infrastructure.Domain.Events;
using Able.Store.Infrastructure.Queue.Product;
using Able.Store.Infrastructure.UniOfWork;
using Able.Store.Infrastructure.Utils;
using Able.Store.Model.OrdersDomain.Events;
using Able.Store.Model.OrdersDomain.States;
using Able.Store.QueueService.Interface.Orders;
using Able.Store.Repository.EF;
using Able.Store.Service.Orders;
using Autofac;
using Autofac.Integration.WebApi;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Able.Store.WebApi
{
    public class AutofacBoot
    {
      
        internal static void Init()
        {

            HttpConfiguration config = GlobalConfiguration.Configuration;

            var builder = new ContainerBuilder();

            // AppDomain.CurrentDomain.GetAssemblies()这个方法只能获取已经加载到此应用程序域的程序集。.Net 有延迟加载机制，有的时候我们可能不能及时的获取到需要的程序集（比如在启动的时候），
            var assemblyCollections = System.Web.Compilation.BuildManager.GetReferencedAssemblies();

           var assemblys=assemblyCollections.Cast<Assembly>();

            var dic = assemblys.ToDictionary(x => x.GetName().Name, x => x);

            builder.RegisterType<DomainEventHandlerFactory>()
                .As<IDomainEventHandlerFactory>().PropertiesAutowired();

            builder.RegisterType<EFUnitOfWork>().Named<IUnitOfWork>("EFUnitOfWork")
                .AsImplementedInterfaces();

            builder.RegisterType<RedisStorage>().Named<ICacheStorage>("Redis")
                .AsImplementedInterfaces(); ;

            builder.RegisterAssemblyTypes(dic["Able.Store.Repository"], dic["Able.Store.Model"])
                .Where(x => x.Name.EndsWith("Repository") && !x.IsAbstract)
                .AsImplementedInterfaces().WithParameter(
                (a,b)=>a.ParameterType==typeof(IUnitOfWork) 
                ,(a,b)=>b.Resolve<IUnitOfWork>());

            builder.RegisterAssemblyTypes(dic["Able.Store.Service"],dic["Able.Store.IService"])
                .Where(x => x.Name.EndsWith("Service") && !x.Name.Equals("BaseService"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(dic["Able.Store.IService"]
                ,dic["Able.Store.CacheService"])
                .Where(x=>x.Name.EndsWith("CacheService"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(dic["Able.Store.QueueService"])
                .Where(x=>x.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly())
                .InstancePerRequest()
                .PropertiesAutowired();

            builder.RegisterType<RabbitProductController>().AsSelf();
            

            #region 订单状态注册
            builder.RegisterType<SystemLockState>()
             .Named<IOrderState>("SystemLockState");

            builder.RegisterType<OrderPayState>()
             .Named<IOrderState>("OrderPayState");
            #endregion

            #region 领域事件注册
            builder.RegisterType<OrderSystemHandler>()
                .Named<IDomaineventHandler<bool,OrderChangeEvent>>("OrderSystemHandler");
            builder.RegisterType<OrderDeliveryHandler>()
           .Named<IDomaineventHandler<bool, OrderChangeEvent>>("OrderDeliveryHandler");
            #endregion

            AutofacHelper.Container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(AutofacHelper.Container);
        }
    }
}