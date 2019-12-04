using System.Collections.Generic;
using System.Linq;

namespace Able.Store.IService.ProductCatalogs
{
    public class SearchProductRequest: SearchPageRequest
    {   
         public string keyword { get; set; }
        
    }
}
