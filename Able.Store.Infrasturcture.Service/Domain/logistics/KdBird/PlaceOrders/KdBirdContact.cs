using Able.Store.Infrasturcture.Service.Interface.logistics;

namespace Able.Store.Infrasturcture.Service.Domain.Logistics.KdBird
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
