//using Able.Store.Infrastructure.Cache;
//using Able.Store.Infrastructure.Cache.Local;
//using System;

//namespace Able.Store.Infrastructure.Serve
//{
//    public static class ConfigurationBroker
//    {
//        private static readonly ICacheStorage genericCacheStorage ;
//        static ConfigurationBroker()
//        {
//            genericCacheStorage = new GenericCacheStorage();
//        }
//        public static void Add<K,V>(K k,V item) where V :class
//        {
//            if ((k == null) || item == null) throw new NullReferenceException();
//            genericCacheStorage.AddEntity(k,item);
//        }
//        public static V GetConfigurationObject<K,V>(K k) where V:class
//        {
//            if (((IGenericCacheStorage)genericCacheStorage).Count<=0)
//            {
//                return null;
//            }
//            V result=null;

//            result= genericCacheStorage.GetEntity<K,V>(k);

//            if (result == null)
//                return null;

//            return result;
//        }
//    }
//}
