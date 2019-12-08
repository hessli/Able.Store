using Able.Store.Infrastructure.ConfigCenter;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
namespace Able.Store.Infrastructure.Queue.RabbitTempContainer
{
    internal class ConnectionFactoryPool
    {
        private ConnectionFactoryPool()
        {

        }
        public static ConnectionFactoryPool Instance
        {
            get;
            private set;
        }
        static ConnectionFactoryPool (){
            Instance = new ConnectionFactoryPool();
        }
        ConcurrentQueue<RabbitConnectionFactory> _queue = new ConcurrentQueue<RabbitConnectionFactory>();

        Dictionary<string, IConnectOptions> _keys = new Dictionary<string,IConnectOptions>();

        ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
        public bool IsEmpty {

            get {
                return _queue.IsEmpty;
            }
        }
        public void Add(IConnectOptions key, RabbitConnectionFactory factory)
        {
            rwLock.EnterWriteLock();

            IConnectOptions connectOptions = null;
            try
            {
                if (_keys.TryGetValue(key.TagName, out connectOptions))
                {
                    //但是属性已发生变更
                    if (connectOptions.CompareTo(key)== 0)
                    {
                        RabbitConnectionFactory oldFactory = null;

                        _keys.Remove(key.TagName);

                        _queue.TryDequeue(out oldFactory);

                        //重新入队
                        _keys.Add(key.TagName, key);
                        _queue.Enqueue(factory);
                    }
                }
                else
                {
                    _keys.Add(key.TagName, key);
                    _queue.Enqueue(factory);
                }
            }
            finally {

                rwLock.ExitWriteLock();
            }
        }
        public bool  TryGet(out RabbitConnectionFactory factory)
        {
            rwLock.EnterReadLock();
            RabbitConnectionFactory tempFactory = null;
            try
            {
                while (!_queue.IsEmpty)
                {
                    if (_queue.TryDequeue(out tempFactory))
                    {

                        if (tempFactory.Connection != null)
                        {
                            _queue.Enqueue(tempFactory);

                            factory = tempFactory;

                            return true;
                        }
                    }
                }
            }
            finally
            {
                rwLock.ExitReadLock();
            }
            factory = tempFactory;
            return false;
        } 
    }
}
