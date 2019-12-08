using Able.Store.Infrasturcture.Service.Interface.logistics;

namespace Able.Store.Infrasturcture.Service.Implementations.logistics
{
    public class KdBirdCommdity : ICommdity
    {
        public string GoodsName { get; set; }
        public int Goodsquantity { get; set; }
        public double GoodsWeight { get; set; }
    }
}
