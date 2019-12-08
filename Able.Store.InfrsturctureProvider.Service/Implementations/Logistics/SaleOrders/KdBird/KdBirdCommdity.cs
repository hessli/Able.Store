using Able.Store.InfrsturctureProvider.Service.Logistics.SaleOrders;

namespace Able.Store.InfrsturctureProvider.Service.Implementations.Logistics.SaleOrders.KdBird
{
    public class KdBirdCommdity : ICommdity
    {
        public string GoodsName { get; set; }
        public int Goodsquantity { get; set; }
        public double GoodsWeight { get; set; }
    }
}
