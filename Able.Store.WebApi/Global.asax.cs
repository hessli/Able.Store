using Able.Store.IService;
using Able.Store.WebApi.App_Start;
using Able.Store.WebApi.Filter;
using System.Web.Http;

namespace Able.Store.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(x =>
            {
                WebApiConfig.Register(x);
                x.Filters.Add(new ExceptionFilter());
                x.Filters.Add(new StoreAuthorizeFilter());
            });
            AutofacBoot.Init();
            AutoMapperBootStrapper.ConfigureAutoMapper();

            Boot.Init();
        }
    }
}
