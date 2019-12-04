using System.Collections.Generic;
using System.Linq;
namespace Able.Store.IService.Shopping
{
    public class BasketRequestView: BaseRequest
    {
        private  IList<int> _skudIds=null;
        public IList<int> skuids
        {
            get
            {
                if (_skudIds == null)
                {
                     _skudIds = pack.Select(x => x.skuid).ToList();
                }
                return _skudIds;
            }
        }
        public IList<BasketItemRequestView> pack { get; set; }
        public void CheckRequest()
        {
            if (userid == default(int))
            {
                throw new System.Exception("登录异常");
            }
            if (pack == null || pack.Count == 0)
            {
                throw new System.Exception("请选择商品");
            }
        }


    }


}
