using System.Collections.Generic;

namespace Able.Store.Infrastructure.ConfigCenter
{
    public interface IConfigurationSource
    {
        IEnumerable<IConnectOptions> Load();
    }
}
