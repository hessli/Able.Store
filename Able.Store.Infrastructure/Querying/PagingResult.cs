using System.Collections.Generic;

namespace Able.Store.Infrastructure.Querying
{
   public class PagingResult<T> 
    {
         public PagingResult(int pageCount, IEnumerable<T> result)
        {
            this.PageCount = pageCount;

            this.Result = result;
        }
         
         public int PageCount { get; private  set; }
         public IEnumerable<T> Result { get; private  set; }
    }

}
