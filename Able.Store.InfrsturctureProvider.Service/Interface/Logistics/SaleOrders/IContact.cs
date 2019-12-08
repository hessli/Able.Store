namespace Able.Store.InfrsturctureProvider.Service.Logistics.SaleOrders
{
    public interface IContact
    {
        string Company { get; set; }
  
        string Name { get; set; }

        string Mobile { get; set; }

        string ProvinceName { get; set; }

        string CityName { get; set; }

        string ExpAreaName { get; set; }
        string Address { get; set; }

    }
}
