using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Able.Store.IService.Orders
{
   public interface IOrderCacheService:IBaseCacheService
    {
        string GetGenerateNo();
    }
}
