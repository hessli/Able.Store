using AutoMapper;
using System.Collections.Generic;
using System.Linq;

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
        public IList<OrderParamter> GetOrderParamter()
        {
            Mapper mapper = new Mapper(AutoMapperBootStrapper.Configuration);

            var results = mapper.Map<IList<OrderParamter>>(order);

            return results;

        }
    }
}
