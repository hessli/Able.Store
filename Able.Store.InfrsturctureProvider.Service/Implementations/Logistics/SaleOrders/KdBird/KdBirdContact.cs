using Able.Store.InfrsturctureProvider.Service.Logistics.SaleOrders;

namespace Able.Store.InfrsturctureProvider.Service.Implementations.Logistics.SaleOrders.KdBird
{ 
    public class KdBirdContact : IContact
    {
        public string Company { get;set; }
        public string Name { get;set; }
        public string Mobile { get;set; }
        public string ProvinceName { get;set; }
        public string CityName { get;set; }
        public string ExpAreaName { get;set; }
        public string Address { get;set; }
    }
}
