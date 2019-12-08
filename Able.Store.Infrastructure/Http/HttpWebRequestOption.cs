using System.Text;

namespace Able.Store.Infrastructure.Http
{
    public class HttpWebRequestOption
    {

        static readonly Encoding DEFAULT_ENCODING = Encoding.UTF8;
        static readonly int DEFAULT_TIMEOUT = 30000;

        static readonly string DEFAULT_USERAGENT = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
        static readonly string DEFAULT_ACCEPT = "*/*";

        public HttpWebRequestOption(string uri)
            : this(uri, null, DEFAULT_ENCODING, HttpMethod.POST, HttpContentType.JSON)
        {
            if (string.IsNullOrEmpty(uri))
                throw new System.ArgumentNullException("uri");
            this.Uri = uri;
        }

        public HttpWebRequestOption(string uri, 
            HttpWebHeaderContent headContent=null,
            HttpMethod method = HttpMethod.POST,
            HttpContentType contentType = HttpContentType.JSON)
            : this(uri, headContent, DEFAULT_ENCODING, method, contentType)
        {
             
        }
        public HttpWebRequestOption(string uri, HttpWebHeaderContent headContent,
            Encoding encoding, HttpMethod method = HttpMethod.POST,
            HttpContentType contentType = HttpContentType.JSON)
        {
            this.Uri = uri;

            this.HttpMethod = method;

            this.HeadContent = headContent;

            this.ContentType = contentType;

            if (encoding == null)
            {
                this.Encoding = DEFAULT_ENCODING;
            }
            else
                this.Encoding = encoding;
        }

        public Encoding Encoding { get; private set; }

        public string Uri { get; private set; }

        public HttpMethod HttpMethod { get; private set; }

        public HttpContentType ContentType { get; private set; }

        public HttpWebHeaderContent HeadContent { get; private set; }

        public string Accept { get; set; } = DEFAULT_ACCEPT;

        public int TimeOut { get; set; } = DEFAULT_TIMEOUT;

        public string UserAgent { get; set; } = DEFAULT_USERAGENT;

        internal string CreateGetUri(string paramter="")
        {
            var uri = this.Uri;
            if (uri.IndexOf("?") == -1)
            {
                uri += "?";
            }

            if (!string.IsNullOrWhiteSpace(paramter))
            {
                uri += paramter;
            }
            return uri;
        }

    }
}
