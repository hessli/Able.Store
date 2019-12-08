using Able.Store.Infrastructure.Querying;
using AutoMapper;
using System.Collections.Generic;

namespace Able.Store.IService
{
    public class SearchPageRequest
    {
        public SearchPageRequest()
        {
            order = new List<SearchPageOrderRequest>();
        }
        public int page_index { get; set; }
        public int page_size { get; set; }
        public IList<SearchPageOrderRequest> order { get; set; }
        public IList<OrderByClause> GetOrderParamter()
        {
            Mapper mapper = new Mapper(AutoMapperBootStrapper.Configuration);

            var results = mapper.Map<IList<OrderByClause>>(order);

            return results;

        }
    }
}
