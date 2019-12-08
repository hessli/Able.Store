namespace Able.Store.InfrsturctureProvider.Domain.Connections
{
    public class KdBridConnect: ILogiticsConnections
    {
        public string ProviderName { get; set; }

        public string AppKey { get; set; }
        public string EBusinessId { get; set; }
        public string Host { get; set; }
    }
}
