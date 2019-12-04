using Able.Store.Administrator.IService.Skus;
using Able.Store.Adminstrator.Model.SkusDomain;
using Able.Store.Infrastructure.Cache.Redis;
using Able.Store.Infrastructure.Queue.Rabbit.Notify;
using System;

namespace Able.Store.Administrator.CacheService
{
    public class SkuCacheService: ISkuCacheService
    {
        private ISkuRepository _skuRepository;
        private Lazy<CacheController> _controller;
        public SkuCacheService(ISkuRepository skuRepository)
        {
            _skuRepository = skuRepository;

            _controller = new Lazy<CacheController>();
        }
        public void NotifyChangeQtyNumber(string requestCorrelationId, bool isSuccess,
            string message, int moduleId,int errorCode=0)
        {
            var args = new RabbitResponseResult {
                ErrorCode=errorCode,
                IsSuccess=isSuccess,
                Message=message
            };
           _controller.Value.SetEntity(RabbitResponseResult.GetCorrelationId(requestCorrelationId),
               args,dataBaseIndex:  moduleId);
        }

        public bool ChangeQtyNumberExsist(string requestCorrelationId, int moduleId)
        {
            var correlationId=   RabbitResponseResult.GetCorrelationId(requestCorrelationId);

             return   _controller.Value.Exsist(correlationId, moduleId);
        }
    }
}
