using Able.Store.Infrastructure.Queue.RabbitTempContainer;
using System.Threading;
namespace Able.Store.Infrastructure.Queue.Rabbit
{
    public abstract class RabbitBaseController
    {
        internal readonly ConnectionFactoryPool Pool = ConnectionFactoryPool.Instance;
        protected RabbitBaseController()
        {
            if (Pool.IsEmpty)
            {
                for (var i = 0; i < 10; i++)
                {
                    Thread.Sleep(10);
                    if (!Pool.IsEmpty)
                        break;
                }
            }
            RabbitConnectionFactory connection;
            if (!Pool.TryGet(out connection))
            {
                //触发事件记录日志等后续方案不写了后面慢慢补全
            }
            else
            {
                ConnectionFactory = connection;
            }
        }
        protected RabbitConnectionFactory ConnectionFactory
        {
            get; private set;
        }
        public bool IsInitialize
        {
            get
            {
                return Pool.IsEmpty;
            }
        }
    }
}
