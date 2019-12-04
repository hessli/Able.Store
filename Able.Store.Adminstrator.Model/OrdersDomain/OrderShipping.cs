using System.Collections.Generic;

namespace Able.Store.Adminstrator.Model.OrdersDomain
{
    public  class OrderShipping
    {
         public int Id { get; set; }
         /// <summary>
         /// 快递单号
         /// </summary>
         public string No { get; set; }
         /// <summary>
         /// 快递公司
         /// </summary>
         public string ShippingName { get; set; }
         /// <summary>
         /// 快递联系电话
         /// </summary>
         public string Tel { get; set; }
         public ICollection<OrderShippingLocus> Locus { get; set; }
         
    }
}
