using Able.Store.Infrastructure.Domain;
using Able.Store.Infrastructure.Querying;
using System.Collections.Generic;
using System.Linq;

namespace Able.Store.Model.SkusDomain
{
    public interface ISkuRepository : IRepository<Sku>
    {

        PagingResult<Sku> PagingResult(string title,
      IList<OrderParamter> orderParamters,
      int pageIndex,
      int pageSize);
        IList<Sku> GetNewProducts(int size);
        IList<Sku> GetRecommendProducts(int size);

        IList<Sku> GetSkus(IList<int> skuids);
    }
}
