using Able.Store.Infrastructure.Cache.Redis;
using Able.Store.Infrastructure.Queue.Rabbit;
using Able.Store.Infrastructure.Queue.Rabbit.Notify;
using System;

namespace Able.Store.Infrastructure.Queue.Product
{
    public class RabbitProductController: RabbitBaseController
    {
        private CacheController _controller;
        public RabbitProductController():base()
        {
           
        }
        public RabbitResponseResult Push(RabbitRequest request,
                 RabbitOption option, Action<ulong, bool> basicAckCall = null)
        {
            //判断是否推送,如果有相同的键则不推送防止重复消费
            _controller = new CacheController();

            var correlationId= request.GetCorrelationId();
            var response = RabbitResponse.CreateResponse(correlationId, request.Header.IsGetNotify,
              request.Header.WaitTime, request.Header.IsSynch, request.Header.ModuleId);

            if (_controller.Exsist(correlationId, request.Header.ModuleId))
            {
                return response.CreateResult();
            }
            else
            {
                _controller.SetEntity(correlationId,
                    request.Header,
                    dataBaseIndex:request.Header.ModuleId);
            }
            var pushed = RabbitObjFactory.CreatePushed(request.Header.PublishMethod);

                pushed.Factory = ConnectionFactory;
           
                pushed.Pushed(request,option, basicAckCall);
            
            var args = response.GetResult(request.SynchCallback, _controller);

            return args;
        }
    }
}
