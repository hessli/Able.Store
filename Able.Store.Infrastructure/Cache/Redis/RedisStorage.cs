using Able.Store.Infrastructure.Cache.Model;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
namespace Able.Store.Infrastructure.Cache.Redis
{
    public class RedisStorage : ICacheStorage
    {

        private RedisRepository _redisRepository;
        public RedisStorage()
        {
            _redisRepository = new RedisRepository(new RedisUnit());
        }

        public void Delete<K>(int dataBaseIndex, K key)
        {
            _redisRepository.Delete(dataBaseIndex, RedisValueKeyHelper.ToKey(key));
        }

        public void Delete<K>(int dataBaseIndex, HashSet<K> keys)
        {
            RedisKey[] keyValues = RedisValueKeyHelper.ToKeys(keys);

            _redisRepository.Delete(dataBaseIndex, keyValues);
        }
        public void HashRemoveFiled<Key, FiledKey>(int dataBaseIndex, Key key, FiledKey filed)
        {
            var keyValue = RedisValueKeyHelper.ToKey(key);
            var fileKey = RedisValueKeyHelper.ToValue(filed);

            _redisRepository.HashRemoveFiled(dataBaseIndex, keyValue, fileKey);
        }

        public void HashRemoveFiled<Key, FiledKey>(int dataBaseIndex, Key key, HashSet<FiledKey> fileds)
        {
            var keyValue = RedisValueKeyHelper.ToKey(key);
            var fileKeys = RedisValueKeyHelper.ToValues(fileds);

            _redisRepository.HashRemoveFiled(dataBaseIndex, keyValue, fileKeys);
        }

        public void HashSetAdd<K, KeyFiled, V>(CacheUnitModel cacheUnitModel, K key, KeyFiled filed, V value)
        {
            var keyValue = RedisValueKeyHelper.ToKey(key);

            var fileKey = RedisValueKeyHelper.ToValue(filed);

            var redisValue = RedisValueKeyHelper.ToValue(value);

            _redisRepository.HashSetAdd(cacheUnitModel.DataBaseIndex, keyValue, fileKey,
                redisValue, cacheUnitModel.Expire, cacheUnitModel.GetWhen());
        }

        public void HashSetAdd<K, KeyFiled, V>(CacheUnitModel cacheUnitModel, K key, Dictionary<KeyFiled, V> values)
        {
            var keyValue = RedisValueKeyHelper.ToKey(key);
        }
        public void StringAdd<K, V>(CacheUnitModel cacheUnitModel, K key, V value)
        {
            var redisValue = RedisValueKeyHelper.ToValue(value);
            var redisKey = RedisValueKeyHelper.ToKey(key);
            _redisRepository.StringAdd(cacheUnitModel.DataBaseIndex, redisKey,
                redisValue, cacheUnitModel.Expire, cacheUnitModel.GetWhen());
        }

        public void StringAdd<K, V>(CacheUnitModel cacheUnitModel, Dictionary<K, V> values)
        {
            throw new NotImplementedException();
        }
        public void SetAdd<K, V>(CacheUnitModel cacheUnitModel, K key, V value)
        {
            var redisValue = RedisValueKeyHelper.ToValue(value);
            var redisKey = RedisValueKeyHelper.ToKey(key);
            _redisRepository.SetAdd(cacheUnitModel.DataBaseIndex, redisKey, redisValue, cacheUnitModel.Expire);
        }
        public void SetAdd<K, V>(CacheUnitModel cacheUnitModel, K key, IEnumerable<V> values)
        {
            var redisKey = RedisValueKeyHelper.ToKey(key);
            var redisValues = RedisValueKeyHelper.ToValues(values);
            _redisRepository.SetAdd(cacheUnitModel.DataBaseIndex, redisKey, redisValues, cacheUnitModel.Expire);
        }

        public void SortedSetAdd<Key, Value>(CacheUnitModel model, Key key, IEnumerable<KeyValuePair<Value, double>> keyValuePairs)
        {
            var redisKey = RedisValueKeyHelper.ToKey(key);

            var values = RedisValueKeyHelper.ToEntry(keyValuePairs);

            _redisRepository.SortSetAdd(model.DataBaseIndex, redisKey, values, model.Expire);
        }

        public void SortedSetAdd<Key, Value>(CacheUnitModel model, Key key, KeyValuePair<Value, double> keyValuePairs)
        {
            var redisKey = RedisValueKeyHelper.ToKey(key);

            var redisValue = RedisValueKeyHelper.ToValue(keyValuePairs.Key);

            var sortedSetEntry = new SortedSetEntry(redisValue, keyValuePairs.Value);

            _redisRepository.SortSetAdd(model.DataBaseIndex, redisKey, sortedSetEntry, model.Expire);
        }
        public long HashIncrement(CacheUnitModel model, string key, string filed, long value = 1)
        {
            return  _redisRepository.HashIncrement(model.DataBaseIndex, key,filed, model.Expire, value);
        }
        public void ListLPush<K, V>(CacheUnitModel cacheUnitModel, K key, V value)
        {
            var redisKey = RedisValueKeyHelper.ToKey(key);

            var redisValue = RedisValueKeyHelper.ToValue(value);

            _redisRepository.ListLPush(cacheUnitModel.DataBaseIndex, redisKey,
            redisValue, cacheUnitModel.Expire, cacheUnitModel.GetWhen());

        }
        public void ListLPush<K, V>(CacheUnitModel cacheUnitModel, K key, IEnumerable<V> values)
        {
            var redisKey = RedisValueKeyHelper.ToKey(key);

            var redisValue = RedisValueKeyHelper.ToValues(values);
            _redisRepository.ListLPush(cacheUnitModel.DataBaseIndex, redisKey, redisValue, cacheUnitModel.Expire);
        }
        public void Publish<V>(int dataBaseIndex, string channelName, V message)
        {
            RedisChannel channel = new RedisChannel(channelName, RedisChannel.PatternMode.Literal);

            var redisValue = RedisValueKeyHelper.ToValue(message);

            _redisRepository.Publish(dataBaseIndex, channel, redisValue);
        }
        public Value HashSetGet<Key, FiledKey, Value>(int dataBaseIndex, Key key, FiledKey filed)
        {
            var redisKey = RedisValueKeyHelper.ToKey(key);

            var filedValue = RedisValueKeyHelper.ToValue(filed);

            var data = _redisRepository.HashSetGet(dataBaseIndex, redisKey, filedValue);

            if (data.HasValue)
            {

                var result = Utils.JsonPase.Deserialize<Value>(data);
                return result;
            }

            return default(Value);
        }

