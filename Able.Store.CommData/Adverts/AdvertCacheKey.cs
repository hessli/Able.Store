namespace Able.Store.CommData.Adverts
{
    public class AdvertCacheKey
    {
        /// <summary>
        /// 前缀
        /// </summary>
        public static readonly string PREFIX = "advert.";
        /// <summary>
        /// banner
        /// </summary>
        public static readonly string BANNERKEY = string.Concat(PREFIX, "index.banner");
        /// <summary>
        /// 库
        /// </summary>
        public static readonly int DBINDEX = (int)RedisDbZone.Comm;
    }
}
