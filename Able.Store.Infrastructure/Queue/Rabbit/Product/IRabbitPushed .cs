using Able.Store.Infrastructure.Queue.Rabbit.Notify;
using Able.Store.Infrastructure.Queue.Rabbit.RabbitTempContainer;
using System;

namespace Able.Store.Infrastructure.Queue.Rabbit.Product
{
    public interface IRabbitPushed
    {
        RabbitConnectionFactory Factory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="option"></param>
        /// <param name="basicAckCall">手动应答回调函数</param>
        void Pushed(RabbitRequest  request,
            RabbitOption option, Action<ulong, bool> basicAckCall = null);
    }
}
