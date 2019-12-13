using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Able.Store.Infrastructure.Cache.Redis
{
    public class RedisRepository
    {
        private RedisUnit _unitOfWork;
        public RedisRepository(RedisUnit unit)
        {
            _unitOfWork = unit;
        }
        public void StringAdd(int dataBaseIndex, RedisKey key, RedisValue value, TimeSpan? expire,
            When when = When.Always)
        {
            _unitOfWork.StringAdd(dataBaseIndex, key, value, expire, when);
        }
        public void StringAdd(int dataBaseIndex, KeyValuePair<RedisKey, RedisValue>[] valuePair, TimeSpan? expire,
        When when = When.Always)
        {
            _unitOfWork.StringAdd(dataBaseIndex, valuePair, expire, when);
        }
        public void SortSetAdd(int dataBaseIndex, RedisKey key, SortedSetEntry value,
             TimeSpan? expire, When when = When.Always)
        {
            _unitOfWork.SortSetAdd(dataBaseIndex, key, value, expire, when);
        }

        public void SortSetAdd(int dataBaseIndex, RedisKey key, SortedSetEntry[] values, TimeSpan? expire)
        {
            _unitOfWork.SortSetAdd(dataBaseIndex, key, values, expire);
        }
        public void HashSetAdd(int dataBaseIndex, RedisKey key, RedisValue filed, RedisValue value, TimeSpan? expire, When when)
        {
            _unitOfWork.HashSetAdd(dataBaseIndex, key, filed, value, expire, when);
        }
        public void HashSetAdd(int dataBaseIndex, RedisKey key, HashEntry[] values, TimeSpan? expire)
        {
            _unitOfWork.HashSetAdd(dataBaseIndex, key, values, expire);
        }
        public void ListLPush(int dataBaseIndex, RedisKey key, RedisValue value, TimeSpan? expire, When when)
        {
            _unitOfWork.ListLPush(dataBaseIndex, key, value, expire, when);
        }
        public void ListLPush(int dataBaseIndex, RedisKey key, RedisValue[] values, TimeSpan? expire)
        {
            _unitOfWork.ListLPush(dataBaseIndex, key, values, expire);
        }
        public void SetAdd(int dataBaseIndex, RedisKey key, RedisValue value, TimeSpan? expire)
        {
            _unitOfWork.SetAdd(dataBaseIndex, key, value, expire);
        }
        public void SetAdd(int dataBaseIndex, RedisKey key, RedisValue[] values, TimeSpan? expire)
        {
            _unitOfWork.SetAdd(dataBaseIndex, key, values, expire);
        }
        public void Delete(int dataBaseIndex, RedisKey key)
        {
            _unitOfWork.Delete(dataBaseIndex, key);
        }
        public void Delete(int dataBaseIndex, RedisKey[] key)
        {
            _unitOfWork.Delete(dataBaseIndex, key);
        }
        public void HashRemoveFiled(int dataBaseIndex, RedisKey key, RedisValue filed)
        {
            _unitOfWork.HashRemoveFiled(dataBaseIndex, key, filed);
        }
        public void HashRemoveFiled(int dataBaseIndex, RedisKey key, RedisValue[] fileds)
        {
            _unitOfWork.HashRemoveFiled(dataBaseIndex, key, fileds);
        }
        public void Publish(int dataBaseIndex, RedisChannel channel, RedisValue message)
        {
            _unitOfWork.Publish(dataBaseIndex, channel, message);
        }
     
        public RedisValue StringGet(int dataBaseIndex, RedisKey key)
        {
            var dataBase = _unitOfWork.GetDatabase(dataBaseIndex);

            var data = dataBase.StringGet(key);
            return data;
        }
        public bool KeyExists(int dataBaseIndex,RedisKey redisKey)
        {
            var dataBase = _unitOfWork.GetDatabase(dataBaseIndex);

            return  dataBase.KeyExists(redisKey);
        }
        public RedisValue[] StringGet(int dataBaseIndex, RedisKey[] keys)
        {
            var dataBase = _unitOfWork.GetDatabase(dataBaseIndex);

            var data = dataBase.StringGet(keys);
            return data;
        }
        public RedisValue HashSetGet(int dataBaseIndex, RedisKey key, RedisValue filed)
        {
            var dataBase = _unitOfWork.GetDatabase(dataBaseIndex);

            var data = dataBase.HashGet(key, filed);
            return data;
        }
        public RedisValue[] HashSetGet(int dataBaseIndex, RedisKey key, RedisValue[] fileds)
        {
            var dataBase = _unitOfWork.GetDatabase(dataBaseIndex);

            var data = dataBase.HashGet(key, fileds);
            return data;
        }

        public long HashIncrement(int dataBaseIndex , RedisKey key, RedisValue filed,TimeSpan? expire, long value=1)
        {
            var dataBase = _unitOfWork.GetDatabase(dataBaseIndex);

            var data = dataBase.HashIncrement(key,filed,value);

            if(expire.HasValue)
            dataBase.KeyExpire(key, expire);

            return data;
        }
        public HashEntry[] HashSetGetAll(int dataBaseIndex, RedisKey key)
        {
            var dataBase = _unitOfWork.GetDatabase(dataBaseIndex);

            var data = dataBase.HashGetAll(key);

            return data;
        }
        public RedisValue[] SortedSetRangeByRank(int dataBaseIndex, RedisKey key, long start = 0, long stop = -1)
        {
            var dataBase = _unitOfWork.GetDatabase(dataBaseIndex);

            var data = dataBase.SortedSetRangeByRank(key, start, stop);

            return data;
        }

        public RedisValue[] SortedSetRangeByValue(int dataBaseIndex, RedisKey key, RedisValue min, RedisValue max, long skip, long take)
        {
            var dataBase = _unitOfWork.GetDatabase(dataBaseIndex);

            var data = dataBase.SortedSetRangeByValue(key, min, max, skip: skip, take: take);

            return data;
        }

        public RedisValue[] ListRange(int dataBaseIndex, RedisKey key, long start = 0, long stop = -1)
        {
            var dataBase = _unitOfWork.GetDatabase(dataBaseIndex);

            var data = dataBase.ListRange(key, start, stop);

            return data;
        }

      

        public void SubScrib(RedisChannel channel, Action<RedisChannel, RedisValue> callBack)
        {
            var scriber = _unitOfWork.GetSubscriber();

            scriber.Subscribe(channel, callBack);
        }
    }
}
