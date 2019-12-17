using Able.Store.Infrastructure.Utils;
using Able.Store.QueueService.Interface.Systems;
using System;
using System.Web.Http.Filters;
namespace Able.Store.WebApi.Filter
{
    public class ExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //所有的异常全部发送到日志系统；

            try
            {
                var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
                var controllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                ISystemQueueService systemQueueService = AutofacHelper.Resolver<ISystemQueueService>();
                var hashCode = actionExecutedContext.ActionContext.ActionDescriptor.GetHashCode();
                systemQueueService.PublisSystemException(actionName, hashCode, controllerName, actionExecutedContext.Exception);
            }
            catch (Exception ex)
            {
               //只能即时调通知系统和日志系统接口.
            }
            base.OnException(actionExecutedContext);
        }
    }
}