using Able.Store.Infrastructure.UniOfWork;
using Able.Store.InfrsturctureProvider.Domain.Connections;

namespace Able.Store.InfrsturctureProvider.Repository
{
    public class ProviderFactoryRepository : BaseRepository<ProviderFactory>, IProviderFactoryRepository
    {
        public ProviderFactoryRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ProviderFactory GetKdBridProviderConnection()
        {
            var entity = base.GetFirstOrDefault(x => x.Provider == ProviderType.物流);

            return entity;
        }
    }
}
