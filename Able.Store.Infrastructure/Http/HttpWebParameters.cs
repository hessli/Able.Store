using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Able.Store.Infrastructure.Http
{
    public class HttpWebParameters: IHttpParamter
    {
        public HttpWebParameters()
        {
            _dict = new Dictionary<string, string>();
            _obj= new HashSet<object>();
        }
        Dictionary<string, string> _dict = null;

        private HashSet<object> _obj = null;
        
        public void AddParameter(string key, string val)
        {
            if (this._dict.ContainsKey(key))
            {
                throw new ArgumentException("key重复");
            }
            if (string.IsNullOrEmpty(val))
            {
                val = "";
            }
            this._dict.Add(key, val);
        }

        public void AddParameter<T>(T data) where T : class
        {
            if(!_obj.Contains(data))
            _obj.Add(data);
        }

        public string GetParameter()
        {
            int len = this._dict.Count;
            if (len == 0)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (var item in this._dict)
            {
                if (i < len)
                {
                    sb.AppendFormat("{0}={1}&", item.Key, item.Value);
                    i++;
                }
                else
                {
                    sb.AppendFormat("{0}={1}", item.Key, item.Value);
                }
            }
            return sb.ToString();
        }

        public string GetPostParameter()
        {

            JObject data = null;
            if (this._obj.Count > 0)
            {
                data = JObject.FromObject(data);
            }
            if (data == null)
            {
                if (this._dict.Count > 0)
                {
                    data = JObject.FromObject(_dict);
                }
            }
            else {
                if (this._dict.Count > 0)
                {
                    foreach (var item in _dict)
                    {
                        if (!data.ContainsKey(item.Key))
                        {
                            data.Add(item.Key, item.Value);
                        }
                    }
                }
            }
            return data.Root.ToString();
        }
    }
}
