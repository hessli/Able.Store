using Able.Store.Infrastructure.Queue.Rabbit;

namespace Able.Store.Administrator.IService.Skus
{
    public interface ISkuCacheService
    {

        bool ChangeQtyNumberExsist(string requestCorrelationId);
        void NotifyChangeQtyNumber(RabbitRequestHeader rabbitRequestHeader, bool isSuccess, string message, int errorCode = 0);
    }
}
