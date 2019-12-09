using Able.Store.InfrsturctureProvider.Service.Logistics.SaleOrders;

namespace Able.Store.InfrsturctureProvider.Service.Implementations.Logistics.SaleOrders.KdBird
{
    public class KdBirdLogisticsOrder : ILogisticsOrder
    {
        public string OrderCode { get ; set ; }
        public string ShipperCode { get ; set ; }
        public string LogisticCode { get ; set ; }
        public string MarkDestination { get ; set ; }
        public string OriginCode { get ; set ; }
        public string OriginName { get ; set ; }
        public string DestinatioCode { get ; set ; }
        public string DestinatioName { get ; set ; }
        public string SortingCode { get ; set ; }
        public string PackageCode { get ; set ; }
        public string PackageName { get ; set ; }
        public string DestinationAllocationCentre { get ; set ; }
    }
}
