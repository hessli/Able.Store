using Able.Store.Infrastructure.Queue.Rabbit;

namespace Able.Store.Administrator.IService.Skus
{
    public interface ISkuService
    {
        void CreateOrderChangeCannotQty(RabbitRequest request);
    }
}
