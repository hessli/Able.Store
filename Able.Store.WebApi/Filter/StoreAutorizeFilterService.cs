using Able.Store.Infrastructure.Cache;
using Able.Store.Infrastructure.Utils;
using Able.Store.IService;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
namespace Able.Store.WebApi.Filter
{
    public class StoreAutorizeFilterService
    {
        private HttpActionContext _actionContext;
        private ICacheStorage _storage;
        public StoreAutorizeFilterService(HttpActionContext actionContext)
        {
            _actionContext = actionContext;
            _storage = AutofacHelper.Resolver<ICacheStorage>();
        }
        /// <summary>
        /// 判断是否允许匿名访问
        /// </summary>
        /// <returns></returns>
        public bool CanAnoymousScan()
        {
            var descriptor = ((System.Web.Http.Controllers.ReflectedHttpActionDescriptor)_actionContext
              .ActionDescriptor);

            var controllerAnoymous = _actionContext.ControllerContext
                                .ControllerDescriptor
                                .GetCustomAttributes<StoreAnonymousAttribute>();

            var methodIsAnoymous = descriptor.MethodInfo.CustomAttributes
                 .Any(x => x.AttributeType == typeof(StoreAnonymousAttribute));

            return (methodIsAnoymous ||
                (controllerAnoymous != null && controllerAnoymous.Count > 0) &&
                !descriptor.MethodInfo.CustomAttributes.Any(x => x.AttributeType == typeof(StoreAuthorizeAttribute)));
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        public ResponseView<TokenModel> GetTokenModelResponse()
        {
            IEnumerable<string> tokenHeads = null;
            ResponseView<TokenModel> response=null;
            if (_actionContext.Request.Headers.TryGetValues("toke", out tokenHeads))
            {
                var tokenStr = tokenHeads.First();

                response = TokenModel.TryGetTokenModel(tokenStr);
            }
            else response = new ResponseView<TokenModel>("非法请求",false,null);

            return response;
        }
    }
}