using System.Collections.Generic;
using System.Collections.Specialized;

namespace Able.Store.Infrastructure.Http
{
    public  class HttpWebHeaderContent
    {
        public HttpWebHeaderContent()
        {
            this.NameValueCollection = new NameValueCollection();

            this.Values = new HashSet<string>();
        }
       internal NameValueCollection NameValueCollection { get; private set; }

       internal HashSet<string> Values { get; private set; }

        public void Add(string value)
        {
            if (!string.IsNullOrWhiteSpace(value) &&
               !Values.Contains(value))
            {
                this.Values.Add(value);
            }
        }

        public void Add(string name,string value)
        {
            this.NameValueCollection.Add(name, value);
        }
    }
}