        public IList<Value> HashSetGet<Key, FiledKey, Value>(int dataBaseIndex, Key key, HashSet<FiledKey> fileds)
        {
            var redisKey = RedisValueKeyHelper.ToKey(key);

            var filedValues = RedisValueKeyHelper.ToValues(fileds);

            var data = _redisRepository.HashSetGet(dataBaseIndex, redisKey, filedValues);

            IList<Value> result = new List<Value>();

            for (var i = 0; i < data.Length; i++)
            {
                result.Add(Utils.JsonPase.Deserialize<Value>(data[i]));
            }
            return result;
        }

        public Dictionary<Key, Value> HashSetGetAll<Key, Value>(int dataBaseIndex, Key key)
        {
            var redisKey = RedisValueKeyHelper.ToKey(key);

            var data = _redisRepository.HashSetGetAll(dataBaseIndex, redisKey);


            Dictionary<Key, Value> dicResults = new Dictionary<Key, Value>();

            for (var i = 0; i < data.Length; i++)
            {
                dicResults.Add(Utils.JsonPase.Deserialize<Key>(data[i].Name),
                    Utils.JsonPase.Deserialize<Value>(data[i].Value));
            }
            return dicResults;
        }

        public IList<Value> ListRange<Key, Value>(int dataBaseIndex, Key key, long start = 0, long stop = -1)
        {
            var redisKey = RedisValueKeyHelper.ToKey(key);

            var data = _redisRepository.ListRange(dataBaseIndex, redisKey, start, stop);

            IList<Value> results = new List<Value>();

            for (var i = 0; i < data.Length; i++)
            {
                results.Add( Utils.JsonPase.Deserialize<Value>(data[i]));
            }
            return results;
        }

      
        public IList<Value> SortedSetRangeByRank<Key, Value>(int dataBaseIndex, Key key, long start = 0, long stop = -1)
        {
            var redisKey = RedisValueKeyHelper.ToKey(key);

            var data = _redisRepository.SortedSetRangeByRank(dataBaseIndex, redisKey, start, stop);

            IList<Value> results = new List<Value>();

            for (var i = 0; i < data.Length; i++)
            {
                results.Add(Utils.JsonPase.Deserialize<Value>(data[i]));
            }
            return results;
        }

        public IList<Value> SortedSetRangeByValue<Key, Value>(int dataBaseIndex, Key key, Value min, Value max, long skip, long take)
        {
            var redisKey = RedisValueKeyHelper.ToKey(key);

            var minRedisValue = RedisValueKeyHelper.ToValue(min);

            var maxRedisValue = RedisValueKeyHelper.ToValue(max);

            var data = _redisRepository.SortedSetRangeByValue(dataBaseIndex, redisKey, minRedisValue, maxRedisValue, skip, take);

            IList<Value> results = new List<Value>();

            for (var i = 0; i < data.Length; i++)
            {
                results.Add(Utils.JsonPase.Deserialize<Value>(data[i]));
            }
            return results;
        }
        public Value StringGet<Key, Value>(int dataBaseIndex, Key key)
        {
            var redisKey = RedisValueKeyHelper.ToKey(key);

            var data = _redisRepository.StringGet(dataBaseIndex, redisKey);

            if ( data.HasValue)
            {
                var result = Utils.JsonPase.Deserialize<Value>(data);

                return result;
            }
            return default(Value);
        }
        public IList<Value> StringGet<Key, Value>(int dataBaseIndex, HashSet<Key> keys)
        {
            var redisKeys = RedisValueKeyHelper.ToKeys(keys);
            var data = _redisRepository.StringGet(dataBaseIndex, redisKeys);
            IList<Value> results = new List<Value>();
            for (var i = 0; i < data.Length; i++)
            {
                results.Add(Utils.JsonPase.Deserialize<Value>(data[i]));
            }
            return results;
        }

        public void Subscrib(string channelName, Action<string, string> callback)
        {
            var channel = new RedisChannel(channelName, RedisChannel.PatternMode.Literal);

            Action<RedisChannel, RedisValue> action = (redisChannel, redisValue) =>
            {
                var channelStr = Utils.JsonPase.Serialize(redisChannel);
                callback(channelStr, redisValue);
            };

            _redisRepository.SubScrib(channel, action);
        }

        public bool KeyExists<Key>(int dataBaseIndex, Key redisKey)
        {
           var exists=  _redisRepository.KeyExists(dataBaseIndex, Utils.JsonPase.Serialize(redisKey));

            return exists;
        }
    }
}
