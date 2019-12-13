using Able.Store.Infrastructure.Cache;
using Able.Store.Infrastructure.Queue.Rabbit;
using System;

namespace Able.Store.Infrastructure.Queue.Product
{
    public class RabbitProductController : RabbitBaseController
    {
        protected ICacheStorage _cacheStorage;

        public RabbitProductController(ICacheStorage cacheStorage) : base()
        {
            _cacheStorage = cacheStorage;
        }
        public void Push(RabbitRequest request,
                 RabbitOption option, Action<ulong, bool> basicAckCall = null)
        {
            var correlationId = request.GetCorrelationId();

            if (!_cacheStorage.KeyExists(request.CacheDbIndex, correlationId))
            {
                var pushed = RabbitObjFactory.CreatePushed(request.Header.PublishMethod);

                pushed.Factory = ConnectionFactory;

                pushed.Pushed(request, option, basicAckCall);
            }
        }
    }
}
