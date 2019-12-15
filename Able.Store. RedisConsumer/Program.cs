using Able.Store.CommData.Orders;
using Able.Store.Infrastructure.Cache;
using Able.Store.Infrastructure.Cache.Redis;
using Able.Store.Infrastructure.Queue.Rabbit;
using Able.Store.RedisConsumer.Business.Orders;
using Able.Store.RedisConsumer.Comm;
using Able.Store.RedisConsumer.Start;
using Newtonsoft.Json;
using System;
using System.Configuration;

namespace Able.Store.RedisConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Boot.Start();
            ICacheStorage storage = new RedisStorage();
            storage.Subscrib(OrderStaticResource.CREATE_ORDER_CHANGEQTY_CHANNELNAME, Handler);

            Console.ReadLine();
        }
        static void Handler(string channelName, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
               var data= JsonConvert.DeserializeObject<RequestView<RabbitRequestHeader>>(value);

                SaleOrderBusiness orderBusiness = new SaleOrderBusiness();
                
                 orderBusiness.CreateOrder(data);
                
            }
        }
    }
}
