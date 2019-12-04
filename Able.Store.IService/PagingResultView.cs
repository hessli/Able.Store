using System.Collections.Generic;

namespace Able.Store.IService
{
    public class PagingResultView<T>
    {
        public int count { get; private set; }

        public IEnumerable<T> data { get; set; }
        public PagingResultView(int pageCount,IEnumerable<T> results)
        {
            data = results;
            count = pageCount;
        }
    }
}
