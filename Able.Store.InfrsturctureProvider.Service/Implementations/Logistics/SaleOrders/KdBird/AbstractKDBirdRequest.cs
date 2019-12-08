using Able.Store.Infrastructure;
using Able.Store.Infrastructure.Crypt;
using Able.Store.Infrastructure.Http;
using System;
using System.Text;
using System.Web;
namespace Able.Store.InfrsturctureProvider.Service.Implementations.Logistics.SaleOrders.KdBird
{
    public abstract class AbstractKDBirdRequest : ISerialization
    {
        private string _serializeationData;


        internal string EBusinessId { get; set; }
        internal string AppKey { get; set; }

        internal static readonly string DataType = "JSON";
        /// <summary>
        /// 下单指令值
        /// </summary>
        internal static readonly  int PLACEORDER_REQUESTTYPE = 1007;

        [Newtonsoft.Json.JsonIgnore]
        internal string SerializeationData
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._serializeationData))
                {
                    _serializeationData = this.Serialization();
                }
                return _serializeationData;
            }
        }
        public string Serialization()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
        public virtual string GetPostReqeustData()
        {
            return HttpUtility.UrlEncode(SerializeationData, Encoding.UTF8);
        }

        internal virtual string Request(HttpWebRequestOption option, int requestType)
        {
            var paramters = CreateHttpRequest(requestType);

            HttpWebRequestUtility httpWebRequestUtility =
                new HttpWebRequestUtility(option, paramters);

            var result = httpWebRequestUtility.Request();

            return result;
        }

        protected KdBirdHttpRequestParamter CreateHttpRequest(int requestType)
        {
            KdBirdHttpRequestParamter kdBirdHttpRequestParamter = new KdBirdHttpRequestParamter();

            if (!string.IsNullOrWhiteSpace(this.SerializeationData))
            {
                kdBirdHttpRequestParamter.AddParameter("RequestData", this.SerializeationData);
            }
            kdBirdHttpRequestParamter.AddParameter("EBusinessID", this.EBusinessId);

            kdBirdHttpRequestParamter.AddParameter("RequestType", requestType.ToString());

            kdBirdHttpRequestParamter.AddParameter("DataSign", this.GetDataSign());

            kdBirdHttpRequestParamter.AddParameter("DataType", DataType);

            return kdBirdHttpRequestParamter;
        }
        /// <summary>
        /// 获取签名
        /// </summary>
        /// <returns></returns>
        internal string GetDataSign()
        {
            var somme = CryptHelper.HashCrypt(HashCryptType.MD5,
                SerializeationData + AppKey, Encoding.UTF8);

            string dataSign = "";
            foreach (byte a in somme)
            {
                if (a < 16)
                    dataSign += "0" + a.ToString("X");
                else
                    dataSign += a.ToString("X");
            }
            dataSign = Convert.ToBase64String(Encoding.UTF8.GetBytes(dataSign.ToLower()));

            return dataSign;

        }
    }
}
