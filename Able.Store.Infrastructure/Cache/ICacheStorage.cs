namespace Able.Store.Infrastructure.Cache
{
    public interface ICacheStorage
    {
        void Remove(string key, int dbIndex=0);
        T GetEntity<K,T>(K key, int dbIndex = 0);
        bool Exists<K>(K key,int dbIndex);
        void SetEntity<K, T>(K key,T value, UpdateStrategy updateStrategy= UpdateStrategy.忽略, int dbIndex = 0);
    }
}
