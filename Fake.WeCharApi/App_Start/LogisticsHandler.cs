
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Fake.WeCharApi.App_Start
{
    public class LogisticsHandler: DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            NameValueCollection query = HttpUtility.ParseQueryString(request.RequestUri.Query);
            //获取Post正文数据，比如json文本
            string fRequesContent = request.Content.ReadAsStringAsync().Result;

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}