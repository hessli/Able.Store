using Able.Store.Infrastructure.Domain;

namespace Able.Store.InfrsturctureProvider.Domain.Connections
{
    public class KdBridConnect: ValueOjectBase,ILogiticsConnections
    {
        public string ProviderName { get; set; }
        public string AppKey { get; set; }
        public string EBusinessId { get; set; }
        public string Host { get; set; }
        protected override void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}
