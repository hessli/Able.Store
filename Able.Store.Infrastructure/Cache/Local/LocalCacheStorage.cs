//using System.Collections;
//using System.Collections.Generic;
//using System.Threading;
//namespace Able.Store.Infrastructure.Cache.Local
//{
//    public class LocalCacheStorage : ICacheStorage,ICacheReadOnly
//    {
//        public ILocalCache _unit;
//        private ReaderWriterLockSlim _lock;
//        public LocalCacheStorage(ICacheUnit<Hashtable> unit)
//        {
//            _unit = (ILocalCache)unit;
//            _lock=((ILocalCachLock)_unit).GetLock(); 
//        }

//        public void Add<KeyId>(ICacheModel<KeyId> cacheModel) where KeyId : class
//        {
//            _unit.Add(cacheModel);
//        }

//        public void Delete<K>(K key, int dataBaseIndex) where K : class
//        {
//            _unit.Delete(key,dataBaseIndex);
//        }

//        public void Delete<K>(K[] keys, int dataBaseIndex) where K : class
//        {
//            _unit.Delete(keys, dataBaseIndex);
//        }
//        public bool Exists<KeyId>(ICacheModel<KeyId> cacheModel) where KeyId : class
//        {

//            _lock.EnterReadLock();
//            try
//            {
//                var dataBase = _unit.GetDatabase();

//                return dataBase.ContainsKey(cacheModel.Key);
//            }
//            finally {

//                _lock.ExitReadLock();
//            }
//        }
//        public IList<Value> GetList<KeyId, Value>(ICacheModel<KeyId> cacheModel) where KeyId : class
//        {
//            throw new  System.NotSupportedException();
//        }

//        public Value GetSinger<KeyId, Value>(ICacheModel<KeyId> cacheModel) where KeyId : class
//        {
//            _lock.EnterReadLock();
//            try
//            {

//                var dataBase = _unit.GetDatabase();

//                if (dataBase.ContainsKey(cacheModel.Key))
//                {
//                     return  (Value)dataBase[cacheModel.Key];
//                }
//                return default(Value);
//            }
//            finally
//            {

//                _lock.ExitReadLock();
//            }
//        }

//        public void Update<KeyId>(ICacheModel<KeyId> cacheModel) where KeyId : class
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}
