using Able.Store.Infrastructure.ConfigCenter;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
namespace Able.Store.Infrastructure.Cache.Redis.RedisTempContainer
{
    public class RedisConnectionPool
    {
        private RedisConnectionPool() {

        }
        public static RedisConnectionPool Instance {

            get;private set;
        }

        static RedisConnectionPool()
        {
            Instance = new RedisConnectionPool();
        }
        private object _synch = new object();

        Dictionary<string, IConnectOptions> _keys = new Dictionary<string, IConnectOptions>();

        ConcurrentDictionary<string, RedisConnectionFactory> _dicFactory = new ConcurrentDictionary<string, RedisConnectionFactory>();

        private ConcurrentQueue<RedisConnectionFactory> _queue = new
         ConcurrentQueue<RedisConnectionFactory>();
        public bool IsEmpty {

            get {
                return _queue.IsEmpty;
            }
        }

        int _count = 0;
        public void Add(IConnectOptions connectOptions, RedisConnectionFactory factory)
        {
            lock (_synch)
            {
                Interlocked.Increment(ref _count);

                IConnectOptions oldConnectOptions = null;
                if (_keys.TryGetValue(connectOptions.TagName, out oldConnectOptions))
                {
                    RedisConnectionFactory oldFactory = null;
                    //但是属性已发生变更
                    if (connectOptions.CompareTo(oldConnectOptions) == 0)
                    {
                        _keys.Remove(connectOptions.TagName);
                        _queue.TryDequeue(out oldFactory);

                        //重新入队
                        _keys.Add(connectOptions.TagName, connectOptions);
                        _queue.Enqueue(factory);
                    }
                }
                else
                {
                    _keys.Add(connectOptions.TagName, connectOptions);
                    _queue.Enqueue(factory);
                    
                }
                Interlocked.Decrement(ref _count);
            }
        }
        public bool TryGet(out RedisConnectionFactory factory)
        {
            RedisConnectionFactory temp=null;

            lock (_synch)
            {
                if (_queue.TryDequeue(out temp) && temp.IsOpend)
                {
                    factory = temp;
                    _queue.Enqueue(temp);
                    return true;
                }
                factory = null;
            }
            return false;
        }
      
    }
}
