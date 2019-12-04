
using Able.Store.Model.OrdersDomain;
using System.Collections.Generic;

namespace Able.Store.QueueService.Interface.Orders
{
    public class LockInventoryItemBody
    {
        public int skuId { get; set; }
        public int qty { get; set; }
        public static IList<LockInventoryItemBody> ToBodys(IEnumerable<OrderItem> items)
        {
            IList<LockInventoryItemBody> bodys = new List<LockInventoryItemBody>();
            foreach (var item in items)
            {
                bodys.Add(new LockInventoryItemBody
                {
                    skuId = item.SkuId,
                    qty = item.TotalQty
                });
            }
            return bodys;
        }
    }
}
