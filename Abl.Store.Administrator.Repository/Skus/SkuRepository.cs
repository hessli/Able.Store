using Abl.Store.Administrator.Repository.EF;
using Able.Store.Adminstrator.Model.SkusDomain;
using Able.Store.Infrastructure.UniOfWork;

namespace Abl.Store.Administrator.Repository.Skus
{
    public class SkuRepository : BaseRepository<Sku>, ISkuRepository
    {
        public SkuRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
