using Able.Store.Infrastructure.Jobs;
using Able.Store.Infrastructure.Queue.Rabbit;
using Able.Store.Infrastructure.Queue.Rabbit.SourceConfig;
using Able.Store.Infrastructure.Serve;
using System.Linq;

namespace Able.Store.Infrastructure.Queue.Rabbit.RabbitTempContainer
{
    public class RabbitConnectionJob : IJob
    {
        public void Excute()
        {
            IConfigurationSource source = new RabbitConnectConfigurationSource();
            var data = source.Load<RabbitConnectOptions>();
            var length= data.Count();

            if (length > 0)
            {
                foreach (var item in data)
                {
                    ConnectionFactoryPool.Instance.
                        Add(item, new RabbitConnectionFactory(item));
                }
            }
        }
    }
}
