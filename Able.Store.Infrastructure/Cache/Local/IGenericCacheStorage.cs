namespace Able.Store.Infrastructure.Cache.Local
{
    public  interface IGenericCacheStorage:ICacheStorage
    {
        int GetCount(int dbIndex);
    }
}
