
using Able.Store.Model.Core;
using System.Collections.Generic;

namespace Able.Store.IService.Orders
{
   public class MerchantView
    {
         public int payType { get; set; }
         public string title { get; set; }

        public static IList<MerchantView> ToPaywayViews()
        {
            IList<MerchantView> views = new List<MerchantView>();
             
            views.Add(new MerchantView {
                 payType= (int)Merchant.微信,
                  title=Merchant.微信.ToString()
            });
            views.Add(new MerchantView
            {
                payType = (int)Merchant.支付宝,
                title = Merchant.支付宝.ToString()
            });
            views.Add(new MerchantView
            {
                payType = (int)Merchant.钱包,
                title = Merchant.钱包.ToString()
            });
            return views;
        }
    }
}
