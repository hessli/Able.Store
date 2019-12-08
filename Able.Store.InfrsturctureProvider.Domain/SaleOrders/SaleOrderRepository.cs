using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Able.Store.InfrsturctureProvider.Domain.SaleOrders
{
   public interface ISaleOrderRepository
    {
        void Add(SaleOrder entity);
        void Commit();
    }
}
