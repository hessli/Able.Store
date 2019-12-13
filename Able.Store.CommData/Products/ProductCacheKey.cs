namespace Able.Store.CommData.Products
{
    public  class ProductCacheKey
    {
        public static readonly string PREFIX = "product.";
        public static readonly string RECOMMENDKEY = string.Concat(PREFIX, "recommend");
        public static readonly string NEWKEY = string.Concat(PREFIX, "news");
        public static readonly int DBINDEX = (int)RedisDbZone.Pms;
    }
}
