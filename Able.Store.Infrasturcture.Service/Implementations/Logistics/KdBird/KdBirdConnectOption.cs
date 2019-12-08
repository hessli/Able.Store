using Able.Store.Infrasturcture.Service.Interface.logistics;

namespace Able.Store.Infrasturcture.Service.Implementations.Logistics.KdBird
{
    public class KdBirdConnectOption: ILogisticsConnectOption
    {
        public KdBirdConnectOption(string appKey,string url,string eBusinessId,string tagName)
        {
            this.AppKey = appKey;
      
            this.EBusinessID = eBusinessId;

            this.Url = url;

            this.TagName = tagName;
        }
        public string AppKey { get; private set; }

        public string EBusinessID { get; private set; }

        public string Url { get; private set; }

        public string TagName { get; private set; }
    }
}
