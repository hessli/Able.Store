using System.Collections.Generic;

namespace Able.Store.Infrastructure.ConfigCenter
{
    public interface IConfigurationSource
    {
       IEnumerable<T> Load<T>() where T :class, IConnectOptions;
    }
}
