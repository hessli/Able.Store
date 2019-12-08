namespace Able.Store.Infrasturcture.Service.Interface.logistics
{
    public interface ILogisticsConnectOption 
    {
        string AppKey { get;  }

        string TagName { get; }
        string EBusinessID { get; }

        string Url { get; }
    }
}
