using Able.Store.Infrastructure.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Able.Store.InfrsturctureProvider.Service.Implementations.Logistics.SaleOrders.KdBird
{
   public class KdBirdHttpRequestParamter : IHttpParamter
    {
        public KdBirdHttpRequestParamter()
        {
            _dict = new Dictionary<string, string>();
        }
        public void AddParameter<T>(T data) where T : class
        {
            throw new NotImplementedException();
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
