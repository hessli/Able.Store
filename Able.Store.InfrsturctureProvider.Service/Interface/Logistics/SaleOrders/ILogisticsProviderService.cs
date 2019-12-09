namespace Able.Store.InfrsturctureProvider.Service.Logistics.SaleOrders
{
    public interface ILogisticsProviderService
    {
        string PlaceOrder(IPlaceOrderRequest placeOrderRequest);
    }
}
