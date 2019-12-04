using System.Collections.Generic;

namespace Able.Store.Administrator.IService.Skus
{
    public class ChangeCannotQtyRequest : ServiceInvalidBase
    {

        public string NotifyKey { get; set; }

        public IList<ChangeCannotQtyItemRequest> items { get; set; }

        public IList<int> Ids { get; private set; }

        private Dictionary<int, ChangeCannotQtyItemRequest> _dic;
        public Dictionary<int, ChangeCannotQtyItemRequest> GetDic()
        {
            if (_dic == null)
            {
                Ids = new List<int>();
                _dic = new Dictionary<int, ChangeCannotQtyItemRequest>();
                foreach (var item in items)
                {

                    if (!_dic.ContainsKey(item.skuId))
                    {
                        Ids.Add(item.skuId);
                        _dic.Add(item.skuId,item);
                    }
                }
            }
            return _dic;
        }
        protected override void Validate()
        {
            if (items == null || items.Count == 0)
                base.AddBrokenRule(new ServiceRule ("","物品明细为空"));
        }
    }
    public class ChangeCannotQtyItemRequest
    {
        public int skuId { get; set; }
        public int qty { get; set; }

    }
}
