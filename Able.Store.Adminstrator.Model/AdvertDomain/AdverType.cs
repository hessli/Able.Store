using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Able.Store.Adminstrator.Model.AdvertDomain
{
  public enum AdverType:short
    {
         站内广告=1,
         站外广告=2
    }

    public enum AdvertState : short
    {
         有效=1,
          无效=2
    }
}
