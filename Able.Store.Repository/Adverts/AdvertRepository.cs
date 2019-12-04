using Able.Store.Infrastructure.UniOfWork;
using Able.Store.Model.AdvertDomain;
using Able.Store.Repository.EF;
namespace Able.Store.Repository.Adverts
{
    public class AdvertRepository : BaseRepository<Advert>, 
        IAdvertRepository
    {
        public AdvertRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
