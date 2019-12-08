using Able.Store.Infrastructure.Crypt;
using Able.Store.Infrastructure.Http;
using Able.Store.Infrasturcture.Service.Implementations.Logistics.KdBird;
using Able.Store.Infrasturcture.Service.Interface.logistics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Linq;
namespace Able.Store.Infrasturcture.Service.Implementations.Logistics
{
    public class KdBirdProvider : ILogisticsProviderService
    {
        public string ProviderName {

            get {
                return "快递鸟";
            }
        }
        ConnectionFromDataBase _connectionFromDataBase;

        IList<ILogisticsConnectOption> _connections;

        KdBirdConnectOption _connection;
        public KdBirdProvider()
        {
            _connectionFromDataBase = new ConnectionFromDataBase();

            _connections = _connectionFromDataBase.Load();

            if (_connections.Count == 0)
                throw new ArgumentException("未配置链接");

            _connection= (KdBirdConnectOption)_connections.FirstOrDefault();
        }

        public void PlaceOrder(IPlaceOrderRequest placeOrderRequest)
        {
           var  kdBirdHttpRequestParamter = new KdBirdHttpRequestParamter();

           var requestData=JsonConvert.SerializeObject(placeOrderRequest);

           var option =new HttpWebRequestOption(_connection.Url+"/Eorderservice");

           var somme =  CryptHelper.HashCrypt(HashCryptType.MD5, requestData+ _connection.AppKey, Encoding.UTF8);

            string ret = "";

            foreach (byte a in somme)
            {
                if (a < 16)
                    ret += "0" + a.ToString("X");
                else
                    ret += a.ToString("X");
            }
            string dataSign = Convert.ToBase64String(Encoding.UTF8.GetBytes(ret.ToLower()));   //encrypt(requestData, AppKey, "UTF-8");

            kdBirdHttpRequestParamter.AddParameter("RequestData", HttpUtility.UrlEncode(requestData, Encoding.UTF8));

            kdBirdHttpRequestParamter.AddParameter("EBusinessID", _connection.EBusinessID);

            kdBirdHttpRequestParamter.AddParameter("RequestType", "1007");

            kdBirdHttpRequestParamter.AddParameter("DataSign", HttpUtility.UrlEncode(dataSign, Encoding.UTF8));

            kdBirdHttpRequestParamter.AddParameter("DataType","2");

            HttpWebRequestUtility requestUtility = new HttpWebRequestUtility(option,kdBirdHttpRequestParamter);

            var rs= requestUtility.Request();
            
        }
        public void Track(ITrackRequest trackRequest)
        {
            throw new NotImplementedException();
        }
    }
}
