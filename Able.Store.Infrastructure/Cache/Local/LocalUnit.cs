//using System.Collections;
//using System.Threading;

//namespace Able.Store.Infrastructure.Cache.Local
//{
//    public class LocalUnit :  ILocalCachLock
//    {
//        private static Hashtable _dataBase = new Hashtable();

//        private static ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();
        
        
//        ReaderWriterLockSlim ILocalCachLock.GetLock()
//        {
//            return _rwLock;
//        }
//        public void Add<KeyId>(ICacheModel<KeyId> cacheModel) where KeyId : class
//        {
//            _rwLock.EnterWriteLock();
//            try
//            {
//                var model=  ((LocalCacheModel<KeyId>)cacheModel);
//                if (!_dataBase.ContainsKey(model.Key))
//                {
//                    _dataBase.Add(model.Key, model.Data);
//                    return;
//                }
//            }
//            finally
//            {
//                _rwLock.ExitWriteLock();
//            }
//        }
//        public void Update<KeyId>(ICacheModel<KeyId> cacheModel) where KeyId : class
//        {
//            _rwLock.EnterWriteLock();
//            try
//            {
//                var model = ((LocalCacheModel<KeyId>)cacheModel);
//                if (!_dataBase.ContainsKey(model.Key))
//                {
//                    _dataBase.Add(model.Key, model.Data);
//                    return;
//                }
//                else
//                {
//                    _dataBase.Remove(model.Key);
//                    _dataBase.Add(model.Key,model.Data);
//                }
//            }
//            finally
//            {
//                _rwLock.ExitWriteLock();
//            }
//        }
//        public void Delete<K>(K key, int dataBaseIndex) where K : class
//        {
//            _rwLock.EnterWriteLock();
//            if (_dataBase.Count > 0)
//            {
//                try
//                {
//                    _dataBase.Remove(key);
//                }
//                finally
//                {
//                    _rwLock.ExitWriteLock();
//                }
//            }
//        }

//        public void Delete<K>(K[] keys, int dataBaseIndex) where K : class
//        {
//            _rwLock.EnterWriteLock();
//            if (_dataBase.Count > 0)
//            {
//                try
//                {
//                    foreach (var item in keys)
//                    {
//                        _dataBase.Remove(item);
//                    }
//                }
//                finally
//                {
//                    _rwLock.ExitWriteLock();
//                }
//            }
//        }

//        public Hashtable GetDatabase(int dataBaseIndex=0)
//        {
//            return _dataBase;
//        }
//    }
//}
