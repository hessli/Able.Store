using System;
namespace Able.Store.InfrsturctureProvider.Domain.SaleOrders
{
    public class PlaceOrder
    {
        public PlaceOrder()
        { }
        public PlaceOrder(string providerName, string requestContent)
        {
            this.ProviderName = providerName;
            this.PlaceOrderRequest = new PlaceOrderRequest
            {

                Conent = requestContent,
                Time = DateTime.Now
            };
        }
        internal void SetResult(string content,bool isSuccess,string reason)
        {
            this.PlaceOrderResponse = new PlaceOrderResponse
            {

                Content = content,
                CreateTime = DateTime.Now,
                Reason = reason,
                IsSuccess = isSuccess
            };
        }
        public string ProviderName { get; set; }
        public PlaceOrderRequest PlaceOrderRequest { get; set; }
        public PlaceOrderResponse PlaceOrderResponse { get; set; }
        public bool IsSuccess
        {
            get
            {
                return (PlaceOrderResponse != null && PlaceOrderResponse.IsSuccess);
            }
        }
    }
}
