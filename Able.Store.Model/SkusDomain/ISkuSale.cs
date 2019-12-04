
namespace Able.Store.Model.SkusDomain
{
        /// <summary>
  public  interface ISkuSale
    { 


        int SkuId { get; set; }

        /// 价格
        /// </summary>
        decimal Price { get; set; }
        /// <summary>
        ///计算实际销量
        /// </summary>
        void TotalRealQty(int qty);
        /// <summary>
        /// 计算意向销量
        /// </summary>
        /// <param name="qty"></param>
        void TotalIntentionQty(int qty);

        
    }
}
