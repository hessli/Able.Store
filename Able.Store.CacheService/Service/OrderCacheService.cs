using Able.Store.Infrastructure.Cache.Redis;
using Able.Store.IService.Orders;
using Able.Store.Model.OrdersDomain;
using System;
using System.Collections.Generic;

namespace Able.Store.CacheService.Service
{
    public class OrderCacheService : IOrderCacheService
    {

        private IOrderRepository _orderRepository;

        private Lazy<CacheController> _cacheController=new Lazy<CacheController> ();
        public string PREFIX => "order_";

        public string GenerateNoKey =>string.Concat(PREFIX ,"S_No");
        public IList<string> CacheKeys { get; } = new List<string>();

        public OrderCacheService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _orderRepository = orderRepository;

            this.CacheKeys.Add(this.GenerateNoKey);
        }
        public string GetGenerateNo()
        {
           var no= _cacheController.Value.GetHashIncrement(GenerateNoKey,"No");

            return no.ToString();
        }

    }
}
