using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Able.Store.Infrasturcture.Service.Domain.logistics.KdBird.PlaceOrders
{
  public  class KdBirdOrder
    {
        public string OrderCode { get; set; }

        public string ShipperCode { get; set; }

        public string LogisticCode { get; set; }

        public string MarkDestination { get; set; }

        public string OriginCode { get; set; }

        public string OriginName { get; set; }

        public string DestinatioCode { get; set; }

        public string DestinatioName { get; set; }

        public string SortingCode { get; set; }

        public string PackageCode { get; set; }

        public string PackageName { get; set; }

        public string DestinationAllocationCentre { get; set; }
 

    }
}
