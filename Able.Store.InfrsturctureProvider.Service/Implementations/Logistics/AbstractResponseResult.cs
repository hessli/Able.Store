using Able.Store.InfrsturctureProvider.Service.Interface.Logistics;

namespace Able.Store.InfrsturctureProvider.Service.Implementations.Logistics
{
    public class AbstractResponseResult : IResponseResult
    {
        public bool Success { get; set; }
        public string ResultCode { get; set; }
        public string Reason { get; set; }
    }
}
