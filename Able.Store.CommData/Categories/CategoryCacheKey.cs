namespace Able.Store.CommData.Categories
{
    public class CategoryCacheKey
    {
        public static readonly string PREFIX = "category.";
        public static readonly string INDEX = string.Concat(PREFIX, "index");
        public static readonly int DBINDEX = (int)RedisDbZone.Pms;
    }
}
