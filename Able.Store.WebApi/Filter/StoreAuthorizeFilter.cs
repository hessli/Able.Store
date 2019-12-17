using Able.Store.Infrastructure.Utils;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
namespace Able.Store.WebApi.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class StoreAuthorizeFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            StoreAutorizeFilterService storeAutorizeFilterService = 
                new StoreAutorizeFilterService(actionContext);

           var  anoymous=  storeAutorizeFilterService.CanAnoymousScan();

            if (!anoymous)
            {
               var response=storeAutorizeFilterService.GetTokenModelResponse();

                if (!response.issuccess)
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
                    actionContext.Response.Content = new StringContent(JsonPase.Serialize(response));
                    return;
                }
            }
            base.OnActionExecuting(actionContext);
        }
    }
}