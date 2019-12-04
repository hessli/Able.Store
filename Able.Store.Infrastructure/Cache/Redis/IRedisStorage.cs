using System.Collections.Generic;

namespace Able.Store.Infrastructure.Cache.Redis
{
    public interface IRedisStorage:ICacheStorage
    {
        double GetHashIncrement(string key, string filed,
         double seed = 1,
         int dataBaseIndex = 0);

        IList<T> HashValues<T>(string key, int dataBaseIndex = 0) where T : class;
        string GetStr(string key, int dataBaseIndex = 0);

        string HashGetPrimitive(string key, string filed, int dataBaseIndex = 0);

        T HashSetSan<T>(string key, string filed, int dataBaseIndex = 0) where T : new();

        bool SetStrPrimitive(string key, string value,
          UpdateStrategy updateStrategy = UpdateStrategy.忽略, int dataBaseIndex = 0);

        bool SetStr<V>(string key, V value, UpdateStrategy updateStrategy = UpdateStrategy.忽略,
            int dataBaseIndex = 0) where V : class;

        void HashSet<V>(string key, IList<KeyValuePair<string, V>> valuePairs, int dataBaseIndex = 0);


        bool HashSet<V>(string key, string filed, V value, UpdateStrategy updateStrategy = UpdateStrategy.忽略, int dataBaseIndex = 0) where V : class;

    
       
    }
}
