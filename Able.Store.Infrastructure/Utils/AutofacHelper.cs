using Autofac;

namespace Able.Store.Infrastructure.Utils
{
    public class AutofacHelper
    {
        public static IContainer Container
        {
            get;set;
             
        }
        public static T ResolveKeyed<T>(object key)
        {
            var service= Container.ResolveKeyed<T>(key);

            return service;
        }
        public static T ResolverNamed<T>(string name)
        {
            var service= Container.ResolveNamed<T>(name);


            return service;
        }
        public static T Resolver<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
