using System.IO;
using System.Net;

namespace Able.Store.Infrastructure.Http
{
    public class HttpWebRequestUtility
    {
        static readonly string HTTP_METHOD_GET = "GET";
        static readonly string HTTP_METHOD_POST = "POST";
        static readonly string CONTENT_TYPE_FORM = "application/x-www-form-urlencoded";
        static readonly string CONTENT_TYPE_JSON = "application/json";
        public HttpWebRequestOption HttpWebRequestOption { get; private set; }
       
        public HttpWebRequestUtility(HttpWebRequestOption option,
            IHttpParamter parameter)
        {
            HttpWebRequestOption = option;

            string uri = "";

            if (HttpWebRequestOption.HttpMethod == HttpMethod.GET &&
                parameter != null)
            {
                uri = HttpWebRequestOption.CreateGetUri(parameter.GetParameter());
            }
            else uri = option.Uri;

            HttpWebRequest request =  WebRequest.Create(uri) as HttpWebRequest;

            request.UserAgent = HttpWebRequestOption.UserAgent;

            request.Timeout = HttpWebRequestOption.TimeOut;

            if (option.HttpMethod == HttpMethod.POST)
            {
                switch (HttpWebRequestOption.ContentType)
                {
                    case HttpContentType.FORM:
                        request.ContentType = CONTENT_TYPE_FORM;
                        break;

                    case HttpContentType.JSON:
                        request.ContentType = CONTENT_TYPE_JSON;
                        break;

                    default:
                        request.ContentType = CONTENT_TYPE_FORM;
                        break;
                }
            }

            request.Method = HttpWebRequestOption.HttpMethod == HttpMethod.GET ? 
                HTTP_METHOD_GET : HTTP_METHOD_POST;

            if (this.HttpWebRequestOption.HeadContent != null)
            {
                foreach (var item in HttpWebRequestOption.HeadContent.Values)
                {
                    _request.Headers.Add(item);
                }
                if(HttpWebRequestOption.HeadContent.NameValueCollection.Count>0)
                     _request.Headers.Add(HttpWebRequestOption
                                               .HeadContent.NameValueCollection);
            }

            _parameter = parameter;

            _request = request;
        }
        
        HttpWebRequest _request = null;

        IHttpParamter _parameter =null;

        byte[] GetRequestStream()
        {
            byte[] bytes = null;
            if (this._parameter != null)
            {
                string text = null;
                if (this.HttpWebRequestOption.ContentType == HttpContentType.JSON)
                {
                    text = this._parameter.GetPostParameter();
                }
                else
                {
                    text = this._parameter.GetParameter();
                }
                bytes = this.HttpWebRequestOption.Encoding.GetBytes(text);
            }
            return bytes;
        }

        public string Request()
        {
            if (this.HttpWebRequestOption.HttpMethod == HttpMethod.POST)
            {
                byte[] bytes = this.GetRequestStream();
                if (bytes != null)
                {
                    this._request.ContentLength = bytes.Length;
                    Stream stream = this._request.GetRequestStream();
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
            }
            WebResponse response = this._request.GetResponse();
            if (response != null)
            {
                StreamReader sr = new StreamReader(response.GetResponseStream(), this.HttpWebRequestOption.Encoding);
                return sr.ReadToEnd().Trim();
            }
            return null;
        }
    }
}
