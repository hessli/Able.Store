
namespace Able.Store.InfrsturctureProvider.Service.Interface.Logistics
{
   public interface IResponseResult
    {
        bool Success { get; set; }

        string ResultCode { get; set; }

        string Reason { get; set; }
    }
}
