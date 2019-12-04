using System;
using System.ComponentModel;

namespace Able.Store.Infrastructure.Domain
{

    [Description("实体接口")]
   public interface IEntityBase<T>
    {
         DateTime? CreateTime { get; set; }        
    }
}
