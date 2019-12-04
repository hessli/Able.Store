using Able.Store.Infrastructure.Cache.Redis.RabbitTempContainer;
using System.Collections.Generic;

namespace Able.Store.Infrastructure.Cache.Redis
{
    public class CacheController
    {
        internal readonly RedisConnectionPool POOL = RedisConnectionPool.Instance;

        public CacheController()
        {
            RedisConnectionFactory connectionFactory = null;
            if (!POOL.TryGet(out connectionFactory))
            {
                //触发事件记录日志等后续方案不写了后面慢慢补全
            }
            _connectionFactory = connectionFactory;

            // POOL.RetrunBroken(connectionFactory); ;

            _redisStorage = new RedisStorage(_connectionFactory);
        }
        RedisConnectionFactory _connectionFactory;
        IRedisStorage _redisStorage;
        public double GetHashIncrement(string key, string filed,
          double seed = 1,
          int dataBaseIndex = 0)
        {
            return _redisStorage.GetHashIncrement(key, filed, seed, dataBaseIndex);
        }
        public IList<T> HashValues<T>(string key, int dataBaseIndex = 0) where T : class
        {
            return _redisStorage.HashValues<T>(key, dataBaseIndex);
        }

        public bool Exsist<K>(K key,int dbIndex)
        {
            return _redisStorage.Exists(key,dbIndex);
        }

        public string GetStr(string key, int dataBaseIndex = 0)
        {
            return _redisStorage.GetStr(key, dataBaseIndex);
        }

        public string HashGetPrimitive(string key, string filed, int dataBaseIndex = 0)
        {
            return _redisStorage.HashGetPrimitive(key, filed, dataBaseIndex);
        }

        public T HashSetSan<T>(string key, string filed, int dataBaseIndex = 0) where T : new()
        {
            return _redisStorage.HashSetSan<T>(key, filed, dataBaseIndex);
        }

        public void HashSet<V>(string key, IList<KeyValuePair<string, V>> valuePairs, int dataBaseIndex = 0)
        {
            _redisStorage.HashSet<V>(key, valuePairs, dataBaseIndex);
        }
     
    
        public void Remove(string key,int dataBaseIndex = 0)
        {
            _redisStorage.Remove(key, dataBaseIndex);
        }
        public T GetEntity<K, T>(K key, int dataBaseIndex = 0)
        {
            return _redisStorage.GetEntity<K, T>(key, dataBaseIndex);
        }
        public void SetEntity<K, T>(K key, T value,
            UpdateStrategy strategy= UpdateStrategy.忽略, int dataBaseIndex = 0)
        {
            _redisStorage.SetEntity<K, T>(key, value, strategy, dataBaseIndex);
        }
        public bool SetStrPrimitive(string key, string value, 
            UpdateStrategy strategy = UpdateStrategy.忽略, int dataBaseIndex = 0)
        {
            return _redisStorage.SetStrPrimitive(key, value, strategy, dataBaseIndex);
        }
        public bool SetStr<V>(string key, V value, UpdateStrategy strategy = UpdateStrategy.忽略, int dataBaseIndex = 0) where V : class
        {
            return _redisStorage.SetStr(key, value, strategy, dataBaseIndex);

        }
        public bool HashSet<V>(string key, string filed, 
            V value, UpdateStrategy strategy = UpdateStrategy.忽略, int dataBaseIndex = 0) where V : class
        {
            return _redisStorage.HashSet<V>(key, filed, value, strategy, dataBaseIndex);
        }
    }
}
