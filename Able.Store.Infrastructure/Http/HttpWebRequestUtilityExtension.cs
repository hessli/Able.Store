namespace Able.Store.Infrastructure.Http
{
    public static class HttpWebRequestUtilityExtension
    {
        public static T Request<T>(this HttpWebRequestUtility requestUtility)
        {
            var result = requestUtility.Request();
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);
            return data;
        }
    }
}
