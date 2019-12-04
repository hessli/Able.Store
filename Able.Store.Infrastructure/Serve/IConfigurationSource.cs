using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Able.Store.Infrastructure.Serve
{
    public interface IConfigurationSource
    {
       IEnumerable<T> Load<T>() where T :class, IConnectOptions;
    }
}
