namespace Able.Store.InfrsturctureProvider.Service.Logistics.SaleOrders
{
    public interface ICommdity
    {
        string GoodsName { get; set; }

        int Goodsquantity { get; set; }

        double GoodsWeight { get; set; }
    }
}
