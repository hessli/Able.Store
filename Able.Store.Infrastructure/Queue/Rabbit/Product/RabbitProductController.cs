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

            if (!request.AllowDuplicatePublishing && _cacheStorage == null)
            {
                throw new Exception("不允许重复消费的队列需缓存对象");
            }
            if (!request.AllowDuplicatePublishing &&
                _cacheStorage.KeyExists(request.CacheDbIndex, correlationId))
            {
                return;
            }
            if (!request.AllowDuplicatePublishing)
            {
               var cacheModel= new Cache.Model.CacheUnitModel
                {
                    DataBaseIndex = request.CacheDbIndex,
                    Expire = TimeSpan.FromDays(1),
                };
                _cacheStorage.SetAdd(cacheModel, correlationId, correlationId);
            }

            var pushed = RabbitObjFactory.CreatePushed(request.Header.PublishMethod);

            pushed.Factory = ConnectionFactory;

            pushed.Pushed(request, option, basicAckCall);
        }
    }
}
