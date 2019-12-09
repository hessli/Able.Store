using Able.Store.InfrsturctureProvider.Service.Interface.Logistics;

namespace Able.Store.InfrsturctureProvider.Service.Logistics.SaleOrders
{ 
    public interface ILogisticsOrder
    {
        string OrderCode { get; set; }

        string ShipperCode { get; set; }

        string LogisticCode { get; set; }

        string MarkDestination { get; set; }

        string OriginCode { get; set; }

        string OriginName { get; set; }

        string DestinatioCode { get; set; }

        string DestinatioName { get; set; }

        string SortingCode { get; set; }

        string PackageCode { get; set; }

        string PackageName { get; set; }

        string DestinationAllocationCentre { get; set; }
    }
}
