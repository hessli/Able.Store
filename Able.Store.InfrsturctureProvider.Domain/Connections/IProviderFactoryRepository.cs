using Able.Store.Infrastructure.Domain;

namespace Able.Store.InfrsturctureProvider.Domain.Connections
{
    public interface IProviderFactoryRepository :IRepository<ProviderFactory>
    {
        ProviderFactory GetKdBridProviderConnection( );
    }
}
