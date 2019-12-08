using Able.Store.Infrasturcture.Service.Interface.logistics;

namespace Able.Store.Infrasturcture.Service.Domain.Logistics.KdBird
{
    public class KdBirdCommdity : ICommdity
    {
        public string GoodsName { get; set; }
        public int Goodsquantity { get; set; }
        public double GoodsWeight { get; set; }
    }
}
