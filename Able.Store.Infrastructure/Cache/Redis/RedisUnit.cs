using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Able.Store.Infrastructure.Cache.Redis
{
    public class RedisUnit : RedisContext
    {
        internal void StringAdd(int dataBaseIndex, RedisKey key, RedisValue value, TimeSpan? expire,
            When when = When.Always)
        {
            var database = GetDatabase(dataBaseIndex);
              database.StringSet(key, value, when: when);

            if (expire.HasValue)
            {
                database.KeyExpire(key, expire.Value);
            }
        }

        internal void StringAdd(int dataBaseIndex,KeyValuePair<RedisKey, RedisValue>[] valuePair, TimeSpan? expire,
        When when = When.Always)
        {
            var database = GetDatabase(dataBaseIndex);
            database.StringSet(valuePair,  when: when);

            if (expire.HasValue)
            {
                foreach (var item in valuePair)
                {
                    database.KeyExpire(item.Key,expire);
                }
            }
        }
      
        internal void SortSetAdd(int dataBaseIndex, RedisKey key, SortedSetEntry value,
            TimeSpan? expire, When when= When.Always)
        {
            var database = GetDatabase(dataBaseIndex);

                database.SortedSetAdd(key,value.Element,value.Score,when:when);

            if(expire.HasValue)
                database.KeyExpire(key,expire);
        }
        internal void SortSetAdd(int dataBaseIndex, RedisKey key, SortedSetEntry[] values, TimeSpan? expire)
        {
            var database = GetDatabase(dataBaseIndex);

            if (database.KeyExists(key)) return;

            database.SortedSetAdd(key, values);
            if (expire.HasValue)
                database.KeyExpire(key, expire);
        }

        internal void HashSetAdd(int dataBaseIndex,RedisKey key, HashEntry[]values,TimeSpan? expire)
        {
            var database = GetDatabase(dataBaseIndex);

            if (database.KeyExists(key))
                return;

            database.HashSet(key, values);
            if (expire.HasValue)
                database.KeyExpire(key, expire);
        }

        internal void HashSetAdd(int dataBaseIndex, RedisKey key, RedisValue filed, RedisValue value, TimeSpan? expire, When when)
        {
            var database = GetDatabase(dataBaseIndex);

            database.HashSet(key, filed, value, when: when);
            if (expire.HasValue)
                database.KeyExpire(key,expire);
        }
        internal void ListLPush(int dataBaseIndex,RedisKey key,RedisValue value, TimeSpan? expire, When when)
        {

            var database = GetDatabase(dataBaseIndex);

            database.ListLeftPush(key, value,when: when);
            if (expire.HasValue)
                database.KeyExpire(key,expire);
        }

        internal void ListLPush(int dataBaseIndex, RedisKey key, RedisValue[] values, TimeSpan? expire)
        {
            var database = GetDatabase(dataBaseIndex);

            if (database.KeyExists(key)) return;

            database.ListLeftPush(key, values);
            if (expire.HasValue)
                database.KeyExpire(key, expire);
        }

        internal void SetAdd(int dataBaseIndex, RedisKey key, RedisValue value, TimeSpan? expire)
        {
            var database = GetDatabase(dataBaseIndex);
            if (database.KeyExists(key))
                return;

            database.SetAdd(key,value);

            if (expire.HasValue) database.KeyExpire(key,expire);
        }
        internal void SetAdd(int dataBaseIndex, RedisKey key, RedisValue[] value, TimeSpan? expire)
        {
            var database = GetDatabase(dataBaseIndex);
            if (database.KeyExists(key))
                return;

            database.SetAdd(key, value);

            if (expire.HasValue) database.KeyExpire(key, expire);
        }

        internal void Delete(int dataBaseIndex, RedisKey key)
        {
            var database = GetDatabase(dataBaseIndex);

            database.KeyDelete(key);
        }
        internal void Delete(int dataBaseIndex, RedisKey[] key)
        {
            var database = GetDatabase(dataBaseIndex);

            database.KeyDelete(key);
        }
        internal void HashRemoveFiled(int dataBaseIndex, RedisKey key,RedisValue filed)
        {
            var database = GetDatabase(dataBaseIndex);

            database.HashDelete(key, filed);
        }

        internal void HashRemoveFiled(int dataBaseIndex, RedisKey key, RedisValue[] fileds)
        {
            var database = GetDatabase(dataBaseIndex);

            database.HashDelete(key, fileds);
        }

        internal void Publish(int dataBaseIndex, RedisChannel channel, RedisValue message)
        {
            var database = GetDatabase(dataBaseIndex);

            database.Publish(channel, message);
        }

    }
}
