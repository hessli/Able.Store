using Able.Store.Infrasturcture.Service.Interface.logistics;

namespace Able.Store.Infrasturcture.Service.Interface.logistics
{
    public interface ILogisticsProviderService
    {
        /// <summary>
        /// 供应名称
        /// </summary>
        string ProviderName { get; }
         /// <summary>
         /// 下单
         /// </summary>
        void PlaceOrder(IPlaceOrderRequest placeOrderRequest);
        /// <summary>
        /// 物流跟踪
        /// </summary>
        void Track(ITrackRequest trackRequest);
    }
}
