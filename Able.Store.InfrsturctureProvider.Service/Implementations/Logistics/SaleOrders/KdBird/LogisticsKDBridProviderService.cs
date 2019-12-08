using Able.Store.Infrastructure.Http;
using Able.Store.InfrsturctureProvider.Domain.Connections;
using Able.Store.InfrsturctureProvider.Domain.SaleOrders;
using Able.Store.InfrsturctureProvider.Service.Logistics.SaleOrders;

namespace Able.Store.InfrsturctureProvider.Service.Implementations.Logistics.SaleOrders.KdBird
{
    public class LogisticsKDBridProviderService : ILogisticsProviderService
    {
        private IProviderFactoryRepository _connectionRepository;
        private ProviderFactory _providerFactory;
        private KdBridConnect _connect;
        private ISaleOrderRepository _saleOrderRepository;
        private static readonly string KDBRID = "Brid";

        /// <summary>
        /// 下单方法
        /// </summary>
        private static readonly string PLACEORDER_METHOD = "Eorderservice";
        public LogisticsKDBridProviderService(IProviderFactoryRepository providerRepository,
            ISaleOrderRepository saleOrderRepository)
        {
            _connectionRepository = providerRepository;
            _saleOrderRepository = saleOrderRepository;
            _providerFactory = _connectionRepository.GetKdBridProviderConnection();

            _connect = _providerFactory.GetConnections<KdBridConnect>(KDBRID);
        }

        public void PlaceOrder(IPlaceOrderRequest placeOrderRequest)
        {
            var kdbirdPlaceOrder = placeOrderRequest as KdBirdPlaceOrderRequest;

            SaleOrder sale = new SaleOrder(placeOrderRequest.OrderId,
                placeOrderRequest.OrderCode,
                KDBRID, kdbirdPlaceOrder.SerializeationData);

            var option = new HttpWebRequestOption(_connect.Host + PLACEORDER_METHOD);

            var resultStr = kdbirdPlaceOrder.Request(option, AbstractKDBirdRequest.PLACEORDER_REQUESTTYPE);

            _saleOrderRepository.Add(sale);

            _saleOrderRepository.Commit();
        }
    }
}
