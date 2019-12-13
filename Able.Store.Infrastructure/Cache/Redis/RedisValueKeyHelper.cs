using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;

namespace Able.Store.Infrastructure.Cache.Redis
{
    internal class RedisValueKeyHelper
    {
       internal static RedisKey ToKey<K>(K key)
        {
            var keyStr = Utils.JsonPase.Serialize(key);
            return keyStr;
        }


        internal static SortedSetEntry[] ToEntry<Value>(IEnumerable<KeyValuePair<Value, double>> values)
        {
            var enumerator = values.GetEnumerator();
            var keyValuePairs = new SortedSetEntry[values.Count()];
            int i = 0;
            while (enumerator.MoveNext())
            {
                var value = ToValue(enumerator.Current.Key);

                keyValuePairs[i] = new SortedSetEntry(value, enumerator.Current.Value);
                i++;
            }
            return keyValuePairs;
        }

        internal static RedisKey[] ToKeys<K>(IEnumerable<K> keys)
        {
            var enumerator = keys.GetEnumerator();
            var keyValues = new RedisKey[keys.Count()];
            int i = 0;
            while (enumerator.MoveNext())
            {
                var value = ToKey(enumerator.Current);

                keyValues[i] = value;
                i++;
            }
            return keyValues;
        }

        internal static RedisValue ToValue<V>(V filedKey)
        {
            var keyStr = Utils.JsonPase.Serialize(filedKey);

            return keyStr;
        }


        internal static RedisValue[] ToValues<V>(IEnumerable<V> values)
        {
            var enumerator = values.GetEnumerator();

            var keyValues = new RedisValue[values.Count()];

            int i = 0;
            while (enumerator.MoveNext())
            {
                var value = ToValue(enumerator.Current);

                keyValues[i] = value;
                i++;
            }
            return keyValues;
        }
    }
}
