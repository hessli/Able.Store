using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Able.Store.IService.Orders
{
   public class OrderRequestView
    {
         public string name { get; set; }

         public string tel { get; set; }

         public string province { get; set; }

         public string city { get; set; }

         public string county { get; set; }

         public string detailed { get; set; }

         public string postal { get; set; }
    }
}
