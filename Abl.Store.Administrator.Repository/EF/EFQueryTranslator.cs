using Able.Store.Infrastructure.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abl.Store.Administrator.Repository.EF
{
   public class EFQueryTranslator
    {
        public void BuildQueryFrom(Query query)
        {
            if (query != null)
            {
                  //(x=>x.a.any(y=>y.id==3 || bool.Equals(5) && y.c="f" H.any(global=>global.ANY).equs950)))
            }
        }


    }
}
