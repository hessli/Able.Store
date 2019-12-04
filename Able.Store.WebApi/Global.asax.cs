
using Able.Store.IService;
using Able.Store.WebApi.App_Start;
using System.Web.Http;

namespace Able.Store.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

             AutofacBoot.Init();

             AutoMapperBootStrapper.ConfigureAutoMapper();

              Boot.Init();
        }
    }
}
