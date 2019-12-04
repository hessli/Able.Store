using System;
using System.Collections;
using System.Threading;

namespace Able.Store.Infrastructure.Cache.Local
{
    public class GenericCacheStorage: IGenericCacheStorage
    {
        private Hashtable dictionary = new Hashtable();

        private int _count=0;

        /// <summary>
        /// 用于并发访问的同步锁
        /// </summary>

        private ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

        public void Remove(string key, int dbIndex = 0)
        {
            if (dictionary.Count > 0)
            {
                rwLock.EnterWriteLock();
                try
                {

                    dictionary.Remove(key);
                    _count--;
                }
                finally
                {
                    rwLock.ExitWriteLock();
                }
            }
        }

        public T GetEntity<K, T>(K key, int dbIndex = 0)
        {
            rwLock.EnterReadLock();
            try
            {
                var value= dictionary[key];

                return  (T)value;     
            }
            finally
            {

                rwLock.ExitReadLock();
            }
        }
        public void SetEntity<K, T>(K key, T value,
            UpdateStrategy strategy=UpdateStrategy.忽略, int dbIndex = 0)
        {
           
            rwLock.EnterWriteLock();
            try
            {
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, value);
                    this._count++;
                    return;
                }
                if (strategy != UpdateStrategy.忽略)
                {
                    dictionary.Remove(key);
                    dictionary.Add(key, value);
                }
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }

        public bool Exists<K>(K key, int dbIndex = 0)
        {
            rwLock.EnterWriteLock();
            try
            {
              return     dictionary.ContainsKey(key);
            }
            finally {

                rwLock.ExitReadLock();
            }
        }

        public int GetCount(int dbIndex=0) {
            return this._count;
        }
    }
}
