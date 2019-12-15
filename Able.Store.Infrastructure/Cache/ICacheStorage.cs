using Able.Store.Infrastructure.Cache.Model;
using System;
using System.Collections.Generic;
namespace Able.Store.Infrastructure.Cache
{
    public interface ICacheStorage
    {
        void StringAdd<K, V>(CacheUnitModel cacheUnitModel, K key, V value);

        void StringAdd<K, V>(CacheUnitModel cacheUnitModel, Dictionary<K, V> values);

        void HashSetAdd<K,KeyFiled, V>(CacheUnitModel cacheUnitModel, K key, KeyFiled filed, V value);

        void HashSetAdd<K, KeyFiled,V>(CacheUnitModel cacheUnitModel, K key,Dictionary<KeyFiled,V> values);

        void ListLPush<K, V>(CacheUnitModel cacheUnitModel, K key, V value);

        void ListLPush<K, V>(CacheUnitModel cacheUnitModel, K key,  IEnumerable<V> values);

        void SetAdd<K, V>(CacheUnitModel cacheUnitModel, K key, V value);

        void SetAdd<K,V>(CacheUnitModel cacheUnitModel, K key, IEnumerable<V> values);

        void SortedSetAdd<Key, Value>(CacheUnitModel model, Key key, KeyValuePair<Value, double> keyValuePairs);
        void SortedSetAdd<Key, Value>(CacheUnitModel model, Key key, IEnumerable<KeyValuePair<Value, double>> keyValuePairs);
        long HashIncrement(CacheUnitModel model, string key, string filed, long value = 1);

        void Delete<K>(int dataBaseIndex, K key);

        void Delete<K>(int dataBaseIndex, HashSet<K> keys);

        void HashRemoveFiled<Key, FiledKey>(int dataBaseIndex, Key key, FiledKey filed);

        void HashRemoveFiled<Key, FiledKey>(int dataBaseIndex, Key key, HashSet<FiledKey> fileds);

        Value StringGet<Key, Value>(int dataBaseIndex, Key key);

        IList<Value> StringGet<Key, Value>(int dataBaseIndex, HashSet<Key> keys);

        Value HashSetGet<Key, FiledKey, Value>(int dataBaseIndex, Key key, FiledKey filed);

        IList<Value> HashSetGet<Key, FiledKey, Value>(int dataBaseIndex, Key key, HashSet<FiledKey> fileds);

        Dictionary<Key, Value> HashSetGetAll<Key, Value>(int dataBaseIndex, Key key);

        IList<Value> SortedSetRangeByRank<Key, Value>(int dataBaseIndex, Key key, long start = 0, long stop = -1);

        IList<Value> SortedSetRangeByValue<Key, Value>(int dataBaseIndex, Key key, Value min, Value max, long skip, long take);

        IList<Value> ListRange<Key,Value>(int dataBaseIndex, Key key, long start = 0, long stop = -1);

        void Publish<V>(int dataBaseIndex, string channelName, V message);

        bool KeyExists<Key>(int dataBaseIndex, Key redisKey);


        void Subscrib(string channelName, Action<string, string> callback);


    }
}
