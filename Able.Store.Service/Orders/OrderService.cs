using Able.Store.Infrastructure.Domain;
using Able.Store.IService;
using Able.Store.IService.Orders;
using Able.Store.Model.BasketsDomain;
using Able.Store.Model.OrdersDomain;
using Able.Store.Model.Users;
using Able.Store.Service.IService;
using System.Collections.Generic;
using System.Threading;

namespace Able.Store.Service.Orders
{
    public class OrderService:BaseService, IOrderService
    {
        IOrderRepository _orderRepository;
        IUserRepository _userRepository;
        IBasketRepository _baseketRepository;
        IOrderCacheService _orderCacheService;
        public OrderService(IOrderRepository orderRepository,IUserRepository userRepository,
            IBasketRepository basketRepository,
            IOrderCacheService orderCacheService)
        {
            _orderCacheService = orderCacheService;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _baseketRepository = basketRepository;
        }
        public ResponseView<int> Create(CreateOrderRequest request)
        {
            try
            {
                request.ThrowExceptionIfInvalid();
            }
            catch (ServiceInvalidException e) {
                return base.OutPutResponseView(default(int),false, e.Message);
            }
            var user = _userRepository.GetPointUserWithReceiver(request.userid,request.receiverid);

            var basket= _baseketRepository.GetBasket(request.userid, request.skuId);

            var stuffix= _orderCacheService.GetGenerateNo();

            var orderGeneral = new OrderGenerateNo(stuffix);

            try
            {
                Order order = new Order(user, basket, orderGeneral);

                _orderRepository.Add(order);

                _orderRepository.Commit();

                var isSuccess=order.SystemLocker();

                if (isSuccess)
                {
                    Thread.Sleep(100);
                    return base.OutPutBrokenResponseView(order.Id);
                }
               return  base.OutPutResponseView(0,false,"系统繁忙正在处理中....");
            }
            catch (ValueObjectIsInvalidException ex)
            {
                base.AddRuleBroke(new ServiceRule ("",ex.Message));

                return base.OutPutBrokenResponseView<int>(default(int));
            }
        }
        public ResponseView<OrderView> GetOrderDetail(int userId,int orderId)
        {
           var order=_orderRepository
                .GetFirstOrDefault(x =>x.UserId==userId && x.Id == orderId);
         
            var data= OrderView.ToView(order);

            return base.OutPutResponseView(data);
        }
        public ResponseView<IList<MerchantView>> GetPayWay()
        {
            var data=base.OutPutResponseView(MerchantView.ToPaywayViews());

            return data;
        }
    }
}
