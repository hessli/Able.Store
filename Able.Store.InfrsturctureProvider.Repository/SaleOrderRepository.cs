using Able.Store.Infrastructure.UniOfWork;
using Able.Store.InfrsturctureProvider.Domain.SaleOrders;

namespace Able.Store.InfrsturctureProvider.Repository
{
    public class SaleOrderRepository : BaseRepository<SaleOrder>, ISaleOrderRepository
    {
        public SaleOrderRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {

        }
    }
}
