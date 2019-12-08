using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Able.Store.InfrsturctureProvider.Service.Logistics.SaleOrders
{
   public interface ILogisticsProviderService
    {
        void PlaceOrder(IPlaceOrderRequest placeOrderRequest);
    }
}
