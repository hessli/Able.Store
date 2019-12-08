using Able.Store.Infrastructure.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Able.Store.Infrasturcture.Service.Implementations.Logistics
{
   public class KdBirdHttpRequestParamter : IHttpParamter
    {
        public KdBirdHttpRequestParamter()
        {
            _dict = new Dictionary<string, string>();
     
        }

        private object _entity = null;
        public void AddParameter<T>(T data) where T : class
        {
            _entity = data;
        }
        Dictionary<string, string> _dict = null;
        
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

        public string GetPostParameter()
        {
            StringBuilder postData = new StringBuilder();

            if (this._entity!=null)
            {
                postData.Append("RequestData=");
                postData.Append(JsonConvert.SerializeObject(_entity));
            }
             
            foreach (var p in _dict)
            {
                if (postData.Length > 0)
                {
                    postData.Append("&");
                }
                postData.Append(p.Key);
                postData.Append("=");
                postData.Append(p.Value);
            }
            return postData.ToString();
        }

        public string GetParameter()
        {
            throw new NotImplementedException();
        }
    }
}
