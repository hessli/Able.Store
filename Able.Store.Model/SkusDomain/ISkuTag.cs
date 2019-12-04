namespace Able.Store.Model.SkusDomain
{
    public interface ISkuTag
    {
        int SkuId { get; set; }
        string TagName { get;  }
        int TagValue { get; }
    }
}
