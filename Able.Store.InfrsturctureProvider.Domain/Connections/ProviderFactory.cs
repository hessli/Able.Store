using System.Collections.Generic;
using System.Linq;
namespace Able.Store.InfrsturctureProvider.Domain.Connections
{
    public class ProviderFactory
    {
        public ProviderFactory()
        {
            KDBridConnections = new List<KdBridConnect>();
        }
        public ProviderType Provider { get; set; }
        public string Remark { get; set; } 
        public virtual ICollection<KdBridConnect> KDBridConnections { get; set; }
        public T GetConnections<T>(string type) where T:class,ILogiticsConnections
        {
            if (type.Equals("Brid"))
            {
                return  KDBridConnections.FirstOrDefault() as T;
            }
            return default(T);
        }
    }
}