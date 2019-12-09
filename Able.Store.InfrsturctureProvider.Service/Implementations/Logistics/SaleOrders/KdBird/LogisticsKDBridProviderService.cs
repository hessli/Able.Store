using Able.Store.Infrastructure.Http;
using Able.Store.InfrsturctureProvider.Domain.Connections;
using Able.Store.InfrsturctureProvider.Domain.SaleOrders;
using Able.Store.InfrsturctureProvider.Service.Logistics.SaleOrders;
using System;

namespace Able.Store.InfrsturctureProvider.Service.Implementations.Logistics.SaleOrders.KdBird
{
    public class LogisticsKdBridProviderService : ILogisticsProviderService
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
        public LogisticsKdBridProviderService(IProviderFactoryRepository providerRepository,
            ISaleOrderRepository saleOrderRepository)
        {
            _connectionRepository = providerRepository;
            _saleOrderRepository = saleOrderRepository;
            _providerFactory = _connectionRepository.GetKdBridProviderConnection();
            _connect = _providerFactory.GetConnections<KdBridConnect>(KDBRID);
        }

        public string PlaceOrder(IPlaceOrderRequest placeOrderRequest)
        {
            var kdbirdPlaceOrder = placeOrderRequest as KdBirdPlaceOrderRequest;

           var resultView=default(KdBirdPlaceOrderResult);

            SaleOrder sale = new SaleOrder(placeOrderRequest.OrderId,
                placeOrderRequest.OrderCode,
                KDBRID, kdbirdPlaceOrder.SerializeationData);

            try
            {
                var option = new HttpWebRequestOption(_connect.Host + PLACEORDER_METHOD);

                var resultStr = kdbirdPlaceOrder.Request(option, AbstractKDBirdRequest.PLACEORDER_REQUESTTYPE);

                 resultView = kdbirdPlaceOrder.GetResult<KdBirdPlaceOrderResult>(resultStr);

                sale.SetPlaceOrderResult(resultStr, resultView.Success, resultView.Reason);

                _saleOrderRepository.Add(sale);

                _saleOrderRepository.Commit();

                return resultView.GetLogisticCode();
            }
            catch (Exception ex)
            {
                 if(resultView!=default(KdBirdPlaceOrderResult))
                    return resultView.GetLogisticCode();

                return string.Empty;
            }
        }
    }
}
