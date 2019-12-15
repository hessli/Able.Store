using Able.Store.Infrastructure.ConfigCenter;
using Able.Store.Infrastructure.Jobs;
using Able.Store.Infrastructure.Queue.Rabbit;
using System.Linq;

namespace Able.Store.Infrastructure.Queue.RabbitTempContainer
{
    public class RabbitConnectionJob : IJob
    {
        public void Excute()
        {
            IConfigurationSource source = new RabbitConnectFromDataBase();
            var data = source.Load();
            var length= data.Count();

            if (length > 0)
            {
                foreach (var item in data)
                {
                    ConnectionFactoryPool.Instance.
                        Add(item, new RabbitConnectionFactory((RabbitConnectOptions)item));
                }
            }
        }
    }
}
