using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Able.Store.WebApi.Controllers
{
    public  class BaseController : ApiController
    {
        protected override ResponseMessageResult ResponseMessage(HttpResponseMessage response)
        {
            var p= response;
            return base.ResponseMessage(response);
        }

    }
}
