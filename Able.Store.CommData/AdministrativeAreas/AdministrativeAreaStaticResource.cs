namespace Able.Store.CommData.AdministrativeAreas
{
    public class AdministrativeAreaStaticResource
    {
        public static readonly int DBINDEX = (int)RedisDbZone.Comm;
        public static readonly string PREFIX = "administrative_";
        public static readonly string PROVINCE = string.Concat(PREFIX, "province");
        public static readonly string CITY = string.Concat(PREFIX, "city");
        public static readonly string AREA = string.Concat(PREFIX, "area");
    }
}
