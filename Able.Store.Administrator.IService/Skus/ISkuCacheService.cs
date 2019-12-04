namespace Able.Store.Administrator.IService.Skus
{
    public interface ISkuCacheService
    {

       bool ChangeQtyNumberExsist(string requestCorrelationId, int moduleId);
     void NotifyChangeQtyNumber(string requestCorrelationId, bool isSuccess, string message, int moduleId, int errorCode = 0);
    }
}
