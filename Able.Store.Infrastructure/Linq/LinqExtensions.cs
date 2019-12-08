using Able.Store.Infrastructure.Querying;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
    public static class LinqExtensions
    {


        public static PagingResult<T> Pagination<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            int count = 0;
            IEnumerable<T> pageSet = new List<T>();

            if (query != null)
            {
                pageSet = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                count = query.Count();
            }
            return new PagingResult<T>(count,pageSet);
        }

        public static PagingResult<T> Pagination<T>(this IList<T> query, int pageIndex, int pageSize)
        {
            int count = 0;
            IEnumerable<T> pageSet = new List<T>();

            if (query != null)
            {
                pageSet = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                count = query.Count();
            }
            return new PagingResult<T>(count, pageSet);
        }
    }
}
