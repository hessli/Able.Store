using Able.Store.Infrastructure.Cache.Redis.RedisTempContainer;
using Able.Store.Infrastructure.Utils;
using StackExchange.Redis;
using System.Collections.Generic;

namespace Able.Store.Infrastructure.Cache.Redis
{
    public class RedisStorage : IRedisStorage
    {
        RedisConnectionFactory _factory;
        public RedisStorage(RedisConnectionFactory factory)
        {
            _factory = factory;
        }
  
        public string GetStr(string key, int dataBaseIndex = 0)
        {
            var database = _factory.GetDatabase(dataBaseIndex);

            var redisValue = database.StringGet(key);

            return redisValue;

        }
        public string HashGetPrimitive(string key, string filed, int dataBaseIndex = 0)
        {
            var database = _factory.GetDatabase(dataBaseIndex);

            var redisValue = database.HashGet(key, filed);

            return redisValue;
        }

        public IList<T> HashValues<T>(string key, int dataBaseIndex = 0) where T : class
        {
            var database = _factory.GetDatabase(dataBaseIndex);

            var values = database.HashValues(key);

            IList<T> results = new List<T>();

            foreach (var item in values)
            {
                results.Add(JsonPase.Deserialize<T>(item));
            }
            return results;
        }
        public T HashSetSan<T>(string key, string filed, int dataBaseIndex = 0) where T : new()
        {
            var redisvalue = HashGetPrimitive(key, filed);

            if (!string.IsNullOrWhiteSpace(redisvalue))
            {
                return JsonPase.Deserialize<T>(redisvalue);
            }
            else return default(T);
        }
        public void HashSet<V>(string key, IList<KeyValuePair<string, V>> valuePairs, int dataBaseIndex = 0)
        {
            if (valuePairs != null && valuePairs.Count > 0)
            {

                var database = _factory.GetDatabase(dataBaseIndex);

                HashEntry[] entries = new HashEntry[valuePairs.Count];

                for (var i = 0; i < valuePairs.Count; i++)
                {
                    entries[i] = new HashEntry(valuePairs[i].Key, JsonPase.Serialize(valuePairs[i].Value));
                }
                database.HashSet(key, entries);
            }
        }
        public bool HashSet<V>(string key, string filed, V value,
           UpdateStrategy strategy, int dataBaseIndex = 0)
            where V : class
        {
            var isSuccess = true;
            if (value != null)
            {
                var database = _factory.GetDatabase(dataBaseIndex);

                if (strategy == UpdateStrategy.更新旧值 ||
                    !database.HashExists(key, filed))
                {
                    var redisValue = JsonPase.Serialize(value);

                    isSuccess = database.HashSet(key, filed, redisValue);
                }
                return isSuccess;
            }
            return false;
        }


        public bool SetStr<V>(string key, V value, 
            UpdateStrategy strategy= UpdateStrategy.忽略,
            int dataBaseIndex = 0) where V : class
        {
            if (value == null) return false;

            
            return SetStrPrimitive(key, JsonPase.Serialize(value), strategy);
        }

        public bool SetStrPrimitive(string key, string value,
            UpdateStrategy strategy = UpdateStrategy.忽略,
            int dataBaseIndex = 0)
        {
            if (value == null) return false;

            var database = _factory.GetDatabase(dataBaseIndex);

            if (strategy == UpdateStrategy.忽略
                && database.KeyExists(key))
            {
                    return true;
            }

            var redisValue = database.StringSet(key, value);

            return redisValue;
        }


        public void SetEntity<K, T>(K key, T value,
            UpdateStrategy strategy = UpdateStrategy.忽略, int dataBaseIndex = 0)
        {
            var strKey = "";
            if (key.GetType() == typeof(string))
            {
                strKey = key.ToString();
            }
            else
            {
                strKey = key.GetHashCode() + JsonPase.Serialize(key);
            }
            this.SetStrPrimitive(strKey, JsonPase.Serialize(value), strategy);
        }
    

        public double GetHashIncrement(string key, string filed,
            double seed = 1,
            int dataBaseIndex = 0)
        {

            var database = _factory.GetDatabase(dataBaseIndex);

            var value = database.HashIncrement(key, filed, seed);

            return value;

        }
        public void Remove(string key,int dataBaseIndex = 0)
        {
            var database = _factory.GetDatabase(dataBaseIndex);

            var redisValue = database.KeyDelete(key);
        }
        public T GetEntity<K, T>(K key,int dataBaseIndex = 0)
        {
            var strKey = GetKey(key);
            var value = this.GetStr(strKey, dataBaseIndex);

            if (!string.IsNullOrWhiteSpace(value))
            {
                return JsonPase.Deserialize<T>(value);
            }
            return default(T);
        }
        private string GetKey<K>(K key)
        {
            var strKey = "";
            if (key.GetType() == typeof(string))
            {
                strKey = key.ToString();
            }
            else
                strKey = key.GetHashCode() + JsonPase.Serialize(key);
            return strKey;
        }
        public bool Exists<K>(K key,int dataBaseIndex=0)
        {
            var strKey = GetKey(key);

            var database = _factory.GetDatabase(dataBaseIndex);

            var exists=  database.KeyExists(strKey);

            return exists;
        }

        
    }
}
