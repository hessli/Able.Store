using Able.Store.Infrastructure.UniOfWork;
using Able.Store.Model.OrdersDomain;
using Able.Store.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Able.Store.Repository.Orders
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
       
    }
}
