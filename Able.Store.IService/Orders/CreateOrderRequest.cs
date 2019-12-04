
using System.Collections.Generic;

namespace Able.Store.IService.Orders
{
    public class CreateOrderRequest: ServiceInvalidBase
    {
      
        public  int[] skuId { get; set; }
        public string message { get; set; }
        public int receiverid { get; set; }
        public int userid { get; set; }
        protected override void Validate()
        {
            if (skuId == null || skuId.Length == 0)
                base.AddBrokenRule(new ServiceRule ("Skuids","未指定商品"));

            if (receiverid == default(int))
                base.AddBrokenRule(new ServiceRule("receiverid", "未指定收货信息"));

            if (userid == default(int))
                base.AddBrokenRule(new ServiceRule ("userid", "非法用户"));
        }
    }
   
}
