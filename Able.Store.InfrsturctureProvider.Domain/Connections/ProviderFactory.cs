using Able.Store.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Able.Store.InfrsturctureProvider.Domain.Connections
{
    public class ProviderFactory : EntityBase<int>, IAggregateRoot
    {
        public ProviderFactory()
        {
            KDBridConnections = new List<KdBridConnect>();
        }

        public override int Id { get; set; }
        public ProviderType Provider { get; set; }
        public string Remark { get; set; }
        public bool RecordState { get; set; }
        public virtual ICollection<KdBridConnect> KDBridConnections { get; set; }
        public override DateTime? CreateTime { get; set; }

        public T GetConnections<T>(string type) where T : class, ILogiticsConnections
        {
            if (type.Equals("Brid"))
            {
                return KDBridConnections.FirstOrDefault() as T;
            }
            return default(T);
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}